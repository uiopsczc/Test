namespace CsCat
{
  public class UnitDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/UnitDefinition"; }
    }

    public UnitDefinition GetData(string id)
    {
      return GetData<UnitDefinition>(id);
    }

    public UnitDefinition GetData(int id)
    {
      return GetData<UnitDefinition>(id);
    }

    public string type;
    public float offset_y;
    public float radius;
    public float scale;
    public float walk_step_length;
    public string model_path;
    public string[] normal_attack_ids;
    public string[] skill_ids;
    public bool is_keep_dead_body;
    public float dead_body_dealy;
    public string death_effect_id;
    public string[] passive_buff_ids;
    public string ai_class_path_cs;

  }
}