using UnityEditor;
using UnityEngine;

namespace CsCat
{
	/// <summary>
	///   CZM工具菜单
	/// </summary>
	public partial class CZMToolMenu
	{
		[MenuItem(CZMToolConst.Menu_Root + "编辑器/IconGUIContent样式")]
		public static void CZMToolMenu_EditorIconGUIContentEditorWindow()
		{
			EditorIconGUIContentEditorWindow window =
				EditorWindow.GetWindow<EditorIconGUIContentEditorWindow>("编辑器IconGUIContent样式参考", true);
			window.minSize = new Vector2(1080f, 800f);
			window.autoRepaintOnSceneChange = true;
		}

		[MenuItem(CZMToolConst.Menu_Root + "编辑器/IconTexture样式")]
		public static void CZMToolMenu_EditorIconTextureEditorWindow()
		{
			EditorIconTextureEditorWindow window =
				EditorWindow.GetWindow<EditorIconTextureEditorWindow>("编辑器IconTexture样式参考", true);
			window.minSize = new Vector2(1080f, 800f);
			window.autoRepaintOnSceneChange = true;
		}

		[MenuItem(CZMToolConst.Menu_Root + "编辑器/EditorStyles样式")]
		public static void CZMToolMenu_EditorStylesEditorWindow()
		{
			EditorStylesEditorWindow window =
				EditorWindow.GetWindow<EditorStylesEditorWindow>("编辑器EditorStyles样式参考", true);
			window.minSize = new Vector2(1080f, 800f);
			window.autoRepaintOnSceneChange = true;
		}
	}
}