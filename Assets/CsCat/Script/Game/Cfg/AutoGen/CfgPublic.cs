//AutoGen. DO NOT EDIT!!!
//ExportFrom GG公共表.xlsx[公共表]
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgPublic {
    protected CfgPublic () {}
    public static CfgPublic Instance => instance;
    protected static CfgPublic instance = new CfgPublic();
    protected CfgPublicRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgPublicRoot>(jsonStr);}
    public List<CfgPublicData> All(){ return this.root.dataList; }
    public CfgPublicData Get(int index){ return this.root.dataList[index]; }
    public CfgPublicData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.indexDict.uniqueIndexesList.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.indexDict.uniqueIndexesList.id.ContainsKey(key);
    }
  }
  public class CfgPublicRoot{
    public List<CfgPublicData> dataList { get; set; }
    public CfgPublicIndexData indexDict { get; set; }
  }
  public partial class CfgPublicData {
    /*id*/
    public string id { get; set; }
    /*值*/
    public string value { get; set; }
    /*值dict*/
    private Dictionary<string,string> _valueDict;
    public Dictionary<string,string> valueDict {
      get{
        if(_valueDict == default(Dictionary<string,string>)) _valueDict = valueDict.To<Dictionary<string,string>>();
        return _valueDict;
      }
    }
  }
  public class CfgPublicIndexData {
    public CfgPublicIndexUniqueIndexesListData uniqueIndexesList{ get; set; }
  }
  public class CfgPublicIndexUniqueIndexesListData {
    public Dictionary<string, int> id { get; set; } 
  }
}