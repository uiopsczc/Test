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
      string[] all_assetBundle_names = manifest.GetAllAssetBundles();
      foreach (var assetBundle_name in all_assetBundle_names)
      {
        //寻找项目中assetBundle_name为assetBundle_name的asset的路径，以Asset/开头
        string[] asset_paths = AssetDatabase.GetAssetPathsFromAssetBundle(assetBundle_name);
        foreach (string asset_path in asset_paths)
        {
          string content = string.Format("{0}{1}{2}", assetBundle_name, StringConst.String_Comma, asset_path);
          contentList.Add(content);
        }
      }

      contentList.Sort();

      string fileOutputPath = BuildConst.AssetPathMap_File_Name.WithRootPath(BuildConst.Output_Path);

      StdioUtil.WriteTextFile(new FileInfo(fileOutputPath), contentList, false);
    }




  }
}