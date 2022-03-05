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
    private Dictionary<string,string> _setAttrDict;
    public Dictionary<string,string> setAttrDict {
      get{
        if(_setAttrDict == default(Dictionary<string,string>)) _setAttrDict = setAttrDict.To<Dictionary<string,string>>();
        return _setAttrDict;
      }
    }
    /*增加属性*/
    private Dictionary<string,string> _addAttrDict;
    public Dictionary<string,string> addAttrDict {
      get{
        if(_addAttrDict == default(Dictionary<string,string>)) _addAttrDict = addAttrDict.To<Dictionary<string,string>>();
        return _addAttrDict;
      }
    }
    /*增加或者删除物品*/
    private Dictionary<string,string> _dealItemDict;
    public Dictionary<string,string> dealItemDict {
      get{
        if(_dealItemDict == default(Dictionary<string,string>)) _dealItemDict = dealItemDict.To<Dictionary<string,string>>();
        return _dealItemDict;
      }
    }
    /*放弃任务ids*/
    private string[] _giveUpMissionIds;
    public string[] giveUpMissionIds {
      get{
        if(_giveUpMissionIds == default(string[])) _giveUpMissionIds = giveUpMissionIds.To<string[]>();
        return _giveUpMissionIds;
      }
    }
    /*接受任务ids*/
    private string[] _acceptMissionIds;
    public string[] acceptMissionIds {
      get{
        if(_acceptMissionIds == default(string[])) _acceptMissionIds = acceptMissionIds.To<string[]>();
        return _acceptMissionIds;
      }
    }
    /*完成任务ids*/
    private string[] _finishMissionIds;
    public string[] finishMissionIds {
      get{
        if(_finishMissionIds == default(string[])) _finishMissionIds = finishMissionIds.To<string[]>();
        return _finishMissionIds;
      }
    }
    /*增加已完成的任务ids*/
    private string[] _addFinishedMissionIds;
    public string[] addFinishedMissionIds {
      get{
        if(_addFinishedMissionIds == default(string[])) _addFinishedMissionIds = addFinishedMissionIds.To<string[]>();
        return _addFinishedMissionIds;
      }
    }
    /*删除已完成的任务ids*/
    private string[] _removeFinishedMissionIds;
    public string[] removeFinishedMissionIds {
      get{
        if(_removeFinishedMissionIds == default(string[])) _removeFinishedMissionIds = removeFinishedMissionIds.To<string[]>();
        return _removeFinishedMissionIds;
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