//AutoGen. DO NOT EDIT!!!
//ExportFrom ZHS召唤兽.xlsx[召唤兽加护技能表]
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgSummonBeastGuardSkill {
    protected CfgSummonBeastGuardSkill () {}
    public static CfgSummonBeastGuardSkill Instance => instance;
    protected static CfgSummonBeastGuardSkill instance = new CfgSummonBeastGuardSkill();
    protected CfgSummonBeastGuardSkillRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgSummonBeastGuardSkillRoot>(jsonStr);}
    public List<CfgSummonBeastGuardSkillData> All(){ return this.root.data_list; }
    public CfgSummonBeastGuardSkillData Get(int index){ return this.root.data_list[index]; }
    public CfgSummonBeastGuardSkillData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.index_dict.unique.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.index_dict.unique.id.ContainsKey(key);
    }
    public CfgSummonBeastGuardSkillData GetBySummonBeastIdAndStar(int summonBeastId,int star){
      string[] keys = {summonBeastId.ToString(),star.ToString()};
      string key = string.Join(".", keys);
      return this.Get(this.root.index_dict.unique.summonBeastId_and_star[key]);
    }
    public bool IsContainsKeyBySummonBeastIdAndStar(int summonBeastId,int star){
      string[] keys = {summonBeastId.ToString(),star.ToString()};
      string key = string.Join(".", keys);
      return this.root.index_dict.unique.summonBeastId_and_star.ContainsKey(key);
    }
    public List<CfgSummonBeastGuardSkillData> GetBySummonBeastId(int summonBeastId){
      string key = summonBeastId.ToString();
      List<CfgSummonBeastGuardSkillData> result = new List<CfgSummonBeastGuardSkillData>();
      List<int> indexes = this.root.index_dict.multiple.summonBeastId[key];
      foreach(int index in indexes) { result.Add(this.Get(index)); }
      return result;
    }
    public bool IsContainsKeyBySummonBeastId(int summonBeastId){
      string key = summonBeastId.ToString();
      return this.root.index_dict.multiple.summonBeastId.ContainsKey(key);
    }
    public List<CfgSummonBeastGuardSkillData> GetBySummonBeastIdAndStarFighting(int summonBeastId,int starFighting){
      string[] keys = {summonBeastId.ToString(),starFighting.ToString()};
      string key = string.Join(".", keys);
      List<CfgSummonBeastGuardSkillData> result = new List<CfgSummonBeastGuardSkillData>();
      List<int> indexes = this.root.index_dict.multiple.summonBeastId_and_starFighting[key];
      foreach(int index in indexes) { result.Add(this.Get(index)); }
      return result;
    }
    public bool IsContainsKeyBySummonBeastIdAndStarFighting(int summonBeastId,int starFighting){
      string[] keys = {summonBeastId.ToString(),starFighting.ToString()};
      string key = string.Join(".", keys);
      return this.root.index_dict.multiple.summonBeastId_and_starFighting.ContainsKey(key);
    }
  }
  public class CfgSummonBeastGuardSkillRoot{
    public List<CfgSummonBeastGuardSkillData> data_list { get; set; }
    public CfgSummonBeastGuardSkillIndexData index_dict { get; set; }
  }
  public partial class CfgSummonBeastGuardSkillData {
    /*id*/
    public string id { get; set; }
    /*召唤兽id*/
    public int summonBeastId { get; set; }
    /*星级*/
    public int star { get; set; }
    /*星级战力*/
    public int starFighting { get; set; }
    /*技能描述1*/
    public string skillDescLang1 { get; set; }
    /*技能描述参数列表1*/
    public LitJson.JsonData skillDescArgs1 { get; set; }
    /*技能描述2*/
    public string skillDescLang2 { get; set; }
    /*技能描述参数列表2*/
    public LitJson.JsonData skillDescArgs2 { get; set; }
    /*技能描述3*/
    public string skillDescLang3 { get; set; }
    /*技能描述参数列表3*/
    public LitJson.JsonData skillDescArgs3 { get; set; }
    /*技能描述4(长描述)*/
    public string skillDescLang4 { get; set; }
    /*技能描述参数列表4*/
    public LitJson.JsonData skillDescArgs4 { get; set; }
  }
  public class CfgSummonBeastGuardSkillIndexData {
    public CfgSummonBeastGuardSkillIndexUniqueData unique{ get; set; }
    public CfgSummonBeastGuardSkillIndexMultipleData multiple{ get; set; }
  }
  public class CfgSummonBeastGuardSkillIndexUniqueData {
    public Dictionary<string, int> id { get; set; } 
    public Dictionary<string, int> summonBeastId_and_star { get; set; } 
  }
  public class CfgSummonBeastGuardSkillIndexMultipleData {
    public Dictionary<string,List<int>> summonBeastId { get; set; } 
    public Dictionary<string,List<int>> summonBeastId_and_starFighting { get; set; } 
  }
}