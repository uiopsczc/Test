#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CsCat
{
	public class EditorGUIInspectorTitlebarScope
	{
		public static bool InspectorTitlebar(Rect position, ref bool isFoldout, Object targetObj, bool isExpandable)
		{
			isFoldout = EditorGUI.InspectorTitlebar(position, isFoldout, targetObj, isExpandable);
			return isFoldout;
		}

		public static bool InspectorTitlebar(Rect position, ref bool isFoldout, Object[] targetObjs, bool isExpandable)
		{
			isFoldout = EditorGUI.InspectorTitlebar(position, isFoldout, targetObjs, isExpandable);
			return isFoldout;
		}
	}
}
#endif