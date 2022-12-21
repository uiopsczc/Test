#if UNITY_EDITOR
using System;
using UnityEditor;

namespace CsCat
{
	public class EditorGUIUtilityLabelWidthScope : IDisposable
	{
		private readonly float _orgLabelWidth;

		public EditorGUIUtilityLabelWidthScope(float newLabelWidth)
		{
			_orgLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = newLabelWidth;
		}

		public void Dispose()
		{
			EditorGUIUtility.labelWidth = _orgLabelWidth;
		}
	}
}
#endif