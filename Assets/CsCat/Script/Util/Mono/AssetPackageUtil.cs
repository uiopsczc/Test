namespace CsCat
{
	public class AssetPackageUtil
	{
		public static string AssetsPackagePathToAssetsPath(string assetPackagePath)
		{
			return assetPackagePath.WithRootPath(BuildConst.AssetsPackage_Root);
		}

		public static bool IsAssetsPackagePath(string assetPath)
		{
			return assetPath.IndexOf(BuildConst.AssetsPackage_Root) != -1;
		}

		public static string AssetsPathToAssetsPackagePath(string assetPath)
		{
			int index = assetPath.IndexEndOf(BuildConst.AssetsPackage_Root);
			if (index != -1)
				return assetPath.Substring(index + 1);
			LogCat.LogError("Asset path is not a package path!");
			return assetPath;
		}
	}
}