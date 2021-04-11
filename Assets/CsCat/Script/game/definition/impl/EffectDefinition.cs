namespace CsCat
{
  public class EffectDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/EffectDefinition"; }
    }

    public EffectDefinition GetData(string id)
    {
      return GetData<EffectDefinition>(id);
    }

    public EffectDefinition GetData(int id)
    {
      return GetData<EffectDefinition>(id);
    }

    public string prefab_path;
    public float duration;
    public string socket_name_1;
    public string socket_name_2;


  }
}