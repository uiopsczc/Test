namespace CsCat
{
	public class MissionFactory : DoerFactory
	{
		protected override string defaultDoerClassPath => "CsCat.Mission";

		protected override string GetClassPath(string id)
		{
			return this.GetCfgMissionData(id).classPathCs.IsNullOrWhiteSpace() ? base.GetClassPath(id) : GetCfgMissionData(id).classPathCs;
		}


		public CfgMissionData GetCfgMissionData(string id)
		{
			return CfgMission.Instance.GetById(id);
		}

		protected override DBase _NewDBase(string idOrRid)
		{
			return new MissionDBase(idOrRid);
		}
	}
}