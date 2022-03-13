//AutoGen. DO NOT EDIT!!!
//ExportFrom YY语言表.xlsx[语言表]
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgLang {
    protected CfgLang () {}
    public static CfgLang Instance => instance;
    protected static CfgLang instance = new CfgLang();
    protected CfgLangRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgLangRoot>(jsonStr);}
    public List<CfgLangData> All(){ return this.root.dataList; }
    public CfgLangData Get(int index){ return this.root.dataList[index]; }
    public CfgLangData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.indexDict.uniqueIndexesList.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.indexDict.uniqueIndexesList.id.ContainsKey(key);
    }
  }
  public class CfgLangRoot{
    public List<CfgLangData> dataList { get; set; }
    public CfgLangIndexData indexDict { get; set; }
  }
  public partial class CfgLangData {
    /*id*/
    public string id { get; set; }
    /*英文*/
    public string english { get; set; }
  }
  public class CfgLangIndexData {
    public CfgLangIndexUniqueIndexesListData uniqueIndexesList{ get; set; }
  }
  public class CfgLangIndexUniqueIndexesListData {
    public Dictionary<string, int> id { get; set; } 
  }
}