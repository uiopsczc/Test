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
    public List<CfgPublicData> All(){ return this.root.data_list; }
    public CfgPublicData Get(int index){ return this.root.data_list[index]; }
    public CfgPublicData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.index_dict.unique.id.ContainsKey(key);
    }
  }
  public class CfgPublicRoot{
    public List<CfgPublicData> data_list { get; set; }
    public CfgPublicIndexData index_dict { get; set; }
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
    public CfgPublicIndexUniqueData unique{ get; set; }
  }
  public class CfgPublicIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
  }
}