

#if UNITY_EDITOR
using UnityEditorInternal;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class ReorderableListUtil
	{
		public static void ToReorderableList(IList to_reorder_list, ref ReorderableList _reorderableList)
		{
			if (_reorderableList != null) return;
			_reorderableList =
			  new ReorderableList(to_reorder_list, to_reorder_list.GetType().GetElementType(), true, false, true, true);
			_reorderableList.headerHeight = 0;
		}

		public static void DrawGUI(ReorderableList reorderableList, GUIToggleTween toggleTween, string titile)
		{
			using (new GUILayoutToggleAreaScope(toggleTween, titile))
			{
				reorderableList.DoLayoutList();
			}
		}

		public static void SetElementHeight(ReorderableList reorderableList, float element_height)
		{
			reorderableList.elementHeight = reorderableList.count == 0 ? EditorConst.Single_Line_Height : element_height;
		}


	}
}
#endif