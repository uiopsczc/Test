//AutoGen. DO NOT EDIT!!!
//ExportFrom SX属性表.xlsx[属性表]
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
    public List<CfgPropertyData> All(){ return this.root.dataList; }
    public CfgPropertyData Get(int index){ return this.root.dataList[index]; }
    public CfgPropertyData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.indexDict.uniqueIndexesList.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.indexDict.uniqueIndexesList.id.ContainsKey(key);
    }
  }
  public class CfgPropertyRoot{
    public List<CfgPropertyData> dataList { get; set; }
    public CfgPropertyIndexData indexDict { get; set; }
  }
  public partial class CfgPropertyData {
    /*id*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
    /*是否是百分数*/
    public bool isPct { get; set; }
  }
  public class CfgPropertyIndexData {
    public CfgPropertyIndexUniqueIndexesListData uniqueIndexesList{ get; set; }
  }
  public class CfgPropertyIndexUniqueIndexesListData {
    public Dictionary<string, int> id { get; set; } 
  }
}