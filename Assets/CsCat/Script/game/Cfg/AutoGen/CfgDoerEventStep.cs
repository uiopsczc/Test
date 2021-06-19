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
    private Dictionary<string,string> __set_attr_dict;
    public Dictionary<string,string> _set_attr_dict {
      get{
        if(__set_attr_dict == default(Dictionary<string,string>)) __set_attr_dict = set_attr_dict.To<Dictionary<string,string>>();
        return __set_attr_dict;
      }
    }
    /*增加属性*/
    public LitJson.JsonData add_attr_dict { get; set; }
    private Dictionary<string,string> __add_attr_dict;
    public Dictionary<string,string> _add_attr_dict {
      get{
        if(__add_attr_dict == default(Dictionary<string,string>)) __add_attr_dict = add_attr_dict.To<Dictionary<string,string>>();
        return __add_attr_dict;
      }
    }
    /*增加或者删除物品*/
    public LitJson.JsonData deal_item_dict { get; set; }
    private Dictionary<string,string> __deal_item_dict;
    public Dictionary<string,string> _deal_item_dict {
      get{
        if(__deal_item_dict == default(Dictionary<string,string>)) __deal_item_dict = deal_item_dict.To<Dictionary<string,string>>();
        return __deal_item_dict;
      }
    }
    /*放弃任务ids*/
    public LitJson.JsonData give_up_mission_ids { get; set; }
    private string[] __give_up_mission_ids;
    public string[] _give_up_mission_ids {
      get{
        if(__give_up_mission_ids == default(string[])) __give_up_mission_ids = give_up_mission_ids.To<string[]>();
        return __give_up_mission_ids;
      }
    }
    /*接受任务ids*/
    public LitJson.JsonData accept_mission_ids { get; set; }
    private string[] __accept_mission_ids;
    public string[] _accept_mission_ids {
      get{
        if(__accept_mission_ids == default(string[])) __accept_mission_ids = accept_mission_ids.To<string[]>();
        return __accept_mission_ids;
      }
    }
    /*完成任务ids*/
    public LitJson.JsonData finish_mission_ids { get; set; }
    private string[] __finish_mission_ids;
    public string[] _finish_mission_ids {
      get{
        if(__finish_mission_ids == default(string[])) __finish_mission_ids = finish_mission_ids.To<string[]>();
        return __finish_mission_ids;
      }
    }
    /*增加已完成的任务ids*/
    public LitJson.JsonData add_finished_mission_ids { get; set; }
    private string[] __add_finished_mission_ids;
    public string[] _add_finished_mission_ids {
      get{
        if(__add_finished_mission_ids == default(string[])) __add_finished_mission_ids = add_finished_mission_ids.To<string[]>();
        return __add_finished_mission_ids;
      }
    }
    /*删除已完成的任务ids*/
    public LitJson.JsonData remove_finished_mission_ids { get; set; }
    private string[] __remove_finished_mission_ids;
    public string[] _remove_finished_mission_ids {
      get{
        if(__remove_finished_mission_ids == default(string[])) __remove_finished_mission_ids = remove_finished_mission_ids.To<string[]>();
        return __remove_finished_mission_ids;
      }
    }
  }
  public class CfgDoerEventStepIndexData {
    public CfgDoerEventStepIndexUniqueData unique{ get; set; }
  }
  public class CfgDoerEventStepIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
  }
}