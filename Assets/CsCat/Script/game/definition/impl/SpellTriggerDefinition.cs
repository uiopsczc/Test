namespace CsCat
{
  public class SpellTriggerDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/SpellTriggerDefinition"; }
    }

    public SpellTriggerDefinition GetData(string id)
    {
      return GetData<SpellTriggerDefinition>(id);
    }

    public SpellTriggerDefinition GetData(int id)
    {
      return GetData<SpellTriggerDefinition>(id);
    }

    public string trigger_type;
    public string trigger_spell_id;
    public float trigger_spell_delay_duration;
    public string check_target;
    public string condition;
  }
}