using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CsCat
{
    public static class FilePathConst
    {
        #region ProjectPath

        private static string _ProjectPath;

        public static string ProjectPath => _ProjectPath ??
                                            (_ProjectPath = Application.dataPath.Replace(StringConst.String_Assets,
                                                StringConst.String_Empty));

        #endregion

        #region dataPath

        private static string _DataPath;
        public static string DataPath = _DataPath ?? (_DataPath = Application.dataPath + StringConst.String_Slash);

        #endregion

        #region AssetPath

        public static string AssetsPath = DataPath;

        #endregion

        #region streamingAssetsPath

        private static string _StreamingAssetsPath;

        public static string StreamingAssetsPath = _StreamingAssetsPath ??
                                                   (_StreamingAssetsPath =
                                                       Application.streamingAssetsPath + StringConst.String_Slash);

        #endregion

        #region persistentDataPath

        private static string _PersistentDataPath;

        public static string PersistentDataPath = _PersistentDataPath ??
                                                  (_PersistentDataPath =
                                                      Application.persistentDataPath + StringConst.String_Slash);

        #endregion

        #region AssetBundlePath

        private static string _AssetBundlesPath;

        public static string AssetBundlesPath = AssetBundlesPath ?? (AssetBundlesPath =
                                                    StreamingAssetsPath + BuildConst.AssetBundle_Folder_Name +
                                                    StringConst.String_Slash);

        #endregion

        #region ResourcesPath

        private static string _ResourcesPath;

        public static string ResourcesPath = _ResourcesPath ??
                                             (_ResourcesPath =
                                                 AssetsPath + StringConst.String_Resources + StringConst.String_Slash);

        public const string ResourcesFlag =
            StringConst.String_Slash + StringConst.String_Resources + StringConst.String_Slash;

        #endregion

        #region SpritesPath

        private static string _SpritesPath;
        public static string SpritesPath = ResourcesPath + StringConst.String_Sprites + StringConst.String_Slash;

        #endregion


        #region assetBundlesMainfest

        private static string _AssetBundlesManifest;

        public static string AssetBundlesManifest =
            _AssetBundlesManifest ?? (_AssetBundlesManifest = AssetBundlesPath + StringConst.String_Manifest);

        #endregion

        #region ExesPath 执行路径

        private static string _ExesPath;

        public static string ExesPath =
            _ExesPath ?? (_ExesPath = AssetsPath + StringConst.String_Exes + StringConst.String_Slash);

        #endregion


        #region ExternalScriptsPath 脚本路径

        private static string _ExternalScriptsPath;

        public static string ExternalScriptsPath =
            ProjectPath + StringConst.String_ExternalScripts + StringConst.String_Slash;

        #endregion

        #region AssetBundlesBuildOutputPath

        private static string __AssetBundlesBuildOutputPath;

        private static string _AssetBundlesBuildOutputPath =
            __AssetBundlesBuildOutputPath ?? (__AssetBundlesBuildOutputPath =
                Path.Combine(ProjectPath, BuildConst.AssetBundle_Folder_Name));

        public static string AssetBundlesBuildOutputPath
        {
            get
            {
                StdioUtil.CreateDirectoryIfNotExist(_AssetBundlesBuildOutputPath);
                return _AssetBundlesBuildOutputPath;
            }
        }

        #endregion


        #region PathBases  Unity所有资源脚本保存数据的路径

        public static List<string> RootPathList
        {
            get
            {
                var result = new List<string>
                {
                    ExternalPath,
                    ExesPath,
                    ExternalScriptsPath,
                    AssetBundlesPath,
                    SpritesPath,
                    ResourcesPath
                };
                //由外而内
                return result;
            }
        }

        #endregion

        public static string GetPathStartWithRelativePath(string path, string relativePath)
        {
            path = GetPathRelativeTo(path, relativePath);
            path = relativePath + path;
            return path;
        }

        public static string GetPathRelativeTo(string path, string relativePath)
        {
            var index = path.IndexEndOf(relativePath);
            if (index != -1)
                path = path.Substring(index + 1);
            return path;
        }

        public static string _PersistentAssetBundleRoot;

        public static string PersistentAssetBundleRoot = _PersistentAssetBundleRoot ?? (_PersistentAssetBundleRoot =
                                                             PersistentDataPath + BuildConst.AssetBundle_Folder_Name +
                                                             StringConst.String_Slash);

        public const string EditorAssetsPath = "Assets/Editor/EditorAssets/";

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