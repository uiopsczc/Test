namespace CsCat
{
  // 翻译
  public class TranslationDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/TranslationDefinition"; }
    }

    public TranslationDefinition GetData(string id)
    {
      return GetData<TranslationDefinition>(id);
    }

    public string english;
  }
}