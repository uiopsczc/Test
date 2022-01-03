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
		[MenuItem(CZMToolConst.Menu_Root + "GameData/Open Folder")]
		public static void GameDataOpenFolder()
		{
			Process.Start("explorer.exe", FilePathConst.PersistentDataPath.Replace("/", "\\"));
		}

		[MenuItem(CZMToolConst.Menu_Root + "GameData/Clear/cs端")]
		public static void GameDataClear_cs()
		{
			LogCat.ClearLogs();
			File.Delete(SerializeDataConst.SaveFilePathCS);
			File.Delete(SerializeDataConst.SaveFilePathCS2);
			EditorUtilityCat.DisplayDialog(string.Format("{0} Clear Finished\n{1} Clear Finished",
				SerializeDataConst.SaveFilePathCS, SerializeDataConst.SaveFilePathCS2));
		}

		[MenuItem(CZMToolConst.Menu_Root + "GameData/Clear/lua端")]
		public static void GameDataClear_lua()
		{
			LogCat.ClearLogs();
			File.Delete(SerializeDataConst.SaveFilePathLua);
			EditorUtilityCat.DisplayDialog(string.Format("{0} Clear Finished", SerializeDataConst.SaveFilePathLua));
		}

		[MenuItem(CZMToolConst.Menu_Root + "GameData/Clear/All")]
		public static void GameDataClear_all()
		{
			LogCat.ClearLogs();
			File.Delete(SerializeDataConst.SaveFilePathCS);
			File.Delete(SerializeDataConst.SaveFilePathCS2);
			File.Delete(SerializeDataConst.SaveFilePathLua);

			EditorUtilityCat.DisplayDialog(string.Format("{0} Clear Finished\n{1} Clear Finished\n{2} Clear Finished",
				SerializeDataConst.SaveFilePathCS, SerializeDataConst.SaveFilePathCS2,
				SerializeDataConst.SaveFilePathLua));
		}
	}
}