using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class TimelineRectTestEditorWindow : EditorWindow
	{
		private TimelineRect _timelineRect;


		void Awake()
		{
			this._timelineRect = new TimelineRect(() => new Rect(0, 0, this.position.width, this.position.height));
		}

		public void OnEnable()
		{
			this._timelineRect.OnEnable();
		}

		public void OnDisable()
		{
			this._timelineRect.OnDisable();
		}


		void OnGUI()
		{
			this._timelineRect.OnGUI();
			Repaint();
		}
	}
}