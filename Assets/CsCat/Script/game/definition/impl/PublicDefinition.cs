using System.Collections.Generic;

namespace CsCat
{
  public class PublicDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/PublicDefinition"; }
    }

    public PublicDefinition GetData(string id)
    {
      return GetData<PublicDefinition>(id);
    }

    public PublicDefinition GetData(int id)
    {
      return GetData<PublicDefinition>(id);
    }

    public string value;
    public Dictionary<string, string> value_dict;
  }
}