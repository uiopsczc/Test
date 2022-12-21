using System;
using UnityEngine;

namespace CsCat
{
	public class GUILayoutBeginVerticalIndentLevelScope : IDisposable
	{
		public const float Width_Of_Per_Indent_Level = 10f;
		public static int Global_Indent_Level = 0;
		private int _add;

		public void _Init(int add = 1)
		{
			this._add = add;
			Global_Indent_Level += add;
			GUILayout.BeginHorizontal();
			GUILayout.Space(Global_Indent_Level * Width_Of_Per_Indent_Level);
			//      GUILayout.FlexibleSpace();
		}

		public GUILayoutBeginVerticalIndentLevelScope(int add = 1)
		{
			_Init(add);
			GUILayout.BeginVertical();
		}

		public GUILayoutBeginVerticalIndentLevelScope(int add = 1, params GUILayoutOption[] layoutOptions)
		{
			_Init(add);
			GUILayout.BeginVertical(layoutOptions);
		}

		public GUILayoutBeginVerticalIndentLevelScope(GUIStyle guiStyle, int add = 1,
			params GUILayoutOption[] layoutOptions)
		{
			_Init(add);
			GUILayout.BeginVertical(guiStyle, layoutOptions);
		}

		public void Dispose()
		{
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			Global_Indent_Level -= _add;
		}
	}
}