using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using System;
using UnityEditor;

namespace CsCat
{
	public partial class EditorGUILayoutUtil
	{
		public static object Field(string label, Type type, object fieldValue)
		{
			if (type == typeof(string)) return EditorGUILayout.TextField(label, (string)fieldValue);
			if (type == typeof(float)) return EditorGUILayout.FloatField(label, (float)fieldValue);
			if (type == typeof(int)) return EditorGUILayout.IntField(label, (int)fieldValue);
			if (type == typeof(Bounds)) return EditorGUILayout.BoundsField(label, (Bounds)fieldValue);

			if (type == typeof(Color)) return EditorGUILayout.ColorField(label, (Color)fieldValue);
			if (type == typeof(AnimationCurve)) return EditorGUILayout.CurveField(label, (AnimationCurve)fieldValue);
			if (type == typeof(Gradient)) return EditorGUILayout.GradientField(label, (Gradient)fieldValue);
			if (type == typeof(long)) return EditorGUILayout.LongField(label, (long)fieldValue);
			if (fieldValue is Object o)
				using (new EditorGUILayoutBeginHorizontalScope())
				{
					EditorGUILayout.LabelField(label, GUILayout.Width(100));
					object result = EditorGUILayout.ObjectField(o, type);
					return result;
				}

			if (type == typeof(Rect)) return EditorGUILayout.RectField(label, (Rect)fieldValue);
			if (type == typeof(Vector2)) return EditorGUILayout.Vector2Field(label, (Vector2)fieldValue);
			if (type == typeof(Vector3)) return EditorGUILayout.Vector3Field(label, (Vector3)fieldValue);
			LogCat.LogError(string.Format("不支持该类型的field:{0}", type));
			return null;
		}

		public static T Field<T>(string label, object fieldValue)
		{
			return (T)Field(label, typeof(T), fieldValue);
		}
	}
}
#endif