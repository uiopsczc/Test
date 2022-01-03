using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CsCat
{
	public class AssetBundleMapBuildUtil
	{
		public static void Build(AssetBundleManifest manifest)
		{
			Dictionary<string, long> dict = new Dictionary<string, long>();
			string[] allAssetBundleNames = manifest.GetAllAssetBundles();
			foreach (var assetBundleName in allAssetBundleNames)
			{
				FileInfo fileInfo = new FileInfo(BuildConst.Output_Path + assetBundleName);
				dict[assetBundleName] = fileInfo.Length;
			}

			List<string> contentList = new List<string>();
			foreach (var assetBundleName in dict.Keys)
				contentList.Add(string.Format("{0},{1}", assetBundleName, dict[assetBundleName]));

			string fileOutputPath = BuildConst.AssetBundleMap_File_Name.WithRootPath(BuildConst.Output_Path);
			StdioUtil.WriteTextFile(new FileInfo(fileOutputPath), contentList, false);
		}
	}
}