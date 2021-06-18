namespace CsCat
{
  public class RoleFactory : DoerFactory
  {
    protected override string default_doer_class_path => "CsCat.Role";

    protected override string GetClassPath(string id)
    {
      return this.GetCfgRoleData(id).class_path_cs.IsNullOrWhiteSpace() ? base.GetClassPath(id) : GetCfgRoleData(id).class_path_cs;
    }
    

    public CfgRoleData GetCfgRoleData(string id)
    {
      return CfgRole.Instance.get_by_id(id);
    }

    protected override DBase __NewDBase(string id_or_rid)
    {
      return new RoleDBase(id_or_rid);
    }

  }
}