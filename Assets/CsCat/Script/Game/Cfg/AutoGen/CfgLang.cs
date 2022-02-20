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
    public List<CfgLangData> All(){ return this.root.data_list; }
    public CfgLangData Get(int index){ return this.root.data_list[index]; }
    public CfgLangData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.index_dict.unique.id.ContainsKey(key);
    }
  }
  public class CfgLangRoot{
    public List<CfgLangData> data_list { get; set; }
    public CfgLangIndexData index_dict { get; set; }
  }
  public partial class CfgLangData {
    /*id*/
    public string id { get; set; }
    /*英文*/
    public string english { get; set; }
  }
  public class CfgLangIndexData {
    public CfgLangIndexUniqueData unique{ get; set; }
  }
  public class CfgLangIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
  }
}