//AutoGen. DO NOT EDIT!!!
//ExportFrom JN技能表.xlsx[技能]
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
    public CfgSpellData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool IsContainsKeyById(string id){
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
    public string classPathLua { get; set; }
    /*C#实现类*/
    public string classPathCs { get; set; }
    /*释放技能时可以操作移动*/
    public bool isCanMoveWhileCast { get; set; }
    /*攻击范围*/
    public float range { get; set; }
    /*伤害加成系数*/
    public float damageFactor { get; set; }
    /*类型*/
    public string type { get; set; }
    /*目标类型*/
    public string targetType { get; set; }
    /*伤害类型*/
    public string damageType { get; set; }
    /*施法类型(正常/触发)*/
    public string castType { get; set; }
    /*actionName*/
    public string actionName { get; set; }
    /*动画名*/
    public string animationName { get; set; }
    /*动画时间*/
    public float animationDuration { get; set; }
    /*起手特效ids*/
    public LitJson.JsonData handEffectIds { get; set; }
    private string[] __handEffectIds;
    public string[] _handEffectIds {
      get{
        if(__handEffectIds == default(string[])) __handEffectIds = handEffectIds.To<string[]>();
        return __handEffectIds;
      }
    }
    /*出手特效ids*/
    public LitJson.JsonData goEffectIds { get; set; }
    private string[] __goEffectIds;
    public string[] _goEffectIds {
      get{
        if(__goEffectIds == default(string[])) __goEffectIds = goEffectIds.To<string[]>();
        return __goEffectIds;
      }
    }
    /*击中特效ids*/
    public LitJson.JsonData hitEffectIds { get; set; }
    private string[] __hitEffectIds;
    public string[] _hitEffectIds {
      get{
        if(__hitEffectIds == default(string[])) __hitEffectIds = hitEffectIds.To<string[]>();
        return __hitEffectIds;
      }
    }
    /*地面特效ids*/
    public LitJson.JsonData groundEffectIds { get; set; }
    private string[] __groundEffectIds;
    public string[] _groundEffectIds {
      get{
        if(__groundEffectIds == default(string[])) __groundEffectIds = groundEffectIds.To<string[]>();
        return __groundEffectIds;
      }
    }
    /*line特效ids*/
    public LitJson.JsonData lineEffectIds { get; set; }
    private string[] __lineEffectIds;
    public string[] _lineEffectIds {
      get{
        if(__lineEffectIds == default(string[])) __lineEffectIds = lineEffectIds.To<string[]>();
        return __lineEffectIds;
      }
    }
    /*不强制面向攻击目标*/
    public bool isNotFaceToTarget { get; set; }
    /*起手前摇时间*/
    public float castTime { get; set; }
    /*可打断后摇时间*/
    public float breakTime { get; set; }
    /*新技能触发id*/
    public LitJson.JsonData newSpellTriggerIds { get; set; }
    private string[] __newSpellTriggerIds;
    public string[] _newSpellTriggerIds {
      get{
        if(__newSpellTriggerIds == default(string[])) __newSpellTriggerIds = newSpellTriggerIds.To<string[]>();
        return __newSpellTriggerIds;
      }
    }
    /*是否需要目标*/
    public bool isNeedTarget { get; set; }
    /*冷却CD*/
    public float cooldownDuration { get; set; }
    /*被动技能ids*/
    public LitJson.JsonData passiveBuffIds { get; set; }
    private string[] __passiveBuffIds;
    public string[] _passiveBuffIds {
      get{
        if(__passiveBuffIds == default(string[])) __passiveBuffIds = passiveBuffIds.To<string[]>();
        return __passiveBuffIds;
      }
    }
    /*选择单位参数*/
    public LitJson.JsonData selectUnitArgDict { get; set; }
    private Dictionary<string,string> __selectUnitArgDict;
    public Dictionary<string,string> _selectUnitArgDict {
      get{
        if(__selectUnitArgDict == default(Dictionary<string,string>)) __selectUnitArgDict = selectUnitArgDict.To<Dictionary<string,string>>();
        return __selectUnitArgDict;
      }
    }
    /*参数*/
    public LitJson.JsonData argDict { get; set; }
    private Dictionary<string,string> __argDict;
    public Dictionary<string,string> _argDict {
      get{
        if(__argDict == default(Dictionary<string,string>)) __argDict = argDict.To<Dictionary<string,string>>();
        return __argDict;
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