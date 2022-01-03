using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class CZMToolMenu
	{
		[MenuItem(CZMToolConst.Menu_Root + "/EditorMode/设置为EditorMode")]
		public static void SetToEditorMode()
		{
			EditorModeConst.IsEditorMode = true;
		}

		[MenuItem(CZMToolConst.Menu_Root + "/EditorMode/设置为EditorMode", true)]
		public static bool CanSetToEditorMode()
		{
			return !EditorModeConst.IsEditorMode;
		}

		[MenuItem(CZMToolConst.Menu_Root + "/EditorMode/设置为SimulationMode")]
		public static void SetToSimulationMode()
		{
			EditorModeConst.IsEditorMode = false;
		}

		[MenuItem(CZMToolConst.Menu_Root + "/EditorMode/设置为SimulationMode", true)]
		public static bool CanSetToSimulationMode()
		{
			return EditorModeConst.IsEditorMode;
		}
	}
}