//AutoGen. DO NOT EDIT!!!
//ExportFrom PZ品质表.xlsx[Sheet1]
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgQuality {
    protected CfgQuality () {}
    public static CfgQuality Instance => instance;
    protected static CfgQuality instance = new CfgQuality();
    protected CfgQualityRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgQualityRoot>(jsonStr);}
    public List<CfgQualityData> All(){ return this.root.dataList; }
    public CfgQualityData Get(int index){ return this.root.dataList[index]; }
    public CfgQualityData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.indexDict.uniqueIndexesList.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.indexDict.uniqueIndexesList.id.ContainsKey(key);
    }
  }
  public class CfgQualityRoot{
    public List<CfgQualityData> dataList { get; set; }
    public CfgQualityIndexData indexDict { get; set; }
  }
  public partial class CfgQualityData {
    /*id*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
    /*图片路径*/
    public string iconPath { get; set; }
  }
  public class CfgQualityIndexData {
    public CfgQualityIndexUniqueIndexesListData uniqueIndexesList{ get; set; }
  }
  public class CfgQualityIndexUniqueIndexesListData {
    public Dictionary<string, int> id { get; set; } 
  }
}