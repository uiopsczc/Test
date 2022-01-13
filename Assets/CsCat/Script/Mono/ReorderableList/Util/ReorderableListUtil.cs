#if UNITY_EDITOR
using UnityEditorInternal;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class ReorderableListUtil
	{
		public static void ToReorderableList(IList toReorderList, ref ReorderableList reorderableList)
		{
			if (reorderableList != null) return;
			reorderableList =
				new ReorderableList(toReorderList, toReorderList.GetType().GetElementType(), true, false, true,
					true);
			reorderableList.headerHeight = 0;
		}

		public static void DrawGUI(ReorderableList reorderableList, GUIToggleTween toggleTween, string title)
		{
			using (new GUILayoutToggleAreaScope(toggleTween, title))
			{
				reorderableList.DoLayoutList();
			}
		}

		public static void SetElementHeight(ReorderableList reorderableList, float elementHeight)
		{
			reorderableList.elementHeight =
				reorderableList.count == 0 ? EditorConst.Single_Line_Height : elementHeight;
		}
	}
}
#endif