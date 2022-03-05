//AutoGen. DO NOT EDIT!!!
//ExportFrom RW任务表.xlsx[任务表]
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgMission {
    protected CfgMission () {}
    public static CfgMission Instance => instance;
    protected static CfgMission instance = new CfgMission();
    protected CfgMissionRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgMissionRoot>(jsonStr);}
    public List<CfgMissionData> All(){ return this.root.data_list; }
    public CfgMissionData Get(int index){ return this.root.data_list[index]; }
    public CfgMissionData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.index_dict.unique.id.ContainsKey(key);
    }
  }
  public class CfgMissionRoot{
    public List<CfgMissionData> data_list { get; set; }
    public CfgMissionIndexData index_dict { get; set; }
  }
  public partial class CfgMissionData {
    /*ID*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
    /*classPathLua*/
    public string classPathLua { get; set; }
    /*classPathCs*/
    public string classPathCs { get; set; }
    /*type1*/
    public string type1 { get; set; }
    /*type2*/
    public string type2 { get; set; }
    /*完成的条件*/
    public string finishCondition { get; set; }
    /*是否是自动检测完成*/
    public bool isAutoCheckFinish { get; set; }
    /*接受时触发的事件id*/
    public string onAcceptDoerEventId { get; set; }
    /*完成时触发的事件id*/
    public string onFinishDoerEventId { get; set; }
    /*放弃时触发的事件id*/
    public string onGiveUpDoerEventId { get; set; }
    /*奖励*/
    private Dictionary<string,string> _rewardDict;
    public Dictionary<string,string> rewardDict {
      get{
        if(_rewardDict == default(Dictionary<string,string>)) _rewardDict = rewardDict.To<Dictionary<string,string>>();
        return _rewardDict;
      }
    }
    /*寻找物品*/
    private Dictionary<string,string> _findItemDict;
    public Dictionary<string,string> findItemDict {
      get{
        if(_findItemDict == default(Dictionary<string,string>)) _findItemDict = findItemDict.To<Dictionary<string,string>>();
        return _findItemDict;
      }
    }
  }
  public class CfgMissionIndexData {
    public CfgMissionIndexUniqueData unique{ get; set; }
  }
  public class CfgMissionIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
  }
}