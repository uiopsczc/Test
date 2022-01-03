using System.IO;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	/// <summary>
	///   CZM工具菜单
	/// </summary>
	public partial class CZMToolMenu : MonoBehaviour
	{
		[MenuItem(CZMToolConst.Menu_Root + "退出所有的线程")]
		public static void AbortAllThreads()
		{
			ThreadManager.instance.Abort();
			LogCat.log("退出所有线程完成");
		}
	}
}