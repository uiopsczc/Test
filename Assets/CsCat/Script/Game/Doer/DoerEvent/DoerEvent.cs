using System.Collections.Generic;

namespace CsCat
{
	public class DoerEvent : Doer
	{
		public DoerEventFactory GetDoerEventFactory()
		{
			return this.factory as DoerEventFactory;
		}

		public CfgDoerEventData GetCfgDoerEventData()
		{
			return CfgDoerEvent.Instance.get_by_id(this.GetId());
		}

		//////////////////////////////////////////////////////////////////////////
		//owner 发放任务的npc
		public virtual bool Execute(string desc, Doer owner, DoerAttrParser doerAttrParser)
		{
			if (!CheckDoerEventTriggerCondition(doerAttrParser))
				return false;
			var cfgDoerEventData = GetCfgDoerEventData();
			bool isNotTalk = cfgDoerEventData.is_not_talk; // 不弹出talk
															 //通用情况
			string triggerDesc = cfgDoerEventData.trigger_desc;
			List<string> wordList = new List<string>();
			if (!triggerDesc.IsNullOrWhiteSpace())
				wordList.Add(doerAttrParser.ParseString(triggerDesc));
			int ok = 1; // 0-触发条件失败，1-触发成功，执行失败，2-触发成功，执行成功
			string[] stepIds = cfgDoerEventData._step_ids;
			if (!stepIds.IsNullOrEmpty())
			{
				for (int i = 0; i < stepIds.Length; i++)
				{
					string stepId = stepIds[i];
					var cfgDoerEventStep = CfgDoerEventStep.Instance.get_by_id(stepId);
					ok = ExecuteStep(desc + "步骤" + (i + 1), stepId, owner, doerAttrParser, wordList);
					if (ok == 0)
						break;
					if (ok == 2 && cfgDoerEventStep.is_stop_here)
						break;
				}
			}

			if (!isNotTalk && wordList.Count > 0)
			{
				for (var i = 0; i < wordList.Count; i++)
				{
					var word = wordList[i];
					Client.instance.uiManager.Notify(word);
				}
			}

			return ok > 0;
		}

		public bool CheckDoerEventTriggerCondition(DoerAttrParser doerAttrParser)
		{
			var cfgDoerEventData = CfgDoerEvent.Instance.get_by_id(this.GetId());
			bool isNotTalk = cfgDoerEventData.is_not_talk; // 不弹出talk
			string triggerCondition = cfgDoerEventData.trigger_condition; // 触发条件
			if (!triggerCondition.IsNullOrWhiteSpace() && !doerAttrParser.ParseBoolean(triggerCondition)) //不满足触发的情况
			{
				if (!isNotTalk)
				{
					string canNotTriggerDesc = cfgDoerEventData.can_not_trigger_desc;
					Client.instance.uiManager.Notify(canNotTriggerDesc.IsNullOrWhiteSpace()
						? Lang.GetText("现在不能触发此操作")
						: doerAttrParser.ParseString(canNotTriggerDesc));
				}

				return false;
			}

			return true;
		}

		//owner 发放任务的npc
		public int ExecuteStep(string desc, string doerEventStepId, Doer owner, DoerAttrParser doerAttrParser,
		  List<string> wordList)
		{
			var cfgDoerEventStepData = CfgDoerEventStep.Instance.get_by_id(doerEventStepId);
			string triggerCondition = cfgDoerEventStepData.trigger_condition; // 触发条件
			if (!triggerCondition.IsNullOrWhiteSpace() && !doerAttrParser.ParseBoolean(triggerCondition)) //不满足触发的情况
			{
				string canNotTriggerDesc = cfgDoerEventStepData.can_not_trigger_desc;
				wordList.Add(canNotTriggerDesc.IsNullOrWhiteSpace()
					? Lang.GetText("现在不能触发此操作")
					: doerAttrParser.ParseString(canNotTriggerDesc));
				return 0;
			}

			string triggerDesc = cfgDoerEventStepData.trigger_desc; // 触发提示语
			if (!triggerDesc.IsNullOrWhiteSpace())
				wordList.Add(doerAttrParser.ParseString(triggerDesc));
			string executeCondition = cfgDoerEventStepData.execute_condition; // 执行条件
			if (!executeCondition.IsNullOrWhiteSpace() && !doerAttrParser.ParseBoolean(executeCondition)) //不满足执行条件的情况
			{
				string canNotExecuteDesc = cfgDoerEventStepData.can_not_execute_desc; // 不执行提示语
				if (!canNotExecuteDesc.IsNullOrWhiteSpace())
					wordList.Add(doerAttrParser.ParseString(canNotExecuteDesc));
				return 1;
			}

			string executeDesc = cfgDoerEventStepData.execute_desc; // 执行提示语
			if (!executeDesc.IsNullOrWhiteSpace())
				wordList.Add(doerAttrParser.ParseString(executeDesc));

			DoerAttrSetter doerAttrSetter = new DoerAttrSetter(desc, doerAttrParser);
			//设置属性、更改属性
			Dictionary<string, string> setAttrDict = cfgDoerEventStepData._set_attr_dict;
			foreach (var attrName in setAttrDict.Keys)
				doerAttrSetter.Set(attrName, setAttrDict[attrName], false);
			Dictionary<string, string> addAttrDict = cfgDoerEventStepData._add_attr_dict;
			foreach (var attrName in addAttrDict.Keys)
				doerAttrSetter.Set(attrName, addAttrDict[attrName], true);

			User user = null;
			if (doerAttrParser.GetU() is User)
				user = (User)doerAttrParser.GetU();
			else if (doerAttrParser.GetO() is User)
				user = (User)doerAttrParser.GetO();
			else if (doerAttrParser.GetE() is User)
				user = (User)doerAttrParser.GetE();
			else
				user = Client.instance.user;

			//添加或者删除物品
			Dictionary<string, string> dealItemDict = cfgDoerEventStepData._deal_item_dict;
			if (!dealItemDict.IsNullOrEmpty())
				user.DealItems(dealItemDict, doerAttrParser);

			// 接受任务
			string[] accept_mission_ids = cfgDoerEventStepData._accept_mission_ids;
			foreach (var accept_mission_id in accept_mission_ids)
				user.AcceptMission(accept_mission_id, owner);

			// 完成任务
			string[] finishMissionIds = cfgDoerEventStepData._finish_mission_ids;
			for (var i = 0; i < finishMissionIds.Length; i++)
			{
				var finishMissionId = finishMissionIds[i];
				user.FinishMission(finishMissionId, owner);
			}

			// 放弃任务
			string[] giveUpMissionIds = cfgDoerEventStepData._give_up_mission_ids;
			for (var i = 0; i < giveUpMissionIds.Length; i++)
			{
				var giveUpMissionId = giveUpMissionIds[i];
				user.GiveUpMission(giveUpMissionId, owner);
			}

			// 添加已完成任务
			string[] addFinishedMissionIds = cfgDoerEventStepData._add_finished_mission_ids;
			for (var i = 0; i < addFinishedMissionIds.Length; i++)
			{
				var addFinishedMissionId = addFinishedMissionIds[i];
				user.AddFinishedMissionId(addFinishedMissionId);
			}

			// 删除已完成任务
			string[] removeFinishedMissionIds = cfgDoerEventStepData._remove_finished_mission_ids;
			for (var i = 0; i < removeFinishedMissionIds.Length; i++)
			{
				var removeFinishedMissionId = removeFinishedMissionIds[i];
				user.RemoveFinishedMissionId(removeFinishedMissionId);
			}

			// 检测完成任务
			user.CheckAutoFinishMissions();
			return 2;
		}
	}
}