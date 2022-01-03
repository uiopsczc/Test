using System.IO;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class CZMToolMenu
	{
		[MenuItem(CZMToolConst.Menu_Root + "复制Assets\\Lua文件夹的文件到Assets\\Resources中")]
		public static void CopyLuaFilesToResourcesDir()
		{
			var fullPath = Application.dataPath + "\\Lua";
			var dir = new DirectoryInfo(fullPath);
			foreach (var child in dir.SearchFiles(f => f.Extension.Equals(".lua")))
				FileUtil.CopyFileOrDirectory(child.FullName, FilePathConst.ResourcesPath + "Lua/" + child.Name);
		}
	}
}