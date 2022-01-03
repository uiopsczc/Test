using UnityEditor;
using UnityEngine;

namespace CsCat
{
	/// <summary>
	///   CZM工具菜单
	/// </summary>
	public partial class CZMToolMenu
	{
		#region 辅助工具

		/// <summary>
		///   时间面板
		///   %t:crtl+t
		///   &t:alt+t
		///   #t:shift+t
		///   _t:t
		/// </summary>
		[MenuItem(CZMToolConst.Menu_Root + "辅助工具/显示时间设置 %&#t")]
		//crtl+alt+shift+t
		public static void ShowProjectSettings_Time()
		{
			//EditorApplication.ExecuteMenuItem("Edit/Project Settings...");
			var unityEditorAssembly = AssemblyUtil.GetAssembly("UnityEditor");
			unityEditorAssembly.GetType("UnityEditor.SettingsWindow")
				.InvokeMethod<object>("Show", false, SettingsScope.Project, "Project/Time");
		}

		[MenuItem(CZMToolConst.Asset_Menu_Root + "辅助工具/复制路径(相对于Project) %&#p")] //crtl+alt+shift+p  %&#p 
		[MenuItem(CZMToolConst.GameObject_Menu_Root + "辅助工具/复制路径")]
		public static void CopyAssetPath()
		{
			var objects = Selection.objects;
			if (objects != null)
			{
				var obj = objects[0];
				string str;
				if (obj.IsAsset())
					str = obj.GetAssetPath();
				else
					str = obj.As<GameObject>().transform.GetFullPath();
				EditorGUIUtility.systemCopyBuffer = str;
			}
		}

		[MenuItem(CZMToolConst.Asset_Menu_Root + "辅助工具/复制Resource路径")]
		private static void CopyResourceAssetPath()
		{
			var objects = Selection.objects;
			if (objects != null)
			{
				var str = "";
				var obj = objects[0];
				str = obj.GetAssetPath();
				str = str.Substring(str.LastIndexEndOf("Resources/") + 1);
				EditorGUIUtility.systemCopyBuffer = str;
			}
		}

		#endregion
	}
}