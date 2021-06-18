namespace CsCat
{
  public class DefinitionManager : ISingleton
  {
    public static DefinitionManager instance
    {
      get { return SingletonFactory.instance.Get<DefinitionManager>(); }
    }
    
    //  public SkillDefinition skillDefinition = new SkillDefinition();


    
  }
}