using System.Collections;
using UnityEngine;

namespace CsCat
{
	public class LuaRequireLoader
	{
		/// <summary>
		/// </summary>
		/// <param name="relativeFilePath"></param>
		/// <returns></returns>
		public static byte[] GetLoader(ref string relativeFilePath)
		{
			var assetPath = XLuaConst.Lua_Root_Folder + relativeFilePath;
			assetPath = assetPath.Replace(".", "/");
			assetPath = assetPath + BuildConst.Lua_Suffix;


			var assetCat = Client.instance.assetBundleManager.GetOrLoadAssetCat(assetPath);
			return assetCat.Get<TextAsset>().text.GetBytes();
		}

		public static IEnumerator LoadLuaFiles()
		{
			if (Application.isEditor && EditorModeConst.IsEditorMode)
				yield break;
			var assetBundleNameList = Client.instance.assetBundleManager.assetPathMap.GetLuaAssetBundlePathList();

			using (new StopwatchScope("Lua Load"))
			{
				for (var i = 0; i < assetBundleNameList.Count; i++)
				{
					var assetBundleName = assetBundleNameList[i];
					var assetPathList =
						Client.instance.assetBundleManager.assetPathMap.GetAllAssetPathList(assetBundleName);
					Client.instance.assetBundleManager.SetResidentAssets(assetPathList, true);
					for (var j = 0; j < assetPathList.Count; j++)
					{
						var assetPath = assetPathList[j];
						Client.instance.assetBundleManager.LoadAssetAsync(assetPath);
					}
				}

				yield return new WaitUntil(
				  () => Client.instance.assetBundleManager.resourceWebRequesterAllDict.Count == 0);
			}

			Debug.LogWarning("LoadLuaFiles finished");
		}
	}
}