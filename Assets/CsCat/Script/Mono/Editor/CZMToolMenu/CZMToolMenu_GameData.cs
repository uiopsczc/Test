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
    [MenuItem(CZMToolConst.MenuRoot + "GameData/Open Folder")]
    public static void GameDataOpenFolder()
    {
      Process.Start("explorer.exe", FilePathConst.PersistentDataPath.Replace("/", "\\"));
    }

    [MenuItem(CZMToolConst.MenuRoot + "GameData/Clear/cs端")]
    public static void GameDataClear_cs()
    {
      LogCat.ClearLogs();
      File.Delete(SerializeDataConst.Save_File_Path_cs);
      File.Delete(SerializeDataConst.Save_File_Path_cs2);
      LogCat.log(string.Format("{0} Clear Finished", SerializeDataConst.Save_File_Path_cs));
      LogCat.log(string.Format("{0} Clear Finished", SerializeDataConst.Save_File_Path_cs2));
    }

    [MenuItem(CZMToolConst.MenuRoot + "GameData/Clear/lua端")]
    public static void GameDataClear_lua()
    {
      LogCat.ClearLogs();
      File.Delete(SerializeDataConst.Save_File_Path_lua);
      LogCat.log(string.Format("{0} Clear Finished", SerializeDataConst.Save_File_Path_lua));
    }

    [MenuItem(CZMToolConst.MenuRoot + "GameData/Clear/All")]
    public static void GameDataClear_all()
    {
      LogCat.ClearLogs();
      File.Delete(SerializeDataConst.Save_File_Path_cs);
      File.Delete(SerializeDataConst.Save_File_Path_cs2);
      File.Delete(SerializeDataConst.Save_File_Path_lua);
      LogCat.log(string.Format("{0} Clear Finished", SerializeDataConst.Save_File_Path_cs));
      LogCat.log(string.Format("{0} Clear Finished", SerializeDataConst.Save_File_Path_cs2));
      LogCat.log(string.Format("{0} Clear Finished", SerializeDataConst.Save_File_Path_lua));
    }
  }
}