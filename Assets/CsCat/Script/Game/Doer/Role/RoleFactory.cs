namespace CsCat
{
	public class RoleFactory : DoerFactory
	{
		protected override string defaultDoerClassPath => "CsCat.Role";

		protected override string GetClassPath(string id)
		{
			return this.GetCfgRoleData(id).classPathCS.IsNullOrWhiteSpace() ? base.GetClassPath(id) : GetCfgRoleData(id).classPathCS;
		}


		public CfgRoleData GetCfgRoleData(string id)
		{
			return CfgRole.Instance.get_by_id(id);
		}

		protected override DBase _NewDBase(string idOrRid)
		{
			return new RoleDBase(idOrRid);
		}

	}
}