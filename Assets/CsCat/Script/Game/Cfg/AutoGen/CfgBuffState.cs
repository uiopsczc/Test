//AutoGen. DO NOT EDIT!!!
//ExportFrom Buff表.xlsx[buff状态表]
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgBuffState {
    protected CfgBuffState () {}
    public static CfgBuffState Instance => instance;
    protected static CfgBuffState instance = new CfgBuffState();
    protected CfgBuffStateRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgBuffStateRoot>(jsonStr);}
    public List<CfgBuffStateData> All(){ return this.root.dataList; }
    public CfgBuffStateData Get(int index){ return this.root.dataList[index]; }
    public CfgBuffStateData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.indexDict.uniqueIndexesList.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.indexDict.uniqueIndexesList.id.ContainsKey(key);
    }
  }
  public class CfgBuffStateRoot{
    public List<CfgBuffStateData> dataList { get; set; }
    public CfgBuffStateIndexData indexDict { get; set; }
  }
  public partial class CfgBuffStateData {
    /*id*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
  }
  public class CfgBuffStateIndexData {
    public CfgBuffStateIndexUniqueIndexesListData uniqueIndexesList{ get; set; }
  }
  public class CfgBuffStateIndexUniqueIndexesListData {
    public Dictionary<string, int> id { get; set; } 
  }
}