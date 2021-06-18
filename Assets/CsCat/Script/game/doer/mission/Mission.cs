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
      return GetMissionFactory().GetCfgMissionData(this.GetId());
    }

    public override void OnInit()
    {
      base.OnInit();
    }

    public override void OnSave(Hashtable dict, Hashtable dict_tmp)
    {
      base.OnSave(dict, dict_tmp);
    }

    public override void OnRestore(Hashtable dict, Hashtable dict_tmp)
    {
      base.OnRestore(dict, dict_tmp);
    }

    //owner 发放任务的npc
    public virtual bool OnAccept(User user)
    {
      string onAccept_doerEvent_id = this.GetCfgMissionData().onAccept_doerEvent_id;
      if (!onAccept_doerEvent_id.IsNullOrWhiteSpace())
      {
        var cfgDoerEventData =CfgDoerEvent.Instance.get_by_id(onAccept_doerEvent_id);
        if (!cfgDoerEventData.is_not_open)
        {
          if (!Client.instance.doerEventFactory.GetDoerEvent(onAccept_doerEvent_id).Execute(
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
      string onFinish_doerEvent_id = this.GetCfgMissionData().onFinish_doerEvent_id;
      if (!onFinish_doerEvent_id.IsNullOrWhiteSpace())
      {
        var cfgDoerEventData =
          CfgDoerEvent.Instance.get_by_id(onFinish_doerEvent_id);
        if (!cfgDoerEventData.is_not_open)
        {
          Client.instance.doerEventFactory.GetDoerEvent(onFinish_doerEvent_id).Execute(
            string.Format("{0} 完成任务 {1}", user.GetShort(), this.GetShort()), this.GetOwner(),
            new DoerAttrParser(user, this, this.GetOwner()));
        }
      }
    }

    //owner 发放任务的npc
    public void OnGiveUp(User user)
    {
      string onGiveUp_doerEvent_id = this.GetCfgMissionData().onGiveUp_doerEvent_id;
      if (!onGiveUp_doerEvent_id.IsNullOrWhiteSpace())
      {
        var cfgDoerEventData =
          CfgDoerEvent.Instance.get_by_id(onGiveUp_doerEvent_id);
        if (!cfgDoerEventData.is_not_open)
        {
          Client.instance.doerEventFactory.GetDoerEvent(onGiveUp_doerEvent_id).Execute(
            string.Format("{0} 放弃任务 {1}", user.GetShort(), this.GetShort()), this.GetOwner(),
            new DoerAttrParser(user, this, this.GetOwner()));
        }
      }
    }

    public virtual bool IsReady()
    {
      if (CheckFinishCondition())
        return true;
      return false;
    }

    public bool CheckFinishCondition()
    {
      string finish_condition = this.GetCfgMissionData().finish_condition;
      if (!finish_condition.IsNullOrWhiteSpace()) // 未设置完成条件的办事任务不能根据派发任务处来完成，只能在设置了可完成任务的时候检测是否就绪
      {
        DoerAttrParser doerAttrParser = new DoerAttrParser(Client.instance.user, this, this.GetOwner());
        if (doerAttrParser.ParseBoolean(finish_condition, false))
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

      Dictionary<string, string> reward_dict = GetCfgMissionData().reward_dict.ToDict<string,string>();
      if (!reward_dict.IsNullOrEmpty())
      {
        foreach (string item_id in reward_dict.Keys)
        {
          string _item_id = doerAttrParser.ParseString(item_id);
          string count_string = reward_dict[item_id];
          int count = doerAttrParser.ParseInt(count_string);
          result[_item_id] = count;
        }
      }

      return result;
    }

  }


}