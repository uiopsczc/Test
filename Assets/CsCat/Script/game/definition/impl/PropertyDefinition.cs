namespace CsCat
{
  public class PropertyDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/PropertyDefinition"; }
    }

    public PropertyDefinition GetData(string id)
    {
      return GetData<PropertyDefinition>(id);
    }

    public PropertyDefinition GetData(int id)
    {
      return GetData<PropertyDefinition>(id);
    }

    public bool is_pct;


  }
}