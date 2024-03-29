using System.Collections;

namespace CsCat
{
	public class Missions
	{
		private readonly Doer _parentDoer;
		private readonly string _subDoerKey;

		public Missions(Doer parentDoer, string subDoerKey)
		{
			this._parentDoer = parentDoer;
			this._subDoerKey = subDoerKey;
		}

		////////////////////DoXXX/////////////////////////////////
		//卸载
		public void DoRelease()
		{
			SubDoerUtil1.DoReleaseSubDoer<Mission>(this._parentDoer, this._subDoerKey);
		}

		//保存
		public void DoSave(Hashtable dict, Hashtable dictTmp, string saveKey = null)
		{
			saveKey = saveKey ?? "missions";
			var missions = this.GetMissions();

			var listMissions = new ArrayList();
			var dictMissionsTmp = new Hashtable();
			for (var i = 0; i < missions.Length; i++)
			{
				var mission = missions[i];
				string rid = mission.GetRid();
				var dictMission = new Hashtable();
				var dictMissionTmp = new Hashtable();
				mission.PrepareSave(dictMission, dictMissionTmp);
				dictMission["rid"] = rid;
				listMissions.Add(dictMission);
				if (!dictMissionTmp.IsNullOrEmpty())
					dictMissionsTmp[rid] = dictMissionTmp;
			}

			if (!listMissions.IsNullOrEmpty())
				dict[saveKey] = listMissions;
			if (!dictMissionsTmp.IsNullOrEmpty())
				dictTmp[saveKey] = dictMissionsTmp;
		}

		//还原
		public void DoRestore(Hashtable dict, Hashtable dictTmp, string restoreKey = null)
		{
			restoreKey = restoreKey ?? "missions";
			this.ClearMissions();
			var listMissions = dict.Remove3<ArrayList>(restoreKey);
			var dictMissionsTmp = dict.Remove3<Hashtable>(restoreKey);
			if (!listMissions.IsNullOrEmpty())
			{
				var missions = this.GetMissions_ToEdit();
				for (var i = 0; i < listMissions.Count; i++)
				{
					var dictMission = (Hashtable) listMissions[i];
					string rid = dictMission.Remove3<string>("rid");
					var mission = Client.instance.missionFactory.NewDoer(rid) as Mission;
					mission.SetEnv(this._parentDoer);
					Hashtable dictMissionTmp = null;
					if (dictMissionsTmp != null)
						dictMissionTmp = dictMissionsTmp[rid] as Hashtable;
					mission.FinishRestore(dictMission, dictMissionTmp);
				}
			}
		}
		//////////////////////////OnXXX//////////////////////////////////////////////////


		////////////////////////////////////////////////////////////////////////////
		public Mission[] GetMissions(string id = null)
		{
			return SubDoerUtil1.GetSubDoers<Mission>(this._parentDoer, this._subDoerKey, id, null);
		}

		public ArrayList GetMissions_ToEdit() //可以直接插入删除
		{
			return SubDoerUtil1.GetSubDoers_ToEdit(this._parentDoer, this._subDoerKey);
		}

		public bool IsHasMissions()
		{
			return SubDoerUtil1.HasSubDoers<Mission>(this._parentDoer, this._subDoerKey);
		}

		public int GetMissionsCount()
		{
			return SubDoerUtil1.GetSubDoersCount<Mission>(this._parentDoer, this._subDoerKey);
		}

		public Mission GetMission(string idOrRid)
		{
			return SubDoerUtil1.GetSubDoer<Mission>(this._parentDoer, this._subDoerKey, idOrRid);
		}

		public void ClearMissions()
		{
			SubDoerUtil1.ClearSubDoers<Mission>(this._parentDoer, this._subDoerKey, (mission) => { });
		}
	}
}