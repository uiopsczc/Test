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
      return GetDoerEventFactory().GetCfgDoerEventData(this.GetId());
    }

    //////////////////////////////////////////////////////////////////////////
    //owner 发放任务的npc
    public virtual bool Execute(string desc, Doer owner, DoerAttrParser doerAttrParser)
    {
      if (!CheckDoerEventTriggerCondition(doerAttrParser))
        return false;
      var cfgDoerEventData = GetCfgDoerEventData();
      bool is_not_talk = cfgDoerEventData.is_not_talk; // 不弹出talk
      //通用情况
      string trigger_desc = cfgDoerEventData.trigger_desc;
      List<string> word_list = new List<string>();
      if (!trigger_desc.IsNullOrWhiteSpace())
        word_list.Add(doerAttrParser.ParseString(trigger_desc));
      int ok = 1; // 0-触发条件失败，1-触发成功，执行失败，2-触发成功，执行成功
      string[] step_ids = cfgDoerEventData.step_ids.ToArray<string>();
      if (!step_ids.IsNullOrEmpty())
      {
        for (int i = 0; i < step_ids.Length; i++)
        {
          string step_id = step_ids[i];
          var cfgDoerEventStep = CfgDoerEventStep.Instance.get_by_id(step_id);
          ok = ExecuteStep(desc + "步骤" + (i + 1), step_id, owner, doerAttrParser, word_list);
          if (ok == 0)
            break;
          else if (ok == 2 && cfgDoerEventStep.is_stop_here)
            break;
        }
      }

      if (!is_not_talk && word_list.Count > 0)
      {
        foreach (var word in word_list)
          Client.instance.uiManager.Notify(word);
      }

      return ok > 0;
    }

    public bool CheckDoerEventTriggerCondition(DoerAttrParser doerAttrParser)
    {
      var cfgDoerEventData = CfgDoerEvent.Instance.get_by_id(this.GetId());
      bool is_not_talk = cfgDoerEventData.is_not_talk; // 不弹出talk
      string trigger_condition = cfgDoerEventData.trigger_condition; // 触发条件
      if (!trigger_condition.IsNullOrWhiteSpace() && !doerAttrParser.ParseBoolean(trigger_condition)) //不满足触发的情况
      {
        if (!is_not_talk)
        {
          string can_not_trigger_desc = cfgDoerEventData.can_not_trigger_desc;
          if (can_not_trigger_desc.IsNullOrWhiteSpace())
            Client.instance.uiManager.Notify(Translation.GetText("现在不能触发此操作"));
          else
            Client.instance.uiManager.Notify(doerAttrParser.ParseString(can_not_trigger_desc));
        }

        return false;
      }

      return true;
    }

    //owner 发放任务的npc
    public int ExecuteStep(string desc, string doerEventStep_id, Doer owner, DoerAttrParser doerAttrParser,
      List<string> word_list)
    {
      var cfgDoerEventStepData = CfgDoerEventStep.Instance.get_by_id(doerEventStep_id);
      string trigger_condition = cfgDoerEventStepData.trigger_condition; // 触发条件
      if (!trigger_condition.IsNullOrWhiteSpace() && !doerAttrParser.ParseBoolean(trigger_condition)) //不满足触发的情况
      {
        string can_not_trigger_desc = cfgDoerEventStepData.can_not_trigger_desc;
        if (can_not_trigger_desc.IsNullOrWhiteSpace())
          word_list.Add(Translation.GetText("现在不能触发此操作"));
        else
          word_list.Add(doerAttrParser.ParseString(can_not_trigger_desc));
        return 0;
      }

      string trigger_desc = cfgDoerEventStepData.trigger_desc; // 触发提示语
      if (!trigger_desc.IsNullOrWhiteSpace())
        word_list.Add(doerAttrParser.ParseString(trigger_desc));
      string execute_condition = cfgDoerEventStepData.execute_condition; // 执行条件
      if (!execute_condition.IsNullOrWhiteSpace() && !doerAttrParser.ParseBoolean(execute_condition)) //不满足执行条件的情况
      {
        string can_not_execute_desc = cfgDoerEventStepData.can_not_execute_desc; // 不执行提示语
        if (!can_not_execute_desc.IsNullOrWhiteSpace())
          word_list.Add(doerAttrParser.ParseString(can_not_execute_desc));
        return 1;
      }

      string execute_desc = cfgDoerEventStepData.execute_desc; // 执行提示语
      if (!execute_desc.IsNullOrWhiteSpace())
        word_list.Add(doerAttrParser.ParseString(execute_desc));

      DoerAttrSetter doerAttrSetter = new DoerAttrSetter(desc, doerAttrParser);
      //设置属性、更改属性
      Dictionary<string, string> set_attr_dict = cfgDoerEventStepData.set_attr_dict.ToDict<string,string>();
      foreach (var attr_name in set_attr_dict.Keys)
        doerAttrSetter.Set(attr_name, set_attr_dict[attr_name], false);
      Dictionary<string, string> add_attr_dict = cfgDoerEventStepData.add_attr_dict.ToDict<string,string>();
      foreach (var attr_name in add_attr_dict.Keys)
        doerAttrSetter.Set(attr_name, add_attr_dict[attr_name], true);

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
      Dictionary<string, string> deal_item_dict = cfgDoerEventStepData.deal_item_dict.ToDict<string,string>();
      if (!deal_item_dict.IsNullOrEmpty())
        user.DealItems(deal_item_dict, doerAttrParser);

      // 接受任务
      string[] accept_mission_ids = cfgDoerEventStepData.accept_mission_ids.ToArray<string>();
      foreach (var accept_mission_id in accept_mission_ids)
        user.AcceptMission(accept_mission_id, owner);

      // 完成任务
      string[] finish_mission_ids = cfgDoerEventStepData.finish_mission_ids.ToArray<string>();
      foreach (var finish_mission_id in finish_mission_ids)
        user.FinishMission(finish_mission_id, owner);

      // 放弃任务
      string[] give_up_mission_ids = cfgDoerEventStepData.give_up_mission_ids.ToArray<string>();
      foreach (var give_up_mission_id in give_up_mission_ids)
        user.GiveUpMission(give_up_mission_id, owner);

      // 添加已完成任务
      string[] add_finished_mission_ids = cfgDoerEventStepData.add_finished_mission_ids.ToArray<string>();
      foreach (var add_finished_mission_id in add_finished_mission_ids)
        user.AddFinishedMissionId(add_finished_mission_id);

      // 删除已完成任务
      string[] remove_finished_mission_ids = cfgDoerEventStepData.remove_finished_mission_ids.ToArray<string>();
      foreach (var remove_finished_mission_id in remove_finished_mission_ids)
        user.RemoveFinishedMissionId(remove_finished_mission_id);

      // 检测完成任务
      user.CheckAutoFinishMissions();
      return 2;
    }
  }
}