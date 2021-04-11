using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CsCat
{
  public static class FilePathConst
  {
    public const string File_Prefix = "file:///";

    #region ProjectPath

    public static string ProjectPath = Application.dataPath.Replace("Assets", "");

    #endregion

    #region dataPath

    public static string DataPath = Application.dataPath + "/";

    #endregion

    #region AssetPath

    public static string AssetsPath = DataPath;

    #endregion

    #region streamingAssetsPath

    public static string StreamingAssetsPath = Application.streamingAssetsPath + "/";

    #endregion

    #region persistentDataPath

    public static string PersistentDataPath = Application.persistentDataPath + "/";

    #endregion

    #region AssetBundlePath

    public static string AssetBundlesPath = StreamingAssetsPath + BuildConst.AssetBundle_Folder_Name + "/";

    #endregion

    #region ResourcesPath

    public static string ResourcesPath = AssetsPath + "Resources/";
    public static string Resources_Flag = "/Resources/";

    #endregion

    #region SpritesPath

    public static string SpritesPath = ResourcesPath + "Sprites/";

    #endregion

    

    #region assetBundlesMainfest

    public static string AssetBundlesMainfest = AssetBundlesPath + "Mainfest";

    #endregion

    #region ExesPath 执行路径

    public static string ExesPath = AssetsPath + "Exes/";

    #endregion


    #region ExternalScriptsPath 脚本路径

    public static string ExternalScriptsPath = ProjectPath + "ExternalScripts/";

    #endregion

    #region AssetBundlesBuildOutputPath

    public static string AssetBundlesBuildOutputPath
    {
      get
      {
        var outputPath = Path.Combine(ProjectPath, BuildConst.AssetBundle_Folder_Name);
        StdioUtil.CreateDirectoryIfNotExist(outputPath);
        return outputPath;
      }
    }

    #endregion


    #region PathBases  Unity所有资源脚本保存数据的路径

    public static List<string> RootPaths
    {
      get
      {
        var result = new List<string>();
        //由外而内
        result.Add(ExternalPath);
        result.Add(ExesPath);
        result.Add(ExternalScriptsPath);
        result.Add(AssetBundlesPath);
        result.Add(SpritesPath);
        result.Add(ResourcesPath);
        return result;
      }
    }

    #endregion

    public static string GetPathStartWithRelativePath(string path, string relative_path)
    {
      path = GetPathRelativeTo(path, relative_path);
      path = relative_path + path;
      return path;
    }

    public static string GetPathRelativeTo(string path, string relative_path)
    {
      var index = path.IndexEndOf(relative_path);
      if (index != -1)
        path = path.Substring(index + 1);
      return path;
    }


    public static string PersistentAssetBundleRoot = PersistentDataPath + BuildConst.AssetBundle_Folder_Name + "/";

    public static string EditorAssetsPath = "Assets/Editor/EditorAssets/";

    #region ExternalPath Unity外部路径

    public static string ExternalPath
    {
      get
      {
        var platform = Application.platform;
        switch (platform)
        {
          case RuntimePlatform.WindowsEditor:
            return AssetsPath + "Patch/";
          case RuntimePlatform.IPhonePlayer:
          case RuntimePlatform.Android:
            return PersistentDataPath;
          default:
            return AssetsPath + "Patch/";
        }
      }
    }

    #endregion

    #region ExcelsPath

    public static string ExcelsPath = Application.dataPath + "/Excels/";
    public static string ExcelAssetsPath = ResourcesPath + "data/excel_asset/";

    #endregion
  }
}