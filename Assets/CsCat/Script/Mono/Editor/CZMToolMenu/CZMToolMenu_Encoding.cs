using System.IO;
using System.Text;
using UnityEditor;

namespace CsCat
{
	/// <summary>
	///   CZM工具菜单
	/// </summary>
	public partial class CZMToolMenu
	{
		[MenuItem(CZMToolConst.Menu_Root + "将所有cs文件保存为utf8编码")]
		public static void ConvertFilesEncoding()
		{
			string dirPath = FilePathConst.AssetsPath;
			//string dir_path = "F:/WorkSpace/Unity/Test/Assets/cscat/test";
			string[] filePaths = Directory.GetFiles(dirPath, "*.cs", SearchOption.AllDirectories);
			foreach (string filePath in filePaths)
			{
				SetFileEncoding(filePath.Replace('\\', '/'));
			}

			EditorUtilityCat.DisplayDialog("EncodingToUtf8 完成");
		}

		static void SetFileEncoding(string filePath, Encoding targetEncoding = null)
		{
			if (targetEncoding == null)
				targetEncoding = Encoding.UTF8;
			Encoding encoding = EncodingUtil.GetEncoding(filePath);
			//    LogCat.logError(file_path);
			//    LogCat.logError(target_encoding);
			var data = File.ReadAllBytes(filePath);
			data = targetEncoding.GetBytes(encoding.GetString(data));
			File.WriteAllBytes(filePath, data);
		}
	}
}