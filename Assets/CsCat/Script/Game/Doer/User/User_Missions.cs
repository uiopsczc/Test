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
			return this.oMissions.GetMissions(id);
		}

		public bool IsHasMissions()
		{
			return this.oMissions.IsHasMissions();
		}

		public bool IsHasMission(string id)
		{
			return this.oMissions.GetMission(id) != null;
		}

		public int GetMissionsCount()
		{
			return this.oMissions.GetMissionsCount();
		}

		//获得指定的任务
		public Mission GetMission(string idOrRid)
		{
			return this.oMissions.GetMission(idOrRid);
		}

		//清除所有任务
		public void ClearMissions()
		{
			this.oMissions.ClearMissions();
		}

		public ArrayList GetFinishedMissionIds()
		{
			return GetOrAdd("finishedMissionIds", () => new ArrayList());
		}

		public void AddFinishedMissionId(string missionId)
		{
			GetFinishedMissionIds().Add(missionId);
		}

		public void RemoveFinishedMissionId(string missionId)
		{
			GetFinishedMissionIds().Remove(missionId);
		}

		//owner 发放任务的npc
		public bool AcceptMission(Mission mission, Doer owner)
		{
			var orgEnv = mission.GetEnv();
			if (orgEnv != null)
			{
				LogCat.LogError(string.Format("{0} still belong to {1}", mission, orgEnv));
				mission.Destruct();
				return false;
			}

			if (IsHasMission(mission.GetId()))
			{
				LogCat.LogError(string.Format("duplicate mission id![{0}]", mission));
				mission.Destruct();
				return false;
			}

			var missions = this.oMissions.GetMissions_ToEdit();
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

			var missions = this.oMissions.GetMissions_ToEdit();
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

			var missions = this.oMissions.GetMissions_ToEdit();
			mission.SetEnv(null);
			mission.SetOwner(owner);
			missions.Remove(mission);
			mission.OnGiveUp(this);
			mission.Destruct();
		}

		public bool CheckAutoFinishMissions()
		{
			Mission[] missions = this.GetMissions();
			List<Mission> toFinishMissionList = new List<Mission>();
			for (var i = 0; i < missions.Length; i++)
			{
				var mission = missions[i];
				if (mission.IsReady())
				{
					if (mission.GetCfgMissionData().isAutoCheckFinish)
						toFinishMissionList.Add(mission);
				}
			}

			for (var i = 0; i < toFinishMissionList.Count; i++)
			{
				var toFinishMission = toFinishMissionList[i];
				this.FinishMission(toFinishMission, toFinishMission.GetOwner());
			}

			return toFinishMissionList.Count > 0;
		}

		///////////////////////Util////////////////////////////////
		//owner 发放任务的npc
		public bool AcceptMission(string missionId, Doer owner)
		{
			var mission = Client.instance.missionFactory.NewDoer(missionId) as Mission;
			return AcceptMission(mission, owner);
		}

		//owner 发放任务的npc
		public void FinishMission(string missionId, Doer owner)
		{
			FinishMission(this.GetMission(missionId), owner);
		}

		//owner 发放任务的npc
		public void GiveUpMission(string missionId, Doer owner)
		{
			GiveUpMission(this.GetMission(missionId), owner);
		}
	}
}