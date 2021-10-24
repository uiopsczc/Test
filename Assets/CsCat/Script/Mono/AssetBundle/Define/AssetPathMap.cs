using System;
using System.Collections.Generic;

namespace CsCat
{
  public class AssetPathMap
  {
    //key是AssetBundlePath,value是AssetPath的list
    protected ValueListDictionary<string, string> assetBundleName_2_assetPathList_Dict =
      new ValueListDictionary<string, string>();

    //key是AssetPath Value是AssetBundlePath
    protected Dictionary<string, string> assetPath_2_assetBundleName_Dict = new Dictionary<string, string>();
    protected List<string> empty_list = new List<string>();

    private string file_content;

    public AssetPathMap()
    {
      asset_path = AssetPackageUtil.AssetsPackagePathToAssetsPath(BuildConst.AssetPathMap_File_Name);
      assetBundle_name = AssetBundleUtil.AssetBundlePathToAssetBundleName(asset_path);
    }

    public string assetBundle_name { get; protected set; }
    public string asset_path { get; protected set; }

    public void SaveToDisk()
    {
      var path = BuildConst.AssetPathMap_File_Name.WithRootPath(FilePathConst.PersistentAssetBundleRoot);
      StdioUtil.WriteTextFile(path, file_content);
    }


    public void Initialize(string content)
    {
      if (content.IsNullOrWhiteSpace())
      {
        LogCat.LogError("ResourceNameMap empty!!");
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

        var item = new AssetPathItem();
        // 如：UI/Prefab/Login.assetbundle
        item.assetBundle_name = splits[0];
        // 如：Assets/AssetsPackage/UI/Prefab/Login.prefab
        item.asset_path = splits[1];

        assetBundleName_2_assetPathList_Dict.Add(item.assetBundle_name, item.asset_path, true);
        assetPath_2_assetBundleName_Dict.Add(item.asset_path, item.assetBundle_name);
      }
    }

    public List<string> GetLuaAssetBundlePathList()
    {
      var result = new List<string>();
      foreach (var assetBundle_name in assetBundleName_2_assetPathList_Dict.Keys)
        if (assetBundle_name.StartsWith(BuildConst.LuaBundle_Prefix_Name))
          result.Add(assetBundle_name);
      return result;
    }


    public List<string> GetAllAssetPathList(string assetBundle_name)
    {
      assetBundleName_2_assetPathList_Dict.TryGetValue(assetBundle_name, out var asset_list);
      return asset_list ?? empty_list;
    }

    public string GetAssetBundleName(string asset_path)
    {
      assetPath_2_assetBundleName_Dict.TryGetValue(asset_path, out var assetBundle_name);
      return assetBundle_name;
    }

    public bool IsContainsAssetPath(string asset_path)
    {
      return this.GetAssetBundleName(asset_path) != null;
    }
  }
}