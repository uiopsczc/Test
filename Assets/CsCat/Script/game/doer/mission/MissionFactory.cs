namespace CsCat
{
  public class MissionFactory : DoerFactory
  {
    protected override string default_doer_class_path => "CsCat.Mission";

    protected override string GetClassPath(string id)
    {
      return this.GetCfgMissionData(id).class_path_cs.IsNullOrWhiteSpace() ? base.GetClassPath(id) : GetCfgMissionData(id).class_path_cs;
    }
    

    public CfgMissionData GetCfgMissionData(string id)
    {
      return CfgMission.Instance.get_by_id(id);
    }

    protected override DBase __NewDBase(string id_or_rid)
    {
      return new MissionDBase(id_or_rid);
    }
  }
}