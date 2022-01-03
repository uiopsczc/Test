namespace CsCat
{
	public static class AssetBuildInfoManagerTest
	{
		public static void Test()
		{
			AssetBuildInfoManager.Instance.Clear();
			AssetBuildInfoManager.Instance.AddRootTarget(
				"Assets/CsCat/Script/Mono/Editor/AssetBundleBuild/AssetBuildInfo/Test/Prefabs/A.prefab");
			AssetBuildInfoManager.Instance.SetAssetBundlePath();
		}
	}
}