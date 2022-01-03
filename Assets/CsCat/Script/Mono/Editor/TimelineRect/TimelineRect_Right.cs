using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class TimelineRect
	{
		public void DrawRight()
		{
			SetDuration(this.duration);
			scrollRect = this.resizableRects.paddingRects[1];
			_viewRect.Set(0, 0, duration * widthPerSecond, scrollRect.height - 16);
			using (var s = new GUI.ScrollViewScope(scrollRect, _scrollPosition, _viewRect, true, false))
			{
				_scrollPosition = s.scrollPosition;
				DrawTimelineTrack();
				onDrawTracksRightSideCallback?.Invoke();
				DrawPlayTimeLine();
				HandleRightMouseInput();
				EditorMouseInput.DrawSelectBox();
			}

			HandleScrollWheelEvent();
		}

		void HandleScrollWheelEvent()
		{
			if (Event.current.type == EventType.ScrollWheel)
			{
				if (totalRect.Contains(Event.current.mousePosition))
					_widthSizePerSecond -= Event.current.delta.y / 100;
			}
		}
	}
}