namespace CsCat
{
  public class ItemDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/ItemDefinition"; }
    }

    public ItemDefinition GetData(string id)
    {
      return GetData<ItemDefinition>(id);
    }

    public ItemDefinition GetData(int id)
    {
      return GetData<ItemDefinition>(id);
    }

    public string bg_path;
    public string quality_id;
    public string icon_path;

  }
}


