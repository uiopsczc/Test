using System.Collections.Generic;

namespace CsCat
{
  public class SpellDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/SpellDefinition"; }
    }

    public SpellDefinition GetData(string id)
    {
      return GetData<SpellDefinition>(id);
    }

    public SpellDefinition GetData(int id)
    {
      return GetData<SpellDefinition>(id);
    }

    public bool is_can_move_while_cast;
    public float range;
    public float damage_factor;
    public string type;
    public string target_type;
    public string damage_type;
    public string cast_type;
    public string action_name;
    public string animation_name;
    public float animation_duration;
    public string[] hand_effect_ids;
    public string[] go_effect_ids;
    public string[] hit_effect_ids;
    public string[] ground_effect_ids;
    public string[] line_effect_ids;
    public bool is_not_face_to_target;
    public float cast_time;
    public float break_time;
    public string[] new_spell_trigger_ids;
    public bool is_need_target;
    public float cooldown_duration;
    public string[] passive_buff_ids;
    public Dictionary<string, string> select_unit_arg_dict;
    public Dictionary<string, string> arg_dict;
  }
}