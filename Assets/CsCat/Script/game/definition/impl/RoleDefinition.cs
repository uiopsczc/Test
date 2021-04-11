namespace CsCat
{
  public class RoleDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/RoleDefinition"; }
    }

    public RoleDefinition GetData(string id)
    {
      return GetData<RoleDefinition>(id);
    }

    public RoleDefinition GetData(int id)
    {
      return GetData<RoleDefinition>(id);
    }

  }
}


  