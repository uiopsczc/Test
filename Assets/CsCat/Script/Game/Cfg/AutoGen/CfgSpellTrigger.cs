//AutoGen. DO NOT EDIT!!!
//ExportFrom JN技能表.xlsx[技能触发表]
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgSpellTrigger {
    protected CfgSpellTrigger () {}
    public static CfgSpellTrigger Instance => instance;
    protected static CfgSpellTrigger instance = new CfgSpellTrigger();
    protected CfgSpellTriggerRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgSpellTriggerRoot>(jsonStr);}
    public List<CfgSpellTriggerData> All(){ return this.root.data_list; }
    public CfgSpellTriggerData Get(int index){ return this.root.data_list[index]; }
    public CfgSpellTriggerData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.index_dict.unique.id.ContainsKey(key);
    }
  }
  public class CfgSpellTriggerRoot{
    public List<CfgSpellTriggerData> data_list { get; set; }
    public CfgSpellTriggerIndexData index_dict { get; set; }
  }
  public partial class CfgSpellTriggerData {
    /*ID*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
    /*触发类型*/
    public string triggerType { get; set; }
    /*触发的技能id*/
    public string triggerSpellId { get; set; }
    /*触发技能延迟时间*/
    public float triggerSpellDelayDuration { get; set; }
    /*检测对象*/
    public string checkTarget { get; set; }
    /*条件类型*/
    public string condition { get; set; }
  }
  public class CfgSpellTriggerIndexData {
    public CfgSpellTriggerIndexUniqueData unique{ get; set; }
  }
  public class CfgSpellTriggerIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
  }
}