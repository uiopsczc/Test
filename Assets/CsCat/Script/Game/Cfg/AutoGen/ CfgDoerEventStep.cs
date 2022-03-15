//AutoGen. DO NOT EDIT!!!
//ExportFrom DDoerEvent表.xlsx[DoerEvent子步骤表]
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class  CfgDoerEventStep {
    protected  CfgDoerEventStep () {}
    public static  CfgDoerEventStep Instance => instance;
    protected static  CfgDoerEventStep instance = new  CfgDoerEventStep();
    protected  CfgDoerEventStepRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject< CfgDoerEventStepRoot>(jsonStr);}
    public List< CfgDoerEventStepData> All(){ return this.root.dataList; }
    public  CfgDoerEventStepData Get(int index){ return this.root.dataList[index]; }
    public  CfgDoerEventStepData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.indexDict.uniqueIndexesList.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.indexDict.uniqueIndexesList.id.ContainsKey(key);
    }
  }
  public class  CfgDoerEventStepRoot{
    public List< CfgDoerEventStepData> dataList { get; set; }
    public  CfgDoerEventStepIndexData indexDict { get; set; }
  }
  public partial class  CfgDoerEventStepData {
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
      set{ _setAttrDict = value; }
      get{ return _setAttrDict; }
    }
    /*增加属性*/
    private Dictionary<string,string> _addAttrDict;
    public Dictionary<string,string> addAttrDict {
      set{ _addAttrDict = value; }
      get{ return _addAttrDict; }
    }
    /*增加或者删除物品*/
    private Dictionary<string,string> _dealItemDict;
    public Dictionary<string,string> dealItemDict {
      set{ _dealItemDict = value; }
      get{ return _dealItemDict; }
    }
    /*放弃任务ids*/
    private string[] _giveUpMissionIds;
    public string[] giveUpMissionIds {
      set{ _giveUpMissionIds = value; }
      get{ return _giveUpMissionIds; }
    }
    /*接受任务ids*/
    private string[] _acceptMissionIds;
    public string[] acceptMissionIds {
      set{ _acceptMissionIds = value; }
      get{ return _acceptMissionIds; }
    }
    /*完成任务ids*/
    private string[] _finishMissionIds;
    public string[] finishMissionIds {
      set{ _finishMissionIds = value; }
      get{ return _finishMissionIds; }
    }
    /*增加已完成的任务ids*/
    private string[] _addFinishedMissionIds;
    public string[] addFinishedMissionIds {
      set{ _addFinishedMissionIds = value; }
      get{ return _addFinishedMissionIds; }
    }
    /*删除已完成的任务ids*/
    private string[] _removeFinishedMissionIds;
    public string[] removeFinishedMissionIds {
      set{ _removeFinishedMissionIds = value; }
      get{ return _removeFinishedMissionIds; }
    }
  }
  public class  CfgDoerEventStepIndexData {
    public  CfgDoerEventStepIndexUniqueIndexesListData uniqueIndexesList{ get; set; }
  }
  public class  CfgDoerEventStepIndexUniqueIndexesListData {
    public Dictionary<string, int> id { get; set; } 
  }
}