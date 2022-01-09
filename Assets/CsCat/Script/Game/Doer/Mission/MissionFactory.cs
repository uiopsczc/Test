namespace CsCat
{
	public class MissionFactory : DoerFactory
	{
		protected override string defaultDoerClassPath => "CsCat.Mission";

		protected override string GetClassPath(string id)
		{
			return this.GetCfgMissionData(id).class_path_cs.IsNullOrWhiteSpace() ? base.GetClassPath(id) : GetCfgMissionData(id).class_path_cs;
		}


		public CfgMissionData GetCfgMissionData(string id)
		{
			return CfgMission.Instance.get_by_id(id);
		}

		protected override DBase _NewDBase(string idOrRid)
		{
			return new MissionDBase(idOrRid);
		}
	}
}