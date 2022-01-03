using UnityEditor;
using Debug = UnityEngine.Debug;

namespace CsCat
{
	/// <summary>
	///   CZM工具菜单
	/// </summary>
	public partial class CZMToolMenu
	{
		[MenuItem(CZMToolConst.Menu_Root + "AssetBundle/Clear/All(Server,PC)")]
		public static void AssetBundleClearAll()
		{
			LogCat.ClearLogs();
			StdioUtil.ClearDir(BuildConst.Output_Path);
			StdioUtil.ClearDir(FilePathConst.PersistentAssetBundleRoot);
			EditorUtilityCat.DisplayDialog("AssetBundle PC_Persistent Clear Finished");
		}

		[MenuItem(CZMToolConst.Menu_Root + "AssetBundle/Clear/Server")]
		public static void AssetBundleClearServer()
		{
			LogCat.ClearLogs();
			StdioUtil.ClearDir(BuildConst.Output_Path);
			EditorUtilityCat.DisplayDialog("AssetBundle Server Clear Finished", BuildConst.Output_Path);
		}

		[MenuItem(CZMToolConst.Menu_Root + "AssetBundle/Clear/PC_Persistent")]
		public static void AssetBundleClearPC()
		{
			LogCat.ClearLogs();
			StdioUtil.ClearDir(FilePathConst.PersistentAssetBundleRoot);
			EditorUtilityCat.DisplayDialog("AssetBundle PC_Persistent Clear Finished",
				FilePathConst.PersistentAssetBundleRoot);
		}
	}
}