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
      string[] all_assetBundle_names = manifest.GetAllAssetBundles();
      foreach (var assetBundle_name in all_assetBundle_names)
      {
        FileInfo fileInfo = new FileInfo(BuildConst.Output_Path + assetBundle_name);
        dict[assetBundle_name] = fileInfo.Length;
      }

      List<string> contentList = new List<string>();
      foreach (var assetBundle_name in dict.Keys)
        contentList.Add(string.Format("{0},{1}", assetBundle_name, dict[assetBundle_name]));

      string fileOutputPath = BuildConst.AssetBundleMap_File_Name.WithRootPath(BuildConst.Output_Path);
      StdioUtil.WriteTextFile(new FileInfo(fileOutputPath), contentList, false);
    }


  }
}