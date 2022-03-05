//AutoGen. DO NOT EDIT!!!
//ExportFrom DDoerEvent表.xlsx[DDoerEvent表]
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgDoerEvent {
    protected CfgDoerEvent () {}
    public static CfgDoerEvent Instance => instance;
    protected static CfgDoerEvent instance = new CfgDoerEvent();
    protected CfgDoerEventRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgDoerEventRoot>(jsonStr);}
    public List<CfgDoerEventData> All(){ return this.root.data_list; }
    public CfgDoerEventData Get(int index){ return this.root.data_list[index]; }
    public CfgDoerEventData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.index_dict.unique.id.ContainsKey(key);
    }
  }
  public class CfgDoerEventRoot{
    public List<CfgDoerEventData> data_list { get; set; }
    public CfgDoerEventIndexData index_dict { get; set; }
  }
  public partial class CfgDoerEventData {
    /*ID*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
    /*是否不打开*/
    public bool isNotOpen { get; set; }
    /*是否不弹窗提示*/
    public bool isNotTalk { get; set; }
    /*触发条件*/
    public string triggerCondition { get; set; }
    /*触发提示*/
    public string triggerDesc { get; set; }
    /*不触发提示*/
    public string canNotTriggerDesc { get; set; }
    /*classPathLua*/
    public string classPathLua { get; set; }
    /*classPathCs*/
    public string classPathCs { get; set; }
    /*子步骤ids*/
    private string[] _stepIds;
    public string[] stepIds {
      get{
        if(_stepIds == default(string[])) _stepIds = stepIds.To<string[]>();
        return _stepIds;
      }
    }
  }
  public class CfgDoerEventIndexData {
    public CfgDoerEventIndexUniqueData unique{ get; set; }
  }
  public class CfgDoerEventIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
  }
}