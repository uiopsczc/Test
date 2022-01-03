using System;
using UnityEngine;

namespace CsCat
{
	/// <summary>
	///   GUI.contentColor   只是text的color
	/// </summary>
	public class GUIContentColorScope : IDisposable
	{
		[SerializeField] private Color _preColor { get; }

		public GUIContentColorScope(Color newColor)
		{
			_preColor = GUI.contentColor;
			GUI.contentColor = newColor;
		}


		public void Dispose()
		{
			GUI.contentColor = _preColor;
		}
	}
}