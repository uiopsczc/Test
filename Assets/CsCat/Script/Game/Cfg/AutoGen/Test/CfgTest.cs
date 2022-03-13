//AutoGen. DO NOT EDIT!!!
//ExportFrom CS测试\CS测试表.xlsx[测试表]
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
    public List<CfgTestData> All(){ return this.root.dataList; }
    public CfgTestData Get(int index){ return this.root.dataList[index]; }
    public CfgTestData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.indexDict.uniqueIndexesList.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.indexDict.uniqueIndexesList.id.ContainsKey(key);
    }
    public CfgTestData GetByName(string name){
      string key = name.ToString();
      return this.Get(this.root.indexDict.uniqueIndexesList.name[key]);
    }
    public bool IsContainsKeyByName(string name){
      string key = name.ToString();
      return this.root.indexDict.uniqueIndexesList.name.ContainsKey(key);
    }
    private Dictionary<string, List<CfgTestData>> multiplyIndexesList_IdAndNameDict = new Dictionary<string, List<CfgTestData>>();
    public List<CfgTestData> GetByIdAndName(string id,string name){
      string[] keys = {id.ToString(),name.ToString()};
      string key = string.Join(".", keys);
      if(multiplyIndexesList_IdAndNameDict.TryGetValue(key, out var cacheValue))
        return cacheValue;
      List<CfgTestData> result = new List<CfgTestData>();
      List<int> indexes = this.root.indexDict.multiplyIndexesList.id_and_name[key];
      for(int i = 1; i < indexes.Count; i++) 
      {
        var index = indexes[i];
        result.Add(this.Get(index));
      }
      multiplyIndexesList_IdAndNameDict[key] = result;
      return result;
    }
    public bool IsContainsKeyByIdAndName(string id,string name){
      string[] keys = {id.ToString(),name.ToString()};
      string key = string.Join(".", keys);
      return this.root.indexDict.multiplyIndexesList.id_and_name.ContainsKey(key);
    }
    private Dictionary<string, List<CfgTestData>> multiplyIndexesList_NameAndCountryDict = new Dictionary<string, List<CfgTestData>>();
    public List<CfgTestData> GetByNameAndCountry(string name,string country){
      string[] keys = {name.ToString(),country.ToString()};
      string key = string.Join(".", keys);
      if(multiplyIndexesList_NameAndCountryDict.TryGetValue(key, out var cacheValue))
        return cacheValue;
      List<CfgTestData> result = new List<CfgTestData>();
      List<int> indexes = this.root.indexDict.multiplyIndexesList.name_and_country[key];
      for(int i = 1; i < indexes.Count; i++) 
      {
        var index = indexes[i];
        result.Add(this.Get(index));
      }
      multiplyIndexesList_NameAndCountryDict[key] = result;
      return result;
    }
    public bool IsContainsKeyByNameAndCountry(string name,string country){
      string[] keys = {name.ToString(),country.ToString()};
      string key = string.Join(".", keys);
      return this.root.indexDict.multiplyIndexesList.name_and_country.ContainsKey(key);
    }
  }
  public class CfgTestRoot{
    public List<CfgTestData> dataList { get; set; }
    public CfgTestIndexData indexDict { get; set; }
  }
  public partial class CfgTestData {
    /*id*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
    /*国家*/
    private string _country;
    public string country {
      get{
        if(_country == default(string)) _country = country.To<string>();
        return _country;
      }
    }
    /*ageDict*/
    private Dictionary<string,int[]> _ageDict;
    public Dictionary<string,int[]> ageDict {
      get{
        if(_ageDict == default(Dictionary<string,int[]>)) _ageDict = ageDict.To<Dictionary<string,int[]>>();
        return _ageDict;
      }
    }
  }
  public class CfgTestIndexData {
    public CfgTestIndexUniqueIndexesListData uniqueIndexesList{ get; set; }
    public CfgTestIndexMultiplyIndexesListData multiplyIndexesList{ get; set; }
  }
  public class CfgTestIndexUniqueIndexesListData {
    public Dictionary<string, int> id { get; set; } 
    public Dictionary<string, int> name { get; set; } 
  }
  public class CfgTestIndexMultiplyIndexesListData {
    public Dictionary<string,List<int>> id_and_name { get; set; } 
    public Dictionary<string,List<int>> name_and_country { get; set; } 
  }
}