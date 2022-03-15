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
    public List<CfgSummonBeastGuardSkillData> All(){ return this.root.dataList; }
    public CfgSummonBeastGuardSkillData Get(int index){ return this.root.dataList[index]; }
    public CfgSummonBeastGuardSkillData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.indexDict.uniqueIndexesList.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.indexDict.uniqueIndexesList.id.ContainsKey(key);
    }
    public CfgSummonBeastGuardSkillData GetBySummonBeastIdAndStar(int summonBeastId,int star){
      string[] keys = {summonBeastId.ToString(),star.ToString()};
      string key = string.Join(".", keys);
      return this.Get(this.root.indexDict.uniqueIndexesList.summonBeastId_and_star[key]);
    }
    public bool IsContainsKeyBySummonBeastIdAndStar(int summonBeastId,int star){
      string[] keys = {summonBeastId.ToString(),star.ToString()};
      string key = string.Join(".", keys);
      return this.root.indexDict.uniqueIndexesList.summonBeastId_and_star.ContainsKey(key);
    }
    private Dictionary<string, List<CfgSummonBeastGuardSkillData>> multiplyIndexesList_SummonBeastIdDict = new Dictionary<string, List<CfgSummonBeastGuardSkillData>>();
    public List<CfgSummonBeastGuardSkillData> GetBySummonBeastId(int summonBeastId){
      string key = summonBeastId.ToString();
      if(multiplyIndexesList_SummonBeastIdDict.TryGetValue(key, out var cacheValue))
        return cacheValue;
      List<CfgSummonBeastGuardSkillData> result = new List<CfgSummonBeastGuardSkillData>();
      List<int> indexes = this.root.indexDict.multiplyIndexesList.summonBeastId[key];
      for(int i = 1; i < indexes.Count; i++) 
      {
        var index = indexes[i];
        result.Add(this.Get(index));
      }
      multiplyIndexesList_SummonBeastIdDict[key] = result;
      return result;
    }
    public bool IsContainsKeyBySummonBeastId(int summonBeastId){
      string key = summonBeastId.ToString();
      return this.root.indexDict.multiplyIndexesList.summonBeastId.ContainsKey(key);
    }
    private Dictionary<string, List<CfgSummonBeastGuardSkillData>> multiplyIndexesList_SummonBeastIdAndStarFightingDict = new Dictionary<string, List<CfgSummonBeastGuardSkillData>>();
    public List<CfgSummonBeastGuardSkillData> GetBySummonBeastIdAndStarFighting(int summonBeastId,int starFighting){
      string[] keys = {summonBeastId.ToString(),starFighting.ToString()};
      string key = string.Join(".", keys);
      if(multiplyIndexesList_SummonBeastIdAndStarFightingDict.TryGetValue(key, out var cacheValue))
        return cacheValue;
      List<CfgSummonBeastGuardSkillData> result = new List<CfgSummonBeastGuardSkillData>();
      List<int> indexes = this.root.indexDict.multiplyIndexesList.summonBeastId_and_starFighting[key];
      for(int i = 1; i < indexes.Count; i++) 
      {
        var index = indexes[i];
        result.Add(this.Get(index));
      }
      multiplyIndexesList_SummonBeastIdAndStarFightingDict[key] = result;
      return result;
    }
    public bool IsContainsKeyBySummonBeastIdAndStarFighting(int summonBeastId,int starFighting){
      string[] keys = {summonBeastId.ToString(),starFighting.ToString()};
      string key = string.Join(".", keys);
      return this.root.indexDict.multiplyIndexesList.summonBeastId_and_starFighting.ContainsKey(key);
    }
  }
  public class CfgSummonBeastGuardSkillRoot{
    public List<CfgSummonBeastGuardSkillData> dataList { get; set; }
    public CfgSummonBeastGuardSkillIndexData indexDict { get; set; }
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
    public CfgSummonBeastGuardSkillIndexUniqueIndexesListData uniqueIndexesList{ get; set; }
    public CfgSummonBeastGuardSkillIndexMultiplyIndexesListData multiplyIndexesList{ get; set; }
  }
  public class CfgSummonBeastGuardSkillIndexUniqueIndexesListData {
    public Dictionary<string, int> id { get; set; } 
    public Dictionary<string, int> summonBeastId_and_star { get; set; } 
  }
  public class CfgSummonBeastGuardSkillIndexMultiplyIndexesListData {
    public Dictionary<string, List<int>> summonBeastId { get; set; } 
    public Dictionary<string, List<int>> summonBeastId_and_starFighting { get; set; } 
  }
}