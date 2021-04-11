namespace CsCat
{
  public class DefinitionManager : ISingleton
  {
    public static DefinitionManager instance
    {
      get { return SingletonFactory.instance.Get<DefinitionManager>(); }
    }

    public TestDefinition testDefinition = new TestDefinition();

    public RoleDefinition roleDefinition = new RoleDefinition();
    public ItemDefinition itemDefinition = new ItemDefinition();
    public QualityDefinition qualityDefinition = new QualityDefinition();
    public MissionDefinition missionDefinition = new MissionDefinition();
    public DoerEventDefinition doerEventDefinition = new DoerEventDefinition();
    public DoerEventStepDefinition doerEventStepDefinition = new DoerEventStepDefinition();

    public SceneDefinition sceneDefinition = new SceneDefinition();
    //  public SkillDefinition skillDefinition = new SkillDefinition();

    public PublicDefinition publicDefinition = new PublicDefinition();
    public EffectDefinition effectDefinition = new EffectDefinition();
    public PropertyDefinition propertyDefinition = new PropertyDefinition();
    public BuffDefinition buffDefinition = new BuffDefinition();
    public BuffStateDefinition buffStateDefinition = new BuffStateDefinition();
    public SpellDefinition spellDefinition = new SpellDefinition();
    public SpellTriggerDefinition spellTriggerDefinition = new SpellTriggerDefinition();
    public UnitDefinition unitDefinition = new UnitDefinition();


    public SpellDefinition GetSpellDefinition(string spell_id)
    {
      return spellDefinition.GetData(spell_id);
    }
  }
}