//AutoGen. DO NOT EDIT!!!
//ExportFrom DDoerEvent表.xlsx[DoerEvent子步骤表]
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
    public CfgDoerEventStepData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool IsContainsKeyById(string id){
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
    public string triggerCondition { get; set; }
    /*触发提示*/
    public string triggerDesc { get; set; }
    /*不触发提示*/
    public string isCanNotTriggerDesc { get; set; }
    /*执行条件*/
    public string executeCondition { get; set; }
    /*执行提示*/
    public string executeDesc { get; set; }
    /*不执行提示*/
    public string isCanNotExecuteDesc { get; set; }
    /*是否在此处停止*/
    public bool isStopHere { get; set; }
    /*设置属性*/
    public LitJson.JsonData setAttrDict { get; set; }
    private Dictionary<string,string> __setAttrDict;
    public Dictionary<string,string> _setAttrDict {
      get{
        if(__setAttrDict == default(Dictionary<string,string>)) __setAttrDict = setAttrDict.To<Dictionary<string,string>>();
        return __setAttrDict;
      }
    }
    /*增加属性*/
    public LitJson.JsonData addAttrDict { get; set; }
    private Dictionary<string,string> __addAttrDict;
    public Dictionary<string,string> _addAttrDict {
      get{
        if(__addAttrDict == default(Dictionary<string,string>)) __addAttrDict = addAttrDict.To<Dictionary<string,string>>();
        return __addAttrDict;
      }
    }
    /*增加或者删除物品*/
    public LitJson.JsonData dealItemDict { get; set; }
    private Dictionary<string,string> __dealItemDict;
    public Dictionary<string,string> _dealItemDict {
      get{
        if(__dealItemDict == default(Dictionary<string,string>)) __dealItemDict = dealItemDict.To<Dictionary<string,string>>();
        return __dealItemDict;
      }
    }
    /*放弃任务ids*/
    public LitJson.JsonData giveUpMissionIds { get; set; }
    private string[] __giveUpMissionIds;
    public string[] _giveUpMissionIds {
      get{
        if(__giveUpMissionIds == default(string[])) __giveUpMissionIds = giveUpMissionIds.To<string[]>();
        return __giveUpMissionIds;
      }
    }
    /*接受任务ids*/
    public LitJson.JsonData acceptMissionIds { get; set; }
    private string[] __acceptMissionIds;
    public string[] _acceptMissionIds {
      get{
        if(__acceptMissionIds == default(string[])) __acceptMissionIds = acceptMissionIds.To<string[]>();
        return __acceptMissionIds;
      }
    }
    /*完成任务ids*/
    public LitJson.JsonData finishMissionIds { get; set; }
    private string[] __finishMissionIds;
    public string[] _finishMissionIds {
      get{
        if(__finishMissionIds == default(string[])) __finishMissionIds = finishMissionIds.To<string[]>();
        return __finishMissionIds;
      }
    }
    /*增加已完成的任务ids*/
    public LitJson.JsonData addFinishedMissionIds { get; set; }
    private string[] __addFinishedMissionIds;
    public string[] _addFinishedMissionIds {
      get{
        if(__addFinishedMissionIds == default(string[])) __addFinishedMissionIds = addFinishedMissionIds.To<string[]>();
        return __addFinishedMissionIds;
      }
    }
    /*删除已完成的任务ids*/
    public LitJson.JsonData removeFinishedMissionIds { get; set; }
    private string[] __removeFinishedMissionIds;
    public string[] _removeFinishedMissionIds {
      get{
        if(__removeFinishedMissionIds == default(string[])) __removeFinishedMissionIds = removeFinishedMissionIds.To<string[]>();
        return __removeFinishedMissionIds;
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