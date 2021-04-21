namespace CsCat
{
  public class SceneDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/SceneDefinition"; }
    }

    public SceneDefinition GetData(string id)
    {
      return GetData<SceneDefinition>(id);
    }

    public SceneDefinition GetData(int id)
    {
      return GetData<SceneDefinition>(id);
    }

    public SceneMapInfo GetSceneMapInfo()
    {
      return null;
    }
  }
}


