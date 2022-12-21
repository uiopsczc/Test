#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class GUILayoutContentScope : IDisposable
	{
		private readonly float _old;
		private const string As_TextArea_String = "AS TextArea";

		public GUILayoutContentScope()
		{
			GUILayout.BeginHorizontal();
			GUILayout.BeginHorizontal(As_TextArea_String, GUILayout.MinHeight(10f));
			GUILayout.BeginVertical();
			GUILayout.Space(2f);
			_old = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = _old - 10;
		}

		public void Dispose()
		{
			GUILayout.Space(3f);
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.Space(3f);
			GUILayout.EndHorizontal();

			GUILayout.Space(3f);
			EditorGUIUtility.labelWidth = _old;
		}
	}
}
#endif