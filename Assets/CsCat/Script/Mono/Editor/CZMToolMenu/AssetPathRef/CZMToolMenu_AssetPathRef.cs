using System.IO;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	/// <summary>
	///   CZM工具菜单
	/// </summary>
	public partial class CZMToolMenu
	{
		[MenuItem(CZMToolConst.Menu_Root + "AssetPathRef/Save")]
		public static void AssetPathRefSave()
		{
			AssetPathRefManager.instance.Save();
			AssetDatabase.Refresh();
			EditorUtilityCat.DisplayDialog("AssetPathRef Saved");
		}

		[MenuItem(CZMToolConst.Menu_Root + "AssetPathRef/Add")]
		public static void AssetPathRefAdd()
		{
			foreach (var selectedObj in Selection.objects)
				AssetPathRefManager.instance.Add(selectedObj.GetGUID());
			AssetPathRefManager.instance.Save();
			AssetDatabase.Refresh();
		}

		[MenuItem(CZMToolConst.Menu_Root + "AssetPathRef/AddByFolder")]
		public static void AssetPathRefAddByFolder()
		{
			Object[] selectedAssets = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
			foreach (Object selectedAsset in selectedAssets)
			{
				var assetPath = AssetDatabase.GetAssetPath(selectedAsset);
				if (Directory.Exists(assetPath))
					continue;
				AssetPathRefManager.instance.Add(AssetDatabase.AssetPathToGUID(assetPath));
			}

			AssetPathRefManager.instance.Save();
			AssetDatabase.Refresh();
		}


		[MenuItem(CZMToolConst.Menu_Root + "AssetPathRef/Clear All")]
		public static void AssetPathRefClearAll()
		{
			AssetPathRefManager.instance.ClearAll();
			AssetDatabase.Refresh();
			EditorUtilityCat.DisplayDialog("AssetPathRef Clear All");
		}
	}
}