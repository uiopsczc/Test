//AutoGen. DO NOT EDIT!!!
//ExportFrom Test\CS测试表.xlsx[测试表]
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgTest {
    protected CfgTest () {}
    public static CfgTest Instance => instance;
    protected static CfgTest instance = new CfgTest();
    protected CfgTestRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgTestRoot>(jsonStr);}
    public List<CfgTestData> All(){ return this.root.data_list; }
    public CfgTestData Get(int index){ return this.root.data_list[index]; }
    public CfgTestData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.index_dict.unique.id.ContainsKey(key);
    }
  }
  public class CfgTestRoot{
    public List<CfgTestData> data_list { get; set; }
    public CfgTestIndexData index_dict { get; set; }
  }
  public partial class CfgTestData {
    /*id*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
    /*国家*/
    public string country { get; set; }
    private string __country;
    public string _country {
      get{
        if(__country == default(string)) __country = country.To<string>();
        return __country;
      }
    }
    /*ageDict*/
    public LitJson.JsonData ageDict { get; set; }
    private Dictionary<string,int[]> __ageDict;
    public Dictionary<string,int[]> _ageDict {
      get{
        if(__ageDict == default(Dictionary<string,int[]>)) __ageDict = ageDict.To<Dictionary<string,int[]>>();
        return __ageDict;
      }
    }
  }
  public class CfgTestIndexData {
    public CfgTestIndexUniqueData unique{ get; set; }
  }
  public class CfgTestIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
  }
}