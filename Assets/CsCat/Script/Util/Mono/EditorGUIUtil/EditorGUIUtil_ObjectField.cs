using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
	public partial class EditorGUIUtil
	{
		public static T ObjectField<T>(Rect position, Object obj, bool isAllowSceneObjects) where T : Object
		{
			return (T)EditorGUI.ObjectField(position, obj, typeof(T), isAllowSceneObjects);
		}

		public static T ObjectField<T>(Rect position, string label, Object obj, bool isAllowSceneObjects)
			where T : Object
		{
			return (T)EditorGUI.ObjectField(position, label, obj, typeof(T), isAllowSceneObjects);
		}

		public static T ObjectField<T>(Rect position, GUIContent label, Object obj, bool isAllowSceneObjects)
			where T : Object
		{
			return (T)EditorGUI.ObjectField(position, obj, typeof(T), isAllowSceneObjects);
		}
	}
}
#endif