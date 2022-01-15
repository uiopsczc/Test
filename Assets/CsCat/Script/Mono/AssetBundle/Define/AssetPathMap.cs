using System;
using System.Collections.Generic;

namespace CsCat
{
	public class AssetPathMap
	{
		//key是AssetBundlePath,value是AssetPath的list
		protected ValueListDictionary<string, string> assetBundleName2AssetPathListDict =
		  new ValueListDictionary<string, string>();

		//key是AssetPath Value是AssetBundlePath
		protected Dictionary<string, string> assetPath2AssetBundleNameDict = new Dictionary<string, string>();
		protected List<string> emptyList = new List<string>();

		private string fileContent;

		public AssetPathMap()
		{
			assetPath = AssetPackageUtil.AssetsPackagePathToAssetsPath(BuildConst.AssetPathMap_File_Name);
			assetBundleName = AssetBundleUtil.AssetBundlePathToAssetBundleName(assetPath);
		}

		public string assetBundleName { get; protected set; }
		public string assetPath { get; protected set; }

		public void SaveToDisk()
		{
			var path = BuildConst.AssetPathMap_File_Name.WithRootPath(FilePathConst.PersistentAssetBundleRoot);
			StdioUtil.WriteTextFile(path, fileContent);
		}


		public void Initialize(string content)
		{
			if (content.IsNullOrWhiteSpace())
			{
				LogCat.LogError("ResourceNameMap empty!!");
				return;
			}

			fileContent = content;
			content = content.Replace("\r\n", "\n");
			var mapList = content.Split('\n');
			for (var i = 0; i < mapList.Length; i++)
			{
				var map = mapList[i];
				if (map.IsNullOrWhiteSpace())
					continue;

				var splits = map.Split(new[] {StringConst.String_Comma}, StringSplitOptions.None);
				if (splits.Length < 2)
				{
					LogCat.LogError("splitArr length < 2 : " + map);
					continue;
				}

				var item = new AssetPathItem();
				// 如：UI/Prefab/Login.assetbundle
				item.assetBundleName = splits[0];
				// 如：Assets/AssetsPackage/UI/Prefab/Login.prefab
				item.assetPath = splits[1];

				assetBundleName2AssetPathListDict.Add(item.assetBundleName, item.assetPath, true);
				assetPath2AssetBundleNameDict.Add(item.assetPath, item.assetBundleName);
			}
		}

		public List<string> GetLuaAssetBundlePathList()
		{
			var result = new List<string>();
			foreach (var keyValue in assetBundleName2AssetPathListDict)
			{
				var assetBundleName = keyValue.Key;
				if (assetBundleName.StartsWith(BuildConst.LuaBundlePrefixName))
					result.Add(assetBundleName);
			}

			return result;
		}


		public List<string> GetAllAssetPathList(string assetBundleName)
		{
			assetBundleName2AssetPathListDict.TryGetValue(assetBundleName, out var assetList);
			return assetList ?? emptyList;
		}

		public string GetAssetBundleName(string assetPath)
		{
			assetPath2AssetBundleNameDict.TryGetValue(assetPath, out var assetBundleName);
			return assetBundleName;
		}

		public bool IsContainsAssetPath(string assetPath)
		{
			return this.GetAssetBundleName(assetPath) != null;
		}
	}
}