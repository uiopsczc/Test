#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class EditorGUIUtil
	{
		public static bool ToggleIconButton(string label, bool value, GUIContent icon)
		{
			GUIStyle buttonStyle = new GUIStyle(StringConst.String_Button)
			{
				padding = new RectOffset(0, 0, 0, 0),
				margin = new RectOffset(0, 0, 0, 0)
			};

			Rect controlRect = EditorGUILayout.GetControlRect(true, 23f);
			Rect position = new Rect(controlRect.xMin + EditorGUIUtility.labelWidth, controlRect.yMin, 33f, 23f);
			GUIContent content = label.ToGUIContent();
			Vector2 vector = GUI.skin.label.CalcSize(content);
			Rect position2 = new Rect(position.xMax + 5f, controlRect.yMin + (controlRect.height - vector.y) * 0.5f,
				vector.x,
				controlRect.height);
			value = GUI.Toggle(position, value, icon, buttonStyle);
			GUI.Label(position2, label);
			return value;
		}
	}
}
#endif