#if UNITY_EDITOR
#endif

namespace CsCat
{
  public static partial class BuildConst
  {
    #region AssetPathMap

    public const string AssetPathMap_File_Name = "AssetPathMap.bytes";

    #endregion

    public static string AssetsPackage_Root = "Assets/" + AssetsPackage_Folder_Name + "/";
    public static string Lua_Root = "Assets/" + "Lua" + "/";



    #region Mainifest

    public const string ManifestBundle_Path = AssetBundle_Folder_Name;
    public const string Manifest_Path = "AssetBundleManifest";

    #endregion

    #region AssetsPackage

    public const string AssetsPackage_Folder_Name = "AssetsPackage";

    #endregion

    #region AssetBundle

    public const string AssetBundle_Suffix = ".ab";
    public const string AssetBundle_Folder_Name = "AssetBundle";

    #endregion

    #region ResVersion

    public const string Res_Version_Default = "1.0.00000";
    public const string Res_Version_File_Name = "ResVersion.bytes";
    public const string Res_Version_File_Path = "Assets/" + Res_Version_File_Name;

    #endregion

    #region AssetBundleMap

    public const string AssetBundleMap_File_Name = "AssetBundleMap.bytes";
    public const string AssetBundleMap_File_Path = "Assets/" + AssetBundleMap_File_Name;

    #endregion

    #region Lua

    public const string LuaBundle_Prefix_Name = "@lua_"; //小写，assetBundle_path全部都是小写的
    public const string Lua_Suffix = ".lua.txt";

    public const string Lua_Path_Map_File_Name = "LuaPathMap.bytes";

    #endregion
  }
}