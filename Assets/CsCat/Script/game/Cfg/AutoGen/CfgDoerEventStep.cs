//AutoGen. DO NOT EDIT!!!
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgDoerEventStep {
    protected CfgDoerEventStep () {}
    public static CfgDoerEventStep Instance => instance;
    protected static CfgDoerEventStep instance = new CfgDoerEventStep();
    protected CfgDoerEventStepRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgDoerEventStepRoot>(jsonStr);}
    public List<CfgDoerEventStepData> All(){ return this.root.data_list; }
    public CfgDoerEventStepData Get(int index){ return this.root.data_list[index]; }
    public CfgDoerEventStepData get_by_id(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool contain_key_by_id(string id){
      string key = id.ToString();
      return this.root.index_dict.unique.id.ContainsKey(key);
    }
  }
  public class CfgDoerEventStepRoot{
    public List<CfgDoerEventStepData> data_list { get; set; }
    public CfgDoerEventStepIndexData index_dict { get; set; }
  }
  public partial class CfgDoerEventStepData {
    /*ID*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
    /*触发条件*/
    public string trigger_condition { get; set; }
    /*触发提示*/
    public string trigger_desc { get; set; }
    /*不触发提示*/
    public string can_not_trigger_desc { get; set; }
    /*执行条件*/
    public string execute_condition { get; set; }
    /*执行提示*/
    public string execute_desc { get; set; }
    /*不执行提示*/
    public string can_not_execute_desc { get; set; }
    /*是否在此处停止*/
    public bool is_stop_here { get; set; }
    /*设置属性*/
    public LitJson.JsonData set_attr_dict { get; set; }
    /*增加属性*/
    public LitJson.JsonData add_attr_dict { get; set; }
    /*增加或者删除物品*/
    public LitJson.JsonData deal_item_dict { get; set; }
    /*放弃任务ids*/
    public LitJson.JsonData give_up_mission_ids { get; set; }
    /*接受任务ids*/
    public LitJson.JsonData accept_mission_ids { get; set; }
    /*完成任务ids*/
    public LitJson.JsonData finish_mission_ids { get; set; }
    /*增加已完成的任务ids*/
    public LitJson.JsonData add_finished_mission_ids { get; set; }
    /*删除已完成的任务ids*/
    public LitJson.JsonData remove_finished_mission_ids { get; set; }
  }
  public class CfgDoerEventStepIndexData {
    public CfgDoerEventStepIndexUniqueData unique{ get; set; }
  }
  public class CfgDoerEventStepIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
  }
}