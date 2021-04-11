using System.Collections.Generic;

namespace CsCat
{
  public class TestDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/Test/TestDefinition"; }
    }

    public string country;
    public Dictionary<string, string> age_dict;

    public TestDefinition GetData(string id)
    {
      return GetData<TestDefinition>(id);
    }

    public TestDefinition GetData(int id)
    {
      return GetData<TestDefinition>(id);
    }
  }
}