namespace CsCat
{
	public class RoleFactory : DoerFactory
	{
		protected override string defaultDoerClassPath => "CsCat.Role";

		protected override string GetClassPath(string id)
		{
			return this.GetCfgRoleData(id).classPathCs.IsNullOrWhiteSpace() ? base.GetClassPath(id) : GetCfgRoleData(id).classPathCs;
		}


		public CfgRoleData GetCfgRoleData(string id)
		{
			return CfgRole.Instance.GetById(id);
		}

		protected override DBase _NewDBase(string idOrRid)
		{
			return new RoleDBase(idOrRid);
		}

	}
}