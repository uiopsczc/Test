using System;
using UnityEngine;

namespace CsCat
{
	public class GUIZoomGroupScope : IDisposable
	{
		private static Matrix4x4 _preGUIMatrix;


		public GUIZoomGroupScope(Rect guiRect, float zoomScale)
		{
			Rect rect = guiRect.ScaleBy(1f / zoomScale, guiRect.min);

			GUI.BeginGroup(rect);
			_preGUIMatrix = GUI.matrix;
			var lhs = Matrix4x4.TRS(rect.min, Quaternion.identity, Vector3.one);
			var one = Vector3.one;
			one.y = zoomScale;
			one.x = zoomScale;
			var rhs = Matrix4x4.Scale(one);
			GUI.matrix = lhs * rhs * lhs.inverse * GUI.matrix;
		}


		public void Dispose()
		{
			GUI.matrix = _preGUIMatrix;
			GUI.EndGroup();
		}
	}
}