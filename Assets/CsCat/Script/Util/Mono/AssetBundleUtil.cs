using System.IO;

namespace CsCat
{
	public class AssetBundleUtil
	{
		public static string AssetBundlePathToAssetBundleName(string assetBundlePath)
		{
			if (!string.IsNullOrEmpty(assetBundlePath))
			{
				if (AssetPackageUtil.IsAssetsPackagePath(assetBundlePath))
					assetBundlePath = AssetPackageUtil.AssetsPathToAssetsPackagePath(assetBundlePath);
				//no " "
				assetBundlePath = assetBundlePath.Replace(StringConst.String_Space, StringConst.String_Empty);
				//there should not be any '.' in the assetbundle name
				//otherwise the variant handling in client may go wrong
				assetBundlePath = assetBundlePath.Replace(StringConst.String_Dot, StringConst.String_Underline);
				//add after suffix ".assetbundle" to the end
				assetBundlePath = assetBundlePath + BuildConst.AssetBundle_Suffix;
				return assetBundlePath.ToLower();
			}

			return null;
		}


		public static string GetPersistentDataPath(string assetPath = null)
		{
			string outputPath = Path.Combine(FilePathConst.PersistentDataPath, BuildConst.AssetBundle_Folder_Name);
			if (!string.IsNullOrEmpty(assetPath))
				outputPath = Path.Combine(outputPath, assetPath);
			return outputPath;
		}

		public static string GetAssetBundleFileUrl(string filePath)
		{
			return IsPersistentFileExist(filePath)
				? GetPersistentFilePath(filePath)
				: GetStreamingAssetsFilePath(filePath);
		}

		public static bool IsPersistentFileExist(string filePath)
		{
			var path = GetPersistentDataPath(filePath);
			return File.Exists(path);
		}


		public static string GetPersistentFilePath(string assetPath = null)
		{
			return StringConst.String_File_Url_Prefix + GetPersistentDataPath(assetPath);
		}


		public static string GetStreamingAssetsFilePath(string assetPath = null)
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