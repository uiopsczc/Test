//AutoGen. DO NOT EDIT!!!
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgSpell {
    protected CfgSpell () {}
    public static CfgSpell Instance => instance;
    protected static CfgSpell instance = new CfgSpell();
    protected CfgSpellRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgSpellRoot>(jsonStr);}
    public List<CfgSpellData> All(){ return this.root.data_list; }
    public CfgSpellData Get(int index){ return this.root.data_list[index]; }
    public CfgSpellData get_by_id(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool contain_key_by_id(string id){
      string key = id.ToString();
      return this.root.index_dict.unique.id.ContainsKey(key);
    }
  }
  public class CfgSpellRoot{
    public List<CfgSpellData> data_list { get; set; }
    public CfgSpellIndexData index_dict { get; set; }
  }
  public partial class CfgSpellData {
    /*ID*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
    /*lua实现类*/
    public string class_path_lua { get; set; }
    /*C#实现类*/
    public string class_path_cs { get; set; }
    /*释放技能时可以操作移动*/
    public bool is_can_move_while_cast { get; set; }
    /*攻击范围*/
    public float range { get; set; }
    /*伤害加成系数*/
    public float damage_factor { get; set; }
    /*类型*/
    public string type { get; set; }
    /*目标类型*/
    public string target_type { get; set; }
    /*伤害类型*/
    public string damage_type { get; set; }
    /*施法类型(正常/触发)*/
    public string cast_type { get; set; }
    /*action_name*/
    public string action_name { get; set; }
    /*动画名*/
    public string animation_name { get; set; }
    /*动画时间*/
    public float animation_duration { get; set; }
    /*起手特效ids*/
    public LitJson.JsonData hand_effect_ids { get; set; }
    private string[] __hand_effect_ids;
    public string[] _hand_effect_ids {
      get{
        if(__hand_effect_ids == default(string[])) __hand_effect_ids = hand_effect_ids.To<string[]>();
        return __hand_effect_ids;
      }
    }
    /*出手特效ids*/
    public LitJson.JsonData go_effect_ids { get; set; }
    private string[] __go_effect_ids;
    public string[] _go_effect_ids {
      get{
        if(__go_effect_ids == default(string[])) __go_effect_ids = go_effect_ids.To<string[]>();
        return __go_effect_ids;
      }
    }
    /*击中特效ids*/
    public LitJson.JsonData hit_effect_ids { get; set; }
    private string[] __hit_effect_ids;
    public string[] _hit_effect_ids {
      get{
        if(__hit_effect_ids == default(string[])) __hit_effect_ids = hit_effect_ids.To<string[]>();
        return __hit_effect_ids;
      }
    }
    /*地面特效ids*/
    public LitJson.JsonData ground_effect_ids { get; set; }
    private string[] __ground_effect_ids;
    public string[] _ground_effect_ids {
      get{
        if(__ground_effect_ids == default(string[])) __ground_effect_ids = ground_effect_ids.To<string[]>();
        return __ground_effect_ids;
      }
    }
    /*line特效ids*/
    public LitJson.JsonData line_effect_ids { get; set; }
    private string[] __line_effect_ids;
    public string[] _line_effect_ids {
      get{
        if(__line_effect_ids == default(string[])) __line_effect_ids = line_effect_ids.To<string[]>();
        return __line_effect_ids;
      }
    }
    /*不强制面向攻击目标*/
    public bool is_not_face_to_target { get; set; }
    /*起手前摇时间*/
    public float cast_time { get; set; }
    /*可打断后摇时间*/
    public float break_time { get; set; }
    /*新技能触发id*/
    public LitJson.JsonData new_spell_trigger_ids { get; set; }
    private string[] __new_spell_trigger_ids;
    public string[] _new_spell_trigger_ids {
      get{
        if(__new_spell_trigger_ids == default(string[])) __new_spell_trigger_ids = new_spell_trigger_ids.To<string[]>();
        return __new_spell_trigger_ids;
      }
    }
    /*是否需要目标*/
    public bool is_need_target { get; set; }
    /*冷却CD*/
    public float cooldown_duration { get; set; }
    /*被动技能ids*/
    public LitJson.JsonData passive_buff_ids { get; set; }
    private string[] __passive_buff_ids;
    public string[] _passive_buff_ids {
      get{
        if(__passive_buff_ids == default(string[])) __passive_buff_ids = passive_buff_ids.To<string[]>();
        return __passive_buff_ids;
      }
    }
    /*选择单位参数*/
    public LitJson.JsonData select_unit_arg_dict { get; set; }
    private Dictionary<string,string> __select_unit_arg_dict;
    public Dictionary<string,string> _select_unit_arg_dict {
      get{
        if(__select_unit_arg_dict == default(Dictionary<string,string>)) __select_unit_arg_dict = select_unit_arg_dict.To<Dictionary<string,string>>();
        return __select_unit_arg_dict;
      }
    }
    /*参数*/
    public LitJson.JsonData arg_dict { get; set; }
    private Dictionary<string,string> __arg_dict;
    public Dictionary<string,string> _arg_dict {
      get{
        if(__arg_dict == default(Dictionary<string,string>)) __arg_dict = arg_dict.To<Dictionary<string,string>>();
        return __arg_dict;
      }
    }
  }
  public class CfgSpellIndexData {
    public CfgSpellIndexUniqueData unique{ get; set; }
  }
  public class CfgSpellIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
  }
}