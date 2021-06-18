namespace CsCat
{
  public class SceneFactory : DoerFactory
  {
    protected override string default_doer_class_path => "CsCat.Scene";

    protected override string GetClassPath(string id)
    {
      return this.GetCfgSceneData(id).class_path_cs.IsNullOrWhiteSpace() ? base.GetClassPath(id) : GetCfgSceneData(id).class_path_cs;
    }
    
    public CfgSceneData GetCfgSceneData(string id)
    {
      return CfgScene.Instance.get_by_id(id);
    }

    protected override DBase __NewDBase(string id_or_rid)
    {
      return new SceneDBase(id_or_rid);
    }
  }
}