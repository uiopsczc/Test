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
    public List<CfgSpellData> All(){ return this.root.dataList; }
    public CfgSpellData Get(int index){ return this.root.dataList[index]; }
    public CfgSpellData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.indexDict.uniqueIndexesList.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.indexDict.uniqueIndexesList.id.ContainsKey(key);
    }
  }
  public class CfgSpellRoot{
    public List<CfgSpellData> dataList { get; set; }
    public CfgSpellIndexData indexDict { get; set; }
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
    private string[] _handEffectIds;
    public string[] handEffectIds {
      set{ _handEffectIds = value; }
      get{ return _handEffectIds; }
    }
    /*出手特效ids*/
    private string[] _goEffectIds;
    public string[] goEffectIds {
      set{ _goEffectIds = value; }
      get{ return _goEffectIds; }
    }
    /*击中特效ids*/
    private string[] _hitEffectIds;
    public string[] hitEffectIds {
      set{ _hitEffectIds = value; }
      get{ return _hitEffectIds; }
    }
    /*地面特效ids*/
    private string[] _groundEffectIds;
    public string[] groundEffectIds {
      set{ _groundEffectIds = value; }
      get{ return _groundEffectIds; }
    }
    /*line特效ids*/
    private string[] _lineEffectIds;
    public string[] lineEffectIds {
      set{ _lineEffectIds = value; }
      get{ return _lineEffectIds; }
    }
    /*不强制面向攻击目标*/
    public bool isNotFaceToTarget { get; set; }
    /*起手前摇时间*/
    public float castTime { get; set; }
    /*可打断后摇时间*/
    public float breakTime { get; set; }
    /*新技能触发id*/
    private string[] _newSpellTriggerIds;
    public string[] newSpellTriggerIds {
      set{ _newSpellTriggerIds = value; }
      get{ return _newSpellTriggerIds; }
    }
    /*是否需要目标*/
    public bool isNeedTarget { get; set; }
    /*冷却CD*/
    public float cooldownDuration { get; set; }
    /*被动技能ids*/
    private string[] _passiveBuffIds;
    public string[] passiveBuffIds {
      set{ _passiveBuffIds = value; }
      get{ return _passiveBuffIds; }
    }
    /*选择单位参数*/
    private Dictionary<string,string> _selectUnitArgDict;
    public Dictionary<string,string> selectUnitArgDict {
      set{ _selectUnitArgDict = value; }
      get{ return _selectUnitArgDict; }
    }
    /*参数*/
    private Dictionary<string,string> _argDict;
    public Dictionary<string,string> argDict {
      set{ _argDict = value; }
      get{ return _argDict; }
    }
  }
  public class CfgSpellIndexData {
    public CfgSpellIndexUniqueIndexesListData uniqueIndexesList{ get; set; }
  }
  public class CfgSpellIndexUniqueIndexesListData {
    public Dictionary<string, int> id { get; set; } 
  }
}