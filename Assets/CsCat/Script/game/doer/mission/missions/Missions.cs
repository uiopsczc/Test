using System.Collections;

namespace CsCat
{
	public class Missions
	{
		private Doer parent_doer;
		private string sub_doer_key;

		public Missions(Doer parent_doer, string sub_doer_key)
		{
			this.parent_doer = parent_doer;
			this.sub_doer_key = sub_doer_key;
		}

		////////////////////DoXXX/////////////////////////////////
		//卸载
		public void DoRelease()
		{
			SubDoerUtil1.DoReleaseSubDoer<Mission>(this.parent_doer, this.sub_doer_key);
		}

		//保存
		public void DoSave(Hashtable dict, Hashtable dict_tmp, string save_key = null)
		{
			save_key = save_key ?? "missions";
			var missions = this.GetMissions();

			var list_missions = new ArrayList();
			var dict_missions_tmp = new Hashtable();
			foreach (var mission in missions)
			{
				string rid = mission.GetRid();
				var dict_mission = new Hashtable();
				var dict_mission_tmp = new Hashtable();
				mission.PrepareSave(dict_mission, dict_mission_tmp);
				dict_mission["rid"] = rid;
				list_missions.Add(dict_mission);
				if (!dict_mission_tmp.IsNullOrEmpty())
					dict_missions_tmp[rid] = dict_mission_tmp;
			}

			if (!list_missions.IsNullOrEmpty())
				dict[save_key] = list_missions;
			if (!dict_missions_tmp.IsNullOrEmpty())
				dict_tmp[save_key] = dict_missions_tmp;
		}

		//还原
		public void DoRestore(Hashtable dict, Hashtable dict_tmp, string restore_key = null)
		{
			restore_key = restore_key ?? "missions";
			this.ClearMissions();
			var list_missions = dict.Remove3<ArrayList>(restore_key);
			var dict_missions_tmp = dict.Remove3<Hashtable>(restore_key);
			if (!list_missions.IsNullOrEmpty())
			{
				var missions = this.GetMissions_ToEdit();
				foreach (Hashtable dict_mission in list_missions)
				{
					string rid = dict_mission.Remove3<string>("rid");
					var mission = Client.instance.missionFactory.NewDoer(rid) as Mission;
					mission.SetEnv(this.parent_doer);
					Hashtable dict_mission_tmp = null;
					if (dict_missions_tmp != null)
						dict_mission_tmp = dict_missions_tmp[rid] as Hashtable;
					mission.FinishRestore(dict_mission, dict_mission_tmp);
				}
			}
		}
		//////////////////////////OnXXX//////////////////////////////////////////////////


		////////////////////////////////////////////////////////////////////////////
		public Mission[] GetMissions(string id = null)
		{
			return SubDoerUtil1.GetSubDoers<Mission>(this.parent_doer, this.sub_doer_key, id, null);
		}

		public ArrayList GetMissions_ToEdit() //可以直接插入删除
		{
			return SubDoerUtil1.GetSubDoers_ToEdit(this.parent_doer, this.sub_doer_key);
		}

		public bool HasMissions()
		{
			return SubDoerUtil1.HasSubDoers<Mission>(this.parent_doer, this.sub_doer_key);
		}

		public int GetMissionsCount()
		{
			return SubDoerUtil1.GetSubDoersCount<Mission>(this.parent_doer, this.sub_doer_key);
		}

		public Mission GetMission(string id_or_rid)
		{
			return SubDoerUtil1.GetSubDoer<Mission>(this.parent_doer, this.sub_doer_key, id_or_rid);
		}

		public void ClearMissions()
		{
			SubDoerUtil1.ClearSubDoers<Mission>(this.parent_doer, this.sub_doer_key, (mission) => { });
		}
	}
}