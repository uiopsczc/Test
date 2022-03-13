//AutoGen. DO NOT EDIT!!!
//ExportFrom WP物品表.xlsx[物品表]
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
    public List<CfgItemData> All(){ return this.root.dataList; }
    public CfgItemData Get(int index){ return this.root.dataList[index]; }
    public CfgItemData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.indexDict.uniqueIndexesList.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.indexDict.uniqueIndexesList.id.ContainsKey(key);
    }
  }
  public class CfgItemRoot{
    public List<CfgItemData> dataList { get; set; }
    public CfgItemIndexData indexDict { get; set; }
  }
  public partial class CfgItemData {
    /*id*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
    /*classPathLua*/
    public string classPathLua { get; set; }
    /*classPathCs*/
    public string classPathCs { get; set; }
    /*type1*/
    public string type1 { get; set; }
    /*type2*/
    public string type2 { get; set; }
    /*能不能被折叠*/
    public bool isCanFold { get; set; }
    /*品质*/
    public string qualityId { get; set; }
    /*背景图片路径*/
    public string bgPath { get; set; }
    /*icon图片路径*/
    public string iconPath { get; set; }
  }
  public class CfgItemIndexData {
    public CfgItemIndexUniqueIndexesListData uniqueIndexesList{ get; set; }
  }
  public class CfgItemIndexUniqueIndexesListData {
    public Dictionary<string, int> id { get; set; } 
  }
}