namespace CsCat
{
  public class RoleFactory : DoerFactory
  {
    protected override string default_doer_class_path
    {
      get { return "CsCat.Role"; }
    }

    public override ExcelAssetBase GetDefinitions()
    {
      return DefinitionManager.instance.roleDefinition;
    }

    public override ExcelAssetBase GetDefinition(string id)
    {
      return GetDefinitions().GetData<RoleDefinition>(id);
    }

    public RoleDefinition GetRoleDefinition(string id)
    {
      return GetDefinition(id) as RoleDefinition;
    }

    protected override DBase __NewDBase(string id_or_rid)
    {
      return new RoleDBase(id_or_rid);
    }

  }
}