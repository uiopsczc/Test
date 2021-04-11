using System.Collections.Generic;

namespace CsCat
{
  public class BuffDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/BuffDefinition"; }
    }

    public BuffDefinition GetData(string id)
    {
      return GetData<BuffDefinition>(id);
    }

    public BuffDefinition GetData(int id)
    {
      return GetData<BuffDefinition>(id);
    }

    public float duration;
    public string[] effect_ids;
    public string state;
    public bool is_unique;
    public string trigger_spell_id;
    public Dictionary<string, string> property_dict;
  }
}