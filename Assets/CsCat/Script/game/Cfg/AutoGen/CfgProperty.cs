//AutoGen. DO NOT EDIT!!!
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgProperty {
    protected CfgProperty () {}
    public static CfgProperty Instance => instance;
    protected static CfgProperty instance = new CfgProperty();
    protected CfgPropertyRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgPropertyRoot>(jsonStr);}
    public List<CfgPropertyData> All(){ return this.root.data_list; }
    public CfgPropertyData Get(int index){ return this.root.data_list[index]; }
    public CfgPropertyData get_by_id(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool contain_key_by_id(string id){
      string key = id.ToString();
      return this.root.index_dict.unique.id.ContainsKey(key);
    }
  }
  public class CfgPropertyRoot{
    public List<CfgPropertyData> data_list { get; set; }
    public CfgPropertyIndexData index_dict { get; set; }
  }
  public partial class CfgPropertyData {
    /*id*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
    /*是否是百分数*/
    public bool is_pct { get; set; }
  }
  public class CfgPropertyIndexData {
    public CfgPropertyIndexUniqueData unique{ get; set; }
  }
  public class CfgPropertyIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
  }
}