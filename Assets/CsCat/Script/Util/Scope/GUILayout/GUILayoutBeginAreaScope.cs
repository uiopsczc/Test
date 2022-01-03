using System;
using UnityEngine;

namespace CsCat
{
	public class GUILayoutBeginAreaScope : IDisposable
	{
		public GUILayoutBeginAreaScope(Rect area)
		{
			GUILayout.BeginArea(area);
		}

		public GUILayoutBeginAreaScope(Rect area, string content)
		{
			GUILayout.BeginArea(area, content);
		}

		public GUILayoutBeginAreaScope(Rect area, string content, string style)
		{
			GUILayout.BeginArea(area, content, style);
		}

		public void Dispose()
		{
			GUILayout.EndArea();
		}
	}
}