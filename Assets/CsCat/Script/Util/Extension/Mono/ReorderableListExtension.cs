#if UNITY_EDITOR
using UnityEditorInternal;
using UnityEngine;

namespace CsCat
{
	public static class ReorderableListExtension
	{
		public static void DrawGUI(this ReorderableList self, GUIToggleTween toggleTween, string title)
		{
			ReorderableListUtil.DrawGUI(self, toggleTween, title);
		}

		public static void SetElementHeight(this ReorderableList reorderableList, float element_height)
		{
			ReorderableListUtil.SetElementHeight(reorderableList, element_height);
		}
	}
}
#endif