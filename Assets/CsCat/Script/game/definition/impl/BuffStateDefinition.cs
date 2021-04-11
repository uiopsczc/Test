namespace CsCat
{
  public class BuffStateDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/BuffStateDefinition"; }
    }

    public BuffStateDefinition GetData(string id)
    {
      return GetData<BuffStateDefinition>(id);
    }

    public BuffStateDefinition GetData(int id)
    {
      return GetData<BuffStateDefinition>(id);
    }
  }
}