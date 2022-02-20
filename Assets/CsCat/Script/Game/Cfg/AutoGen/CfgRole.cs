//AutoGen. DO NOT EDIT!!!
//ExportFrom JS角色表.xlsx[角色表]
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgRole {
    protected CfgRole () {}
    public static CfgRole Instance => instance;
    protected static CfgRole instance = new CfgRole();
    protected CfgRoleRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgRoleRoot>(jsonStr);}
    public List<CfgRoleData> All(){ return this.root.data_list; }
    public CfgRoleData Get(int index){ return this.root.data_list[index]; }
    public CfgRoleData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.index_dict.unique.id.ContainsKey(key);
    }
  }
  public class CfgRoleRoot{
    public List<CfgRoleData> data_list { get; set; }
    public CfgRoleIndexData index_dict { get; set; }
  }
  public partial class CfgRoleData {
    /*ID*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
    /*classPathLua*/
    public string classPathLua { get; set; }
    /*classPathCs*/
    public string classPathCs { get; set; }
  }
  public class CfgRoleIndexData {
    public CfgRoleIndexUniqueData unique{ get; set; }
  }
  public class CfgRoleIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
  }
}