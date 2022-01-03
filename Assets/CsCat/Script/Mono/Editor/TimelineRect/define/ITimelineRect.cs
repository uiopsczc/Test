using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public interface ITimelineRect
	{
		void OnDrawTracks(TimelineRect timelineRect);
		bool OnTrySelectAnyone(TimelineRect timelineRect);
		void OnEditorMouseInputStatusChange(TimelineRect timelineRect);
		void OnMouseRightClick(TimelineRect timelineRect);
		void OnPlayTimeChange(TimelineRect timelineRect);
		void OnAnimating(TimelineRect timelineRect);
	}
}