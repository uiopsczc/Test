using System;
using UnityEngine;

namespace CsCat
{
	public class GUILabelAlignScope : IDisposable
	{
		private readonly TextAnchor _old;

		public GUILabelAlignScope(TextAnchor alignment)
		{
			_old = GUI.skin.label.alignment;
			GUI.skin.label.alignment = alignment;
		}

		public void Dispose()
		{
			GUI.skin.label.alignment = _old;
		}
	}
}