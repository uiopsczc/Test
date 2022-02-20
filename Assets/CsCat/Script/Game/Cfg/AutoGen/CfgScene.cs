//AutoGen. DO NOT EDIT!!!
//ExportFrom CJ场景表.xlsx[场景表]
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgScene {
    protected CfgScene () {}
    public static CfgScene Instance => instance;
    protected static CfgScene instance = new CfgScene();
    protected CfgSceneRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgSceneRoot>(jsonStr);}
    public List<CfgSceneData> All(){ return this.root.data_list; }
    public CfgSceneData Get(int index){ return this.root.data_list[index]; }
    public CfgSceneData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.index_dict.unique.id.ContainsKey(key);
    }
  }
  public class CfgSceneRoot{
    public List<CfgSceneData> data_list { get; set; }
    public CfgSceneIndexData index_dict { get; set; }
  }
  public partial class CfgSceneData {
    /*id*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
    /*classPathLua*/
    public string classPathLua { get; set; }
    /*classPathCs*/
    public string classPathCs { get; set; }
  }
  public class CfgSceneIndexData {
    public CfgSceneIndexUniqueData unique{ get; set; }
  }
  public class CfgSceneIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
  }
}