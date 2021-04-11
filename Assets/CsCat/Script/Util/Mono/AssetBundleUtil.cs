

using System.IO;

namespace CsCat
{
  public class AssetBundleUtil
  {
    public static string AssetBundlePathToAssetBundleName(string assetBundle_path)
    {
      if (!string.IsNullOrEmpty(assetBundle_path))
      {
        if (AssetPackageUtil.IsAssetsPackagePath(assetBundle_path))
          assetBundle_path = AssetPackageUtil.AssetsPathToAssetsPackagePath(assetBundle_path);
        //no " "
        assetBundle_path = assetBundle_path.Replace(" ", "");
        //there should not be any '.' in the assetbundle name
        //otherwise the variant handling in client may go wrong
        assetBundle_path = assetBundle_path.Replace(".", "_");
        //add after suffix ".assetbundle" to the end
        assetBundle_path = assetBundle_path + BuildConst.AssetBundle_Suffix;
        return assetBundle_path.ToLower();
      }

      return null;
    }


    public static string GetPersistentDataPath(string asset_path = null)
    {
      string output_path = Path.Combine(FilePathConst.PersistentDataPath, BuildConst.AssetBundle_Folder_Name);
      if (!string.IsNullOrEmpty(asset_path))
        output_path = Path.Combine(output_path, asset_path);
      return output_path;
    }

    public static string GetAssetBundleFileUrl(string file_path)
    {
      if (CheckPersistentFileExsits(file_path))
      {
        return GetPersistentFilePath(file_path);
      }
      else
      {
        return GetStreamingAssetsFilePath(file_path);
      }
    }

    public static bool CheckPersistentFileExsits(string file_path)
    {
      var path = GetPersistentDataPath(file_path);
      return File.Exists(path);
    }


    public static string GetPersistentFilePath(string asset_path = null)
    {
      return "file://" + GetPersistentDataPath(asset_path);
    }


    public static string GetStreamingAssetsFilePath(string asset_path = null)
    {
      //#if UNITY_EDITOR
      //        string outputPath = Path.Combine("file://" + Application.streamingAssetsPath, AssetBundleConst.FolderName);
      //#else
      //#if UNITY_IPHONE || UNITY_IOS
      //            string outputPath = Path.Combine("file://" + Application.streamingAssetsPath, AssetBundleConfig.AssetBundlesFolderName);
      //#elif UNITY_ANDROID
      //            string outputPath = Path.Combine(Application.streamingAssetsPath, AssetBundleConfig.AssetBundlesFolderName);
      //#else
      //            LogCat.LogError("Unsupported platform!!!");
      //#endif
      //#endif
      //        if (!string.IsNullOrEmpty(assetPath))
      //        {
      //            outputPath = Path.Combine(outputPath, assetPath);
      //        }
      //        return outputPath;
      return "";
    }

  }
}