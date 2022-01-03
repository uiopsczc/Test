using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	/// <summary>
	///   CZM工具菜单
	/// </summary>
	public partial class CZMToolMenu : MonoBehaviour
	{
		[MenuItem(CZMToolConst.Menu_Root + "Protobuf/生成")]
		public static void ProtobufGenerate()
		{
			Process.Start(FilePathConst.ProjectPath + "py_tools/protobuf/" + "Protobuf.bat");
		}

		[MenuItem(CZMToolConst.Menu_Root + "Protobuf/Open protos dir")]
		public static void OpenProtosDir()
		{
			Process.Start("explorer.exe", FilePathConst.ProjectPath.Replace("/", "\\") + "py_tools\\protobuf\\protos");
		}
	}
}