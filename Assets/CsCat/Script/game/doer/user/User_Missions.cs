using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public partial class User
  {
    ////////////////////////////Mission////////////////////////
    //获得指定的Mission
    public Mission[] GetMissions(string id = null)
    {
      return this.o_missions.GetMissions(id);
    }

    public bool HasMissions()
    {
      return this.o_missions.HasMissions();
    }

    public bool HasMission(string id)
    {
      return this.o_missions.GetMission(id) != null;
    }

    public int GetMissionsCount()
    {
      return this.o_missions.GetMissionsCount();
    }

    //获得指定的任务
    public Mission GetMission(string id_or_rid)
    {
      return this.o_missions.GetMission(id_or_rid);
    }

    //清除所有任务
    public void ClearMissions()
    {
      this.o_missions.ClearMissions();
    }

    public ArrayList GetFinishedMissionIds()
    {
      return GetOrAdd("finished_mission_ids", () => new ArrayList());
    }

    public void AddFinishedMissionId(string mission_id)
    {
      GetFinishedMissionIds().Add(mission_id);
    }

    public void RemoveFinishedMissionId(string mission_id)
    {
      GetFinishedMissionIds().Remove(mission_id);
    }

    //owner 发放任务的npc
    public bool AcceptMission(Mission mission, Doer owner)
    {
      var org_env = mission.GetEnv();
      if (org_env != null)
      {
        LogCat.LogError(string.Format("{0} still belong to {1}", mission, org_env));
        mission.Destruct();
        return false;
      }

      if (HasMission(mission.GetId()))
      {
        LogCat.LogError(string.Format("duplicate mission id![{0}]", mission));
        mission.Destruct();
        return false;
      }

      var missions = this.o_missions.GetMissions_ToEdit();
      mission.SetEnv(this);
      mission.SetOwner(owner);
      missions.Add(mission);
      if (!mission.OnAccept(this))
      {
        mission.Destruct();
        missions.Remove(mission); //失败，减回去
        return false;
      }

      // 检测完成任务
      this.CheckAutoFinishMissions();

      return true;
    }

    //owner 发放任务的npc
    public void FinishMission(Mission mission, Doer owner)
    {
      if (mission == null)
      {
        LogCat.LogError("mission is null");
        return;
      }

      if (this.GetMission(mission.GetId()) != mission)
      {
        LogCat.LogError(string.Format("{0} not belong to {1}", mission, this));
        return;
      }

      var missions = this.o_missions.GetMissions_ToEdit();
      mission.SetEnv(null);
      mission.SetOwner(owner);
      missions.Remove(mission);
      this.AddFinishedMissionId(mission.GetId());
      mission.OnFinish(this);
      mission.Destruct();
    }

    //owner 发放任务的npc
    public void GiveUpMission(Mission mission, Doer owner)
    {
      if (mission == null)
      {
        LogCat.LogError("mission is null");
        return;
      }

      if (this.GetMission(mission.GetId()) != mission)
      {
        LogCat.LogError(string.Format("{0} not belong to {1}", mission, this));
        return;
      }

      var missions = this.o_missions.GetMissions_ToEdit();
      mission.SetEnv(null);
      mission.SetOwner(owner);
      missions.Remove(mission);
      mission.OnGiveUp(this);
      mission.Destruct();
    }

    public bool CheckAutoFinishMissions()
    {
      Mission[] missions = this.GetMissions();
      List<Mission> to_finish_mission_list = new List<Mission>();
      foreach (var mission in missions)
      {
        if (mission.IsReady())
        {
          if (mission.GetCfgMissionData().is_auto_check_finish)
            to_finish_mission_list.Add(mission);
        }
      }

      foreach (var to_finish_mission in to_finish_mission_list)
        this.FinishMission(to_finish_mission, to_finish_mission.GetOwner());
      return to_finish_mission_list.Count > 0;
    }

    ///////////////////////Util////////////////////////////////
    //owner 发放任务的npc
    public bool AcceptMission(string mission_id, Doer owner)
    {
      var mission = Client.instance.missionFactory.NewDoer(mission_id) as Mission;
      return AcceptMission(mission, owner);
    }

    //owner 发放任务的npc
    public void FinishMission(string mission_id, Doer owner)
    {
      FinishMission(this.GetMission(mission_id), owner);
    }

    //owner 发放任务的npc
    public void GiveUpMission(string mission_id, Doer owner)
    {
      GiveUpMission(this.GetMission(mission_id), owner);
    }
  }
}