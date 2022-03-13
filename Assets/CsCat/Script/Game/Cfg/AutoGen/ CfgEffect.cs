//AutoGen. DO NOT EDIT!!!
//ExportFrom TX特效表.xlsx[特效表]
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class  CfgEffect {
    protected  CfgEffect () {}
    public static  CfgEffect Instance => instance;
    protected static  CfgEffect instance = new  CfgEffect();
    protected  CfgEffectRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject< CfgEffectRoot>(jsonStr);}
    public List< CfgEffectData> All(){ return this.root.dataList; }
    public  CfgEffectData Get(int index){ return this.root.dataList[index]; }
    public  CfgEffectData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.indexDict.uniqueIndexesList.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.indexDict.uniqueIndexesList.id.ContainsKey(key);
    }
  }
  public class  CfgEffectRoot{
    public List< CfgEffectData> dataList { get; set; }
    public  CfgEffectIndexData indexDict { get; set; }
  }
  public partial class  CfgEffectData {
    /*id*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
    /*预设路径*/
    public string prefabPath { get; set; }
    /*时长*/
    public float duration { get; set; }
    /*插座1*/
    public string socketName1 { get; set; }
    /*插座2*/
    public string socketName2 { get; set; }
  }
  public class  CfgEffectIndexData {
    public  CfgEffectIndexUniqueIndexesListData uniqueIndexesList{ get; set; }
  }
  public class  CfgEffectIndexUniqueIndexesListData {
    public Dictionary<string, int> id { get; set; } 
  }
}