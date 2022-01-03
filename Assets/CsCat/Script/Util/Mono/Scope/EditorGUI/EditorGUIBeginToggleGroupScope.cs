using System;
#if UNITY_EDITOR
using UnityEditor;

namespace CsCat
{
	public class EditorGUIBeginToggleGroupScope : IDisposable
	{
		public bool toggle { get; set; }

		public EditorGUIBeginToggleGroupScope(bool isToggle, string name = StringConst.String_Empty)
		{
			toggle = EditorGUILayout.BeginToggleGroup(name, isToggle);
		}


		public void Dispose()
		{
			EditorGUILayout.EndToggleGroup();
		}
	}
}
#endif