namespace CsCat
{
  public class QualityDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/QualityDefinition"; }
    }

    public QualityDefinition GetData(string id)
    {
      return GetData<QualityDefinition>(id);
    }

    public QualityDefinition GetData(int id)
    {
      return GetData<QualityDefinition>(id);
    }

    public string icon_path;



  }
}