//AutoGen. DO NOT EDIT!!!
//ExportFrom DW单位表.xlsx[单位表]
using System;
using System.Collections.Generic;
using LitJson;
namespace CsCat{
  public class CfgUnit {
    protected CfgUnit () {}
    public static CfgUnit Instance => instance;
    protected static CfgUnit instance = new CfgUnit();
    protected CfgUnitRoot root;
    public void Parse(string jsonStr) { this.root=JsonMapper.ToObject<CfgUnitRoot>(jsonStr);}
    public List<CfgUnitData> All(){ return this.root.dataList; }
    public CfgUnitData Get(int index){ return this.root.dataList[index]; }
    public CfgUnitData GetById(string id){
      string key = id.ToString();
      return this.Get(this.root.indexDict.uniqueIndexesList.id[key]);
    }
    public bool IsContainsKeyById(string id){
      string key = id.ToString();
      return this.root.indexDict.uniqueIndexesList.id.ContainsKey(key);
    }
  }
  public class CfgUnitRoot{
    public List<CfgUnitData> dataList { get; set; }
    public CfgUnitIndexData indexDict { get; set; }
  }
  public partial class CfgUnitData {
    /*ID*/
    public string id { get; set; }
    /*名字*/
    public string name { get; set; }
    /*类型*/
    public string type { get; set; }
    /*y轴偏移*/
    public float offsetY { get; set; }
    /*半径*/
    public float radius { get; set; }
    /*缩放*/
    public float scale { get; set; }
    /*一轮走路动画走多远*/
    public float walkStepLength { get; set; }
    /*模型路径*/
    public string modelPath { get; set; }
    /*普攻ids*/
    private string[] _normalAttackIds;
    public string[] normalAttackIds {
      get{
        if(_normalAttackIds == default(string[])) _normalAttackIds = normalAttackIds.To<string[]>();
        return _normalAttackIds;
      }
    }
    /*技能ids*/
    private string[] _skillIds;
    public string[] skillIds {
      get{
        if(_skillIds == default(string[])) _skillIds = skillIds.To<string[]>();
        return _skillIds;
      }
    }
    /*ai实现类(lua)*/
    public string aiClassPathLua { get; set; }
    /*ai实现类(cs)*/
    public string aiClassPathCs { get; set; }
    /*死亡后是否保留尸体*/
    public bool isKeepDeadBody { get; set; }
    /*死亡后多少秒才销毁尸体*/
    public float deadBodyDealy { get; set; }
    /*死亡时候触发的特效id*/
    public string deathEffectId { get; set; }
    /*被动buff ids*/
    private string[] _passiveBuffIds;
    public string[] passiveBuffIds {
      get{
        if(_passiveBuffIds == default(string[])) _passiveBuffIds = passiveBuffIds.To<string[]>();
        return _passiveBuffIds;
      }
    }
  }
  public class CfgUnitIndexData {
    public CfgUnitIndexUniqueIndexesListData uniqueIndexesList{ get; set; }
  }
  public class CfgUnitIndexUniqueIndexesListData {
    public Dictionary<string, int> id { get; set; } 
  }
}