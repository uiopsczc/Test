using System;
#if UNITY_EDITOR
using UnityEditor;

namespace CsCat
{
	public class EditorGUIIndentLevelScope : IDisposable
	{
		private readonly int _add;

		public EditorGUIIndentLevelScope(int add = 1)
		{
			this._add = add;
			EditorGUI.indentLevel += add;
		}

		public void Dispose()
		{
			EditorGUI.indentLevel -= _add;
		}
	}
}
#endif