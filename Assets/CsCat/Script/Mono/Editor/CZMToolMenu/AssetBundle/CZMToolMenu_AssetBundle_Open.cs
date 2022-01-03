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
		[MenuItem(CZMToolConst.Menu_Root + "AssetBundle/Open/Server")]
		public static void AssetBundleOpenServer()
		{
			Process.Start("explorer.exe", BuildConst.Output_Path.Replace("/", "\\"));
		}

		[MenuItem(CZMToolConst.Menu_Root + "AssetBundle/Open/PC_Persistent")]
		public static void AssetBundleOpenPC()
		{
			Process.Start("explorer.exe", FilePathConst.PersistentAssetBundleRoot.Replace("/", "\\"));
		}
	}
}