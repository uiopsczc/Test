using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class TimelineRect
	{
		bool isMouseLeftButtonPressed = false;

		void HandleRightMouseInput()
		{
			if (Event.current.isMouse)
			{
				if (this.resizableRects.isResizing)
					return;
				switch (Event.current.type)
				{
					case EventType.MouseDown:
						if (Event.current.button == 0)
						{
							isMouseLeftButtonPressed = true;
							EditorMouseInput.touchPoint = Event.current.mousePosition;
							EditorMouseInput.lastTouchPoint = EditorMouseInput.touchPoint;
							Rect timelineRect = new Rect(_viewRect);
							timelineRect.x = 0;
							timelineRect.height = TimelineRectConst.Timeline_Track_Height;
							if (timelineRect.Contains(EditorMouseInput.touchPoint))
								EditorMouseInput.status = EditorMouseInputStatus.Retime;
							else if (IsMouseDownOfSelectedItem())
								;
							else if (TryToSelectUnselectedItem())
								EditorMouseInput.status = EditorMouseInputStatus.Selected;
							else if (Event.current.control)
								;
							else
								EditorMouseInput.status = EditorMouseInputStatus.Normal;
						}
						else if (Event.current.button == 1)
						{
							Rect positionRect = new Rect();
							positionRect.position = Event.current.mousePosition;
							OnMouseRightButtonClick(positionRect);
						}

						break;
					case EventType.MouseDrag:
						if (isMouseLeftButtonPressed)
						{
							EditorMouseInput.touchPoint = Event.current.mousePosition;
							switch (EditorMouseInput.status)
							{
								case EditorMouseInputStatus.Normal:
									EditorMouseInput.status = EditorMouseInputStatus.Selecting;
									UpdateSelectedItems();
									break;
								case EditorMouseInputStatus.Retime:
									EditorMouseInput.status = EditorMouseInputStatus.Retime;
									break;
								case EditorMouseInputStatus.Selected:
									if (!Event.current.control)
									{
										EditorMouseInput.status = EditorMouseInputStatus.DraggingSelected;
										OnDraggingSelected();
									}
									else
									{
										EditorMouseInput.status = EditorMouseInputStatus.Selecting;
										UpdateSelectedItems();
									}

									break;
								case EditorMouseInputStatus.Selecting:
									EditorMouseInput.status = EditorMouseInputStatus.Selecting;
									UpdateSelectedItems();
									break;
								case EditorMouseInputStatus.DraggingSelected:
									EditorMouseInput.status = EditorMouseInputStatus.DraggingSelected;
									OnDraggingSelected();
									break;
							}
						}

						break;
				}
			}
			else if (Event.current.isKey)
			{
				if (Event.current.type == EventType.KeyDown)
				{
					switch (Event.current.keyCode)
					{
						case KeyCode.V:
							if (Event.current.alt)
								DoEditorCommand(EditorCommand.Paste);
							break;
						case KeyCode.D:
							if (Event.current.alt)
								DoEditorCommand(EditorCommand.Delete);
							break;

						default:
							break;
					}
				}
			}


			if (Event.current.rawType == EventType.MouseUp)
			{
				if (isMouseLeftButtonPressed)
				{
					EditorMouseInput.touchPoint = Event.current.mousePosition;
					isMouseLeftButtonPressed = false;
					switch (EditorMouseInput.status)
					{
						case EditorMouseInputStatus.Retime:
						case EditorMouseInputStatus.Selecting:
							EditorMouseInput.status =
								IsHasSelectedItem() ? EditorMouseInputStatus.Selected : EditorMouseInputStatus.Normal;
							break;
						case EditorMouseInputStatus.DraggingSelected:
							EditorMouseInput.status = EditorMouseInputStatus.Selected;
							break;
					}
				}
			}
		}

		void OnEditorMouseInputStatusChanged(EditorMouseInputStatus editorMouseInputStatus)
		{
			switch (editorMouseInputStatus)
			{
				case EditorMouseInputStatus.Retime:
					var time = EditorMouseInput.touchPoint.x / widthPerSecond;
					this.playTime = time < 0 ? 0 : time;
					AutoScrollPosition();
					break;
				case EditorMouseInputStatus.Selecting:
				case EditorMouseInputStatus.DraggingSelected:
					AutoScrollPosition();
					break;
			}

			onEditorMouseInputStatusChangeCallback?.Invoke(editorMouseInputStatus);
		}

		void AutoScrollPosition()
		{
			const float sampleRatio = 1 / 30f;
			if (EditorMouseInput.touchPoint.x - _scrollPosition.x > scrollRect.width * 0.85f)
				_scrollPosition.x += widthPerSecond * sampleRatio;
			else if (EditorMouseInput.touchPoint.x - _scrollPosition.x < scrollRect.width * 0.15f)
				_scrollPosition.x -= widthPerSecond * sampleRatio;
		}


		bool TryToSelectUnselectedItem()
		{
			if (tryToSelectUnselectedItemCallback != null)
				return tryToSelectUnselectedItemCallback(Event.current.mousePosition);
			return false;
		}

		bool IsMouseDownOfSelectedItem()
		{
			if (isMouseDownOfSelectedItem != null)
				return isMouseDownOfSelectedItem(Event.current.mousePosition);
			return false;
		}

		bool IsHasSelectedItem()
		{
			return isHasSelectedItem != null && isHasSelectedItem();
		}

		void UpdateSelectedItems()
		{
			updateSelectedItemsCallback?.Invoke(EditorMouseInput.selectedRect);
		}

		void OnDraggingSelected()
		{
			onDraggingSelectedCallback?.Invoke(Event.current.delta.x / widthPerSecond);
		}

		void DoEditorCommand(EditorCommand editorCommand)
		{
			onDoEditorCommandCallback?.Invoke(editorCommand);
		}

		void OnMouseRightButtonClick(Rect positionRect)
		{
			this.onMouseRightButtonClickCallback?.Invoke(positionRect);
		}
	}
}