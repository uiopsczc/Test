using System;
using UnityEngine;

namespace CsCat
{
	/// <summary>
	///   GUI.matrix   GUI使用的矩阵
	/// </summary>
	public class GUIMatrixScope : IDisposable
	{
		[SerializeField] private Matrix4x4 _preMatrix { get; }

		public GUIMatrixScope(Matrix4x4 newMatrix)
		{
			_preMatrix = GUI.matrix;
			GUI.matrix = newMatrix;
		}


		public void Dispose()
		{
			GUI.matrix = _preMatrix;
		}
	}
}