using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class AnimationTimelinableTestEditorWindow : EditorWindow
	{
		private HorizontalResizableRects _resizableRects;
		private TimelineRect _timelineRect;

		AnimationTimelinableSequence _sequence;


		void Awake()
		{
			this._resizableRects =
				new HorizontalResizableRects(() => new Rect(0, 0, this.position.width, this.position.height),
					new[] {300f});
			this._timelineRect = new TimelineRect(() => _resizableRects.rects[1]);
			this._timelineRect.onDrawTracksLeftSideCallback = () =>
				TimelinableEditorWindowUtil.OnDrawTracksLeftSideCallback(_sequence, _timelineRect);
			this._timelineRect.onDrawTracksRightSideCallback = () =>
				TimelinableEditorWindowUtil.OnDrawTracksRightSideCallback(_sequence, _timelineRect);

			this._timelineRect.isMouseDownOfSelectedItem = (mousePosition) =>
				TimelinableEditorWindowUtil.IsMouseDownOfSelectedItem(mousePosition, _sequence);
			this._timelineRect.tryToSelectUnselectedItemCallback = (mousePosition) =>
				TimelinableEditorWindowUtil.TryToSelectUnselectedItemCallback(mousePosition, _sequence);
			this._timelineRect.updateSelectedItemsCallback = (selecting_rect) =>
				TimelinableEditorWindowUtil.UpdateSelectedItemsCallback(selecting_rect, _sequence);
			this._timelineRect.onDraggingSelectedCallback = (deltaDuration) =>
				TimelinableEditorWindowUtil.OnDraggingSelectedCallback(deltaDuration, _sequence, _timelineRect.playTime);
			this._timelineRect.isHasSelectedItem = () => TimelinableEditorWindowUtil.IsHasSelectedItem(_sequence);

			this._timelineRect.onMouseRightButtonClickCallback = (positionRect) =>
				TimelinableEditorWindowUtil.OnMouseRightButtonClickCallback(positionRect, _sequence.tracks);

			this._timelineRect.onDoEditorCommandCallback = (editorCommand) =>
				TimelinableEditorWindowUtil.OnDoEditorCommandCallback(editorCommand, _sequence.tracks);

			this._timelineRect.onPlayTimeChangeCallback = (playTime) =>
				TimelinableEditorWindowUtil.OnPlayTimeChangeCallback(_sequence, playTime);
			this._timelineRect.onAnimatingCallback = OnAnimatingCallback;
		}

		public void OnEnable()
		{
			this._timelineRect.OnEnable();
		}

		public void OnDisable()
		{
			this._timelineRect.OnDisable();
			_sequence?.OnSequenceDisable();
		}


		void OnGUI()
		{
			this._resizableRects.OnGUI();
			DrawLeft();
			this._timelineRect.OnGUI();
			Repaint();
		}
	}
}