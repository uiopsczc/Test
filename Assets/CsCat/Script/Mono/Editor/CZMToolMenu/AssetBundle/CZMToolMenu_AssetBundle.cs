using System.Diagnostics;
using System.Linq;
using AssetBundleBrowser;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace CsCat
{
  /// <summary>
  ///   CZM工具菜单
  /// </summary>
  public partial class CZMToolMenu
  {
    [MenuItem(CZMToolConst.MenuRoot + "AssetBundle/AssetBundle Build")]
    public static void AssetBundleBuild()
    {
      LogCat.ClearLogs();
      //AssetBuildInfoManagerTest.Test();//需要自动设置依赖的，请在AssetBuildInfoManagerTest中添加Root文件，然后取消注释
      StdioUtil.CreateDirectoryIfNotExist(BuildConst.Output_Path);
      LuaBuildUtil.PreBuild();
      AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(BuildConst.Output_Path,
        BuildAssetBundleOptions.None,
        BuildTarget.Android);
      AssetPathMapBuildUtil.Build(manifest);
      AssetBundleMapBuildUtil.Build(manifest);
      ResVersionBuildUtil.Build();
      AssetPathRefBuildUtil.Build();
      var NetBox_Path = (BuildConst.Output_Base_Path + "/NetBox.exe").Replace("/", "\\");
      Process.Start(NetBox_Path);
      EditorUtilityCat.DisplayDialog(string.Format("AssetBundle Build Finished,output:\n  {0}", BuildConst.Output_Path), BuildConst.Output_Path);
    }

    [MenuItem(CZMToolConst.MenuRoot + "AssetBundle/AssetBundle Browser")]
    public static void ShowAssetBundleBrowser()
    {
      typeof(AssetBundleBrowserMain).InvokeMethod("ShowWindow");
    }
  }
}