

namespace CsCat
{
  public class AssetPackageUtil
  {

    public static string AssetsPackagePathToAssetsPath(string assetPackage_path)
    {
      return assetPackage_path.WithRootPath(BuildConst.AssetsPackage_Root);
    }

    public static bool IsAssetsPackagePath(string asset_path)
    {
      return asset_path.IndexOf(BuildConst.AssetsPackage_Root) != -1;
    }

    public static string AssetsPathToAssetsPackagePath(string asset_path)
    {
      int index = asset_path.IndexEndOf(BuildConst.AssetsPackage_Root);
      if (index != -1)
        return asset_path.Substring(index + 1);
      else
      {
        LogCat.LogError("Asset path is not a package path!");
        return asset_path;
      }
    }





  }


}