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
		[MenuItem(CZMToolConst.Menu_Root + "Log/Open Folder")]
		public static void LogOpenFolder()
		{
			Process.Start("explorer.exe", LogCatConst.LogBasePath.Replace("/", "\\") + "");
		}

		[MenuItem(CZMToolConst.Menu_Root + "Log/Clear Log")]
		public static void LogClear()
		{
			LogCat.ClearLogs();
			StdioUtil.ClearDir(LogCatConst.LogBasePath);
			LogCat.log(string.Format("Clear Finished Dir:{0}", LogCatConst.LogBasePath));
		}
	}
}