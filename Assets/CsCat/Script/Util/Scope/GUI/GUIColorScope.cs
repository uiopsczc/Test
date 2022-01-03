using System;
using UnityEngine;

namespace CsCat
{
	/// <summary>
	///   GUI.color   包括TextColor&BackgroundColor
	/// </summary>
	public class GUIColorScope : IDisposable
	{
		[SerializeField] private Color _preColor { get; }

		public GUIColorScope(Color newColor) : this()
		{
			GUI.color = newColor;
		}

		public GUIColorScope()
		{
			_preColor = GUI.color;
		}

		public void Dispose()
		{
			GUI.color = _preColor;
		}
	}
}