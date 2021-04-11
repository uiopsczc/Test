namespace CsCat
{
  public class SceneFactory : DoerFactory
  {
    protected override string default_doer_class_path
    {
      get { return "CsCat.Scene"; }
    }

    public override ExcelAssetBase GetDefinitions()
    {
      return DefinitionManager.instance.sceneDefinition;
    }

    public override ExcelAssetBase GetDefinition(string id)
    {
      return GetDefinitions().GetData<SceneDefinition>(id);
    }

    public SceneDefinition GetSceneDefinition(string id)
    {
      return GetDefinition(id) as SceneDefinition;
    }

    protected override DBase __NewDBase(string id_or_rid)
    {
      return new SceneDBase(id_or_rid);
    }
  }
}