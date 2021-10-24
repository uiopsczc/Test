using System;
using System.Collections.Generic;

namespace CsCat
{
  public class AssetBundleMap
  {
    public Dictionary<string, long> dict = new Dictionary<string, long>();
    private string file_content;

    public void SaveToDisk()
    {
      var path = BuildConst.AssetBundleMap_File_Name.WithRootPath(FilePathConst.PersistentAssetBundleRoot);
      StdioUtil.WriteTextFile(path, file_content);
    }

    public long GetAssetBundleBytes(string assetBundle_name)
    {
      return this.dict[assetBundle_name];
    }


    public void Initialize(string content)
    {
      if (content.IsNullOrWhiteSpace())
      {
        LogCat.LogError("AssetBundleMap empty!!");
        return;
      }

      file_content = content;
      content = content.Replace("\r\n", "\n");
      var map_list = content.Split('\n');
      foreach (var map in map_list)
      {
        if (map.IsNullOrWhiteSpace())
          continue;

        var splits = map.Split(new[] { StringConst.String_Comma }, StringSplitOptions.None);
        if (splits.Length < 2)
        {
          LogCat.LogError("splitArr length < 2 : " + map);
          continue;
        }

        string assetBundle_name = splits[0];
        long bytes = splits[1].To<long>();
        dict[assetBundle_name] = bytes;
      }
    }

  }
}