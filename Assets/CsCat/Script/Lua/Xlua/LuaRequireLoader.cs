using System.Collections;
using UnityEngine;

namespace CsCat
{
	public class LuaRequireLoader
	{
		/// <summary>
		/// </summary>
		/// <param name="relative_file_path"></param>
		/// <returns></returns>
		public static byte[] GetLoader(ref string relative_file_path)
		{
			var asset_path = XLuaConst.Lua_Root_Folder + relative_file_path;
			asset_path = asset_path.Replace(".", "/");
			asset_path = asset_path + BuildConst.Lua_Suffix;


			var assetCat = Client.instance.assetBundleManager.GetOrLoadAssetCat(asset_path);
			return assetCat.Get<TextAsset>().text.GetBytes();
		}

		public static IEnumerator LoadLuaFiles()
		{
			if (Application.isEditor && EditorModeConst.IsEditorMode)
				yield break;
			var assetBundle_name_list = Client.instance.assetBundleManager.assetPathMap.GetLuaAssetBundlePathList();

			using (new StopwatchScope("Lua Load"))
			{
				foreach (var assetBundle_name in assetBundle_name_list)
				{
					var asset_path_list = Client.instance.assetBundleManager.assetPathMap.GetAllAssetPathList(assetBundle_name);
					Client.instance.assetBundleManager.SetResidentAssets(asset_path_list, true);
					foreach (var asset_path in asset_path_list)
						Client.instance.assetBundleManager.LoadAssetAsync(asset_path);
				}

				yield return new WaitUntil(
				  () => { return Client.instance.assetBundleManager.resourceWebRequester_all_dict.Count == 0; });
			}

			Debug.LogWarning("LoadLuaFiles finished");
		}
	}
}