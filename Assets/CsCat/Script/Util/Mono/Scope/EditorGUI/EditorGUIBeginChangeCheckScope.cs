#if UNITY_EDITOR
using System;
using UnityEditor;

namespace CsCat
{
	public class EditorGUIBeginChangeCheckScope : IDisposable
	{
		private bool isEndChangeCheck = false;
		private bool isChanged;


		public EditorGUIBeginChangeCheckScope()
		{
			EditorGUI.BeginChangeCheck();
		}

		public bool IsChanged
		{
			get
			{
				if (isEndChangeCheck) return isChanged;
				isChanged = EditorGUI.EndChangeCheck();
				isEndChangeCheck = true;

				return isChanged;
			}
		}

		public void Dispose()
		{
			if (isEndChangeCheck)
				return;
			isChanged = EditorGUI.EndChangeCheck();
			isEndChangeCheck = true;
		}
	}
}
#endif