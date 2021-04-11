namespace CsCat
{
  public class MissionFactory : DoerFactory
  {
    protected override string default_doer_class_path
    {
      get { return "CsCat.Mission"; }
    }

    public override ExcelAssetBase GetDefinitions()
    {
      return DefinitionManager.instance.missionDefinition;
    }

    public override ExcelAssetBase GetDefinition(string id)
    {
      return GetDefinitions().GetData<MissionDefinition>(id);
    }

    public MissionDefinition GetMissionDefinition(string id)
    {
      return GetDefinition(id) as MissionDefinition;
    }

    protected override DBase __NewDBase(string id_or_rid)
    {
      return new MissionDBase(id_or_rid);
    }
  }
}