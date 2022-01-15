namespace CsCat
{
	public class AssetPathInfo
	{
		public string mainAssetPath;
		public string subAssetPath;

		public AssetPathInfo(string path)
		{
			var paths = path.Split(":");
			mainAssetPath = paths[0];
			if (paths.Length > 1)
				subAssetPath = paths[1];
		}
	}
}