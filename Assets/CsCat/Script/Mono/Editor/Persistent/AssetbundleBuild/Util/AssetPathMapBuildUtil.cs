using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace CsCat
{
	public class AssetPathMapBuildUtil
	{
		public static void Build(AssetBundleManifest manifest)
		{
			List<string> contentList = new List<string>();
			string[] allAssetBundleNames = manifest.GetAllAssetBundles();
			foreach (var assetBundleName in allAssetBundleNames)
			{
				//寻找项目中assetBundle_name为assetBundle_name的asset的路径，以Asset/开头
				string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleName);
				foreach (string assetPath in assetPaths)
				{
					string content = string.Format("{0}{1}{2}", assetBundleName, StringConst.String_Comma, assetPath);
					contentList.Add(content);
				}
			}

			contentList.Sort();

			string fileOutputPath = BuildConst.AssetPathMap_File_Name.WithRootPath(BuildConst.Output_Path);

			StdioUtil.WriteTextFile(new FileInfo(fileOutputPath), contentList, false);
		}
	}
}