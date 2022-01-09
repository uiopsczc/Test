//namespace CsCat
//{
//  public class SkillFactory : DoerFactory
//  {
//    protected override string default_doer_class_path
//    {
//      get { return "CsCat.Skill"; }
//    }
//
//    public override ExcelAssetBase GetDefinitions()
//    {
//      return DefinitionManager.instance.skillDefinition;
//    }
//
//    public override ExcelAssetBase GetDefinition(string id)
//    {
//      return GetDefinitions().GetData<SkillDefinition>(id);
//    }
//
//    public SkillDefinition GetSkillDefinition(string id)
//    {
//      return GetDefinition(id) as SkillDefinition;
//    }
//
//    protected override DBase __NewDBase(string id_or_rid)
//    {
//      return new SkillDBase(id_or_rid);
//    }
//
//  }
//}