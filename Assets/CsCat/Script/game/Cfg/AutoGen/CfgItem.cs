//AutoGen. DO NOT EDIT!!!
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgItem {
    protected CfgItem () {}
    public static CfgItem Instance => instance;
    protected static CfgItem instance = new CfgItem();
    protected CfgItemRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgItemRoot>(jsonStr);}
    public List<CfgItemData> All(){ return this.root.data_list; }
    public CfgItemData Get(int index){ return this.root.data_list[index]; }
    public CfgItemData get_by_id(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool contain_key_by_id(string id){
      string key = id.ToString();
      return this.root.index_dict.unique.id.ContainsKey(key);
    }
  }
  public class CfgItemRoot{
    public List<CfgItemData> data_list { get; set; }
    public CfgItemIndexData index_dict { get; set; }
  }
  public partial class CfgItemData {
    /*id*/
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
    /*能不能被折叠*/
    public bool can_fold { get; set; }
    /*品质*/
    public string quality_id { get; set; }
    /*背景图片路径*/
    public string bg_path { get; set; }
    /*icon图片路径*/
    public string icon_path { get; set; }
  }
  public class CfgItemIndexData {
    public CfgItemIndexUniqueData unique{ get; set; }
  }
  public class CfgItemIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
  }
}