//AutoGen. DO NOT EDIT!!!
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgBuff {
    protected CfgBuff () {}
    public static CfgBuff Instance => instance;
    protected static CfgBuff instance = new CfgBuff();
    protected CfgBuffRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgBuffRoot>(jsonStr);}
    public List<CfgBuffData> All(){ return this.root.data_list; }
    public CfgBuffData Get(int index){ return this.root.data_list[index]; }
    public CfgBuffData get_by_id(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool contain_key_by_id(string id){
      string key = id.ToString();
      return this.root.index_dict.unique.id.ContainsKey(key);
    }
  }
  public class CfgBuffRoot{
    public List<CfgBuffData> data_list { get; set; }
    public CfgBuffIndexData index_dict { get; set; }
  }
  public partial class CfgBuffData {
    /*id*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
    /*类型
(buff,debuff)*/
    public string type_1 { get; set; }
    /*二级类型
(控制,)*/
    public string type_2 { get; set; }
    /*持续时间*/
    public float duration { get; set; }
    /*特效ids*/
    public LitJson.JsonData effect_ids { get; set; }
    /*状态*/
    public string state { get; set; }
    /*是否只会只有一个生效*/
    public bool is_unique { get; set; }
    /*触发技能id*/
    public string trigger_spell_id { get; set; }
    /*修改属性dict*/
    public LitJson.JsonData property_dict { get; set; }
  }
  public class CfgBuffIndexData {
    public CfgBuffIndexUniqueData unique{ get; set; }
  }
  public class CfgBuffIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
  }
}