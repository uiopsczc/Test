using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class TimelineRect
	{
		public void DrawLeftBottom()
		{
			Rect rect = new Rect(0, this.resizableRects.paddingRects[0].height - 20,
				this.resizableRects.paddingRects[0].width, 20);
			using (new GUILayout.AreaScope(rect))
			{
				using (new GUILayoutBeginHorizontalScope(EditorStyles.toolbar))
				{
					GUILayout.FlexibleSpace();
					GUILayout.Label(EditorMouseInput.status.ToString(),
						new GUIStyle(EditorStyles.toolbarButton) {normal = {textColor = new Color(0, 0, 0, 0.5f)}});
				}
			}
		}
	}
}