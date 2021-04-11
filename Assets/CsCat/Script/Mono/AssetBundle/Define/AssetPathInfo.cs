namespace CsCat
{
  public class AssetPathInfo
  {
    public string main_asset_path;
    public string sub_asset_path;

    public AssetPathInfo(string path)
    {
      var pathes = path.Split(":");
      main_asset_path = pathes[0];
      if (pathes.Length > 1)
        sub_asset_path = pathes[1];
    }
  }
}