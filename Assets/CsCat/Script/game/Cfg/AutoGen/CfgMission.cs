//AutoGen. DO NOT EDIT!!!
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
    public CfgMissionData get_by_id(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool contain_key_by_id(string id){
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
    /*class_path_lua*/
    public string class_path_lua { get; set; }
    /*class_path_cs*/
    public string class_path_cs { get; set; }
    /*type_1*/
    public string type_1 { get; set; }
    /*type_2*/
    public string type_2 { get; set; }
    /*完成的条件*/
    public string finish_condition { get; set; }
    /*是否是自动检测完成*/
    public bool is_auto_check_finish { get; set; }
    /*接受时触发的事件id*/
    public string onAccept_doerEvent_id { get; set; }
    /*完成时触发的事件id*/
    public string onFinish_doerEvent_id { get; set; }
    /*放弃时触发的事件id*/
    public string onGiveUp_doerEvent_id { get; set; }
    /*奖励*/
    public LitJson.JsonData reward_dict { get; set; }
    /*寻找物品*/
    public LitJson.JsonData find_item_dict { get; set; }
  }
  public class CfgMissionIndexData {
    public CfgMissionIndexUniqueData unique{ get; set; }
  }
  public class CfgMissionIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
  }
}