//AutoGen. DO NOT EDIT!!!
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgTranslation {
    protected CfgTranslation () {}
    public static CfgTranslation Instance => instance;
    protected static CfgTranslation instance = new CfgTranslation();
    protected CfgTranslationRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgTranslationRoot>(jsonStr);}
    public List<CfgTranslationData> All(){ return this.root.data_list; }
    public CfgTranslationData Get(int index){ return this.root.data_list[index]; }
    public CfgTranslationData get_by_id(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool contain_key_by_id(string id){
      string key = id.ToString();
      return this.root.index_dict.unique.id.ContainsKey(key);
    }
  }
  public class CfgTranslationRoot{
    public List<CfgTranslationData> data_list { get; set; }
    public CfgTranslationIndexData index_dict { get; set; }
  }
  public partial class CfgTranslationData {
    /*id*/
    public string id { get; set; }
    /*英文*/
    public string english { get; set; }
  }
  public class CfgTranslationIndexData {
    public CfgTranslationIndexUniqueData unique{ get; set; }
  }
  public class CfgTranslationIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
  }
}