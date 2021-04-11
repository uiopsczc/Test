using System.Diagnostics;
using System.IO;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace CsCat
{
  /// <summary>
  ///   CZM工具菜单
  /// </summary>
  public partial class CZMToolMenu
  {
    [MenuItem(CZMToolConst.MenuRoot + "Log/Open Folder")]
    public static void LogOpenFolder()
    {
      Process.Start("explorer.exe", LogCatConst.Log_Base_Path.Replace("/", "\\") + "");
    }

    [MenuItem(CZMToolConst.MenuRoot + "Log/Clear Log")]
    public static void LogClear()
    {
      LogCat.ClearLogs();
      StdioUtil.ClearDir(LogCatConst.Log_Base_Path);
      LogCat.log(string.Format("Clear Finished Dir:{0}", LogCatConst.Log_Base_Path));
    }
  }
}