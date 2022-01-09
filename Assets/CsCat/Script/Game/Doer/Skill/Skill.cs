//using System.Collections;
//
//namespace CsCat
//{
//  public partial class Skill : Doer
//  {
//    public override void Init()
//    {
//      base.Init();
//    }
//
//    public SkillFactory GetSkillFactory()
//    {
//      return this.factory as SkillFactory;
//    }
//
//    public SkillDefinition GetSkillDefinition()
//    {
//      return GetSkillFactory().GetSkillDefinition(this.GetId());
//    }
//
//    ///////////////////////////////////////DoXXX//////////////////////////////
//    //卸载
//    public override void DoRelease()
//    {
//      //卸载
//      //xxxx
//      base.DoRelease();
//    }
//
//    //保存
//    public override void DoSave(Hashtable dict, Hashtable dict_tmp)
//    {
//      base.DoSave(dict, dict_tmp);
//      // 存储
//      //xxxx
//    }
//
//    //还原
//    public override void DoRestore(Hashtable dict, Hashtable dict_tmp)
//    {
//      // 还原
//      //xxxx
//      base.DoRestore(dict, dict_tmp);
//    }
//
//
//    ///////////////////////////////////////OnXXX//////////////////////////////
//    // 升级检测事件，返回true则调用升级事件，调用升级事件后将会再次检测该方法，直到返回false
//    public override bool OnCheckUpgrade(string key)
//    {
//      return true;
//    }
//
//    //升级事件（此处做升级相关处理）
//    public override void OnUpgrade(string key)
//    {
//      this.AddLevel(1);
//    }
//
//    //学会
//    public void OnLearn(Critter critter)
//    {
//    }
//
//    //废除
//    public void OnUnLearn(Critter critter)
//    {
//    }
//
//    public void OnCheckUse(SkillContext skillContext)
//    {
//
//    }
//
//    public void OnUse(SkillContext skillContext)
//    {
//
//    }
//
//    /////////////////////////////////////////////////Util//////////////////////////////////
//    public void SetLevel(int level)
//    {
//      this.Set("level", level);
//    }
//
//    public void AddLevel(int add_level)
//    {
//      this.SetLevel(this.GetLevel() + add_level);
//    }
//
//    public int GetLevel()
//    {
//      return this.Get("level", 1);
//    }
//
//    public void SetGroup(string group)
//    {
//      this.Set("group", group);
//    }
//
//    public string GetGroup()
//    {
//      return this.Get<string>("group");
//    }
//  }
//}