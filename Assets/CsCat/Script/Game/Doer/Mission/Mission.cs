using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class Mission : Doer
	{
		public MissionFactory GetMissionFactory()
		{
			return this.factory as MissionFactory;
		}

		public CfgMissionData GetCfgMissionData()
		{
			return CfgMission.Instance.GetById(this.GetId());
		}

		public override void OnInit()
		{
			base.OnInit();
		}

		public override void OnSave(Hashtable dict, Hashtable dictTmp)
		{
			base.OnSave(dict, dictTmp);
		}

		public override void OnRestore(Hashtable dict, Hashtable dictTmp)
		{
			base.OnRestore(dict, dictTmp);
		}

		//owner 发放任务的npc
		public virtual bool OnAccept(User user)
		{
			string onAcceptDoerEventId = this.GetCfgMissionData().onAcceptDoerEventId;
			if (!onAcceptDoerEventId.IsNullOrWhiteSpace())
			{
				var cfgDoerEventData = CfgDoerEvent.Instance.GetById(onAcceptDoerEventId);
				if (!cfgDoerEventData.isNotOpen)
				{
					if (!Client.instance.doerEventFactory.GetDoerEvent(onAcceptDoerEventId).Execute(
					  string.Format("{0} 接受任务 {1}", user.GetShort(), this.GetShort()), this.GetOwner(),
					  new DoerAttrParser(user, this, this.GetOwner())))
						return false;
				}
			}

			return true;
		}

		//owner 发放任务的npc
		public virtual void OnFinish(User user)
		{
			string onFinishDoerEventId = this.GetCfgMissionData().onFinishDoerEventId;
			if (!onFinishDoerEventId.IsNullOrWhiteSpace())
			{
				var cfgDoerEventData =
				  CfgDoerEvent.Instance.GetById(onFinishDoerEventId);
				if (!cfgDoerEventData.isNotOpen)
				{
					Client.instance.doerEventFactory.GetDoerEvent(onFinishDoerEventId).Execute(
					  string.Format("{0} 完成任务 {1}", user.GetShort(), this.GetShort()), this.GetOwner(),
					  new DoerAttrParser(user, this, this.GetOwner()));
				}
			}
		}

		//owner 发放任务的npc
		public void OnGiveUp(User user)
		{
			string onGiveUpDoerEventId = this.GetCfgMissionData().onGiveUpDoerEventId;
			if (!onGiveUpDoerEventId.IsNullOrWhiteSpace())
			{
				var cfgDoerEventData =
				  CfgDoerEvent.Instance.GetById(onGiveUpDoerEventId);
				if (!cfgDoerEventData.isNotOpen)
				{
					Client.instance.doerEventFactory.GetDoerEvent(onGiveUpDoerEventId).Execute(
					  string.Format("{0} 放弃任务 {1}", user.GetShort(), this.GetShort()), this.GetOwner(),
					  new DoerAttrParser(user, this, this.GetOwner()));
				}
			}
		}

		public virtual bool IsReady()
		{
			return CheckFinishCondition();
		}

		public bool CheckFinishCondition()
		{
			string finishCondition = this.GetCfgMissionData().finishCondition;
			if (!finishCondition.IsNullOrWhiteSpace()) // 未设置完成条件的办事任务不能根据派发任务处来完成，只能在设置了可完成任务的时候检测是否就绪
			{
				DoerAttrParser doerAttrParser = new DoerAttrParser(Client.instance.user, this, this.GetOwner());
				if (doerAttrParser.ParseBoolean(finishCondition, false))
					return true;
			}

			return false;
		}

		public virtual string GetStatusString()
		{
			return null;
		}

		public Dictionary<string, int> GetRewards(DoerAttrParser doerAttrParser)
		{
			Dictionary<string, int> result = new Dictionary<string, int>();

			Dictionary<string, string> rewardDict = GetCfgMissionData()._rewardDict;
			if (!rewardDict.IsNullOrEmpty())
			{
				foreach (var keyValue in rewardDict)
				{
					string itemId = keyValue.Key;
					string curItemId = doerAttrParser.ParseString(itemId);
					string countString = rewardDict[itemId];
					int count = doerAttrParser.ParseInt(countString);
					result[curItemId] = count;
				}
			}

			return result;
		}

	}


}