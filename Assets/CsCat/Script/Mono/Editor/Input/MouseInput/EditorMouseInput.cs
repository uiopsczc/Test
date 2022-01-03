using System;
using UnityEngine;
using UnityEditor;

namespace CsCat
{
	public static class EditorMouseInput
	{
		public static Action<EditorMouseInputStatus> onStatusChanged;
		static EditorMouseInputStatus _status = EditorMouseInputStatus.Normal;

		public static EditorMouseInputStatus status
		{
			get => _status;
			set
			{
				_status = value;
				onStatusChanged?.Invoke(_status);
			}
		}

		public static Vector2 touchPoint;
		public static Vector2 lastTouchPoint;

		public static Rect selectedRect
		{
			get
			{
				Vector2 min = Vector2.Min(touchPoint, lastTouchPoint);
				Vector2 max = Vector2.Max(touchPoint, lastTouchPoint);
				Rect selectedRect = Rect.MinMaxRect(min.x, min.y, max.x, max.y);
				return selectedRect;
			}
		}

		public static bool IsSelectedOf(Rect rect)
		{
			return selectedRect.Overlaps(rect);
		}

		public static void DrawSelectBox()
		{
			if (_status == EditorMouseInputStatus.Selecting)
			{
				Vector2 min = Vector2.Min(lastTouchPoint, touchPoint);
				Vector2 max = Vector2.Max(lastTouchPoint, touchPoint);
				if (!min.Equals(max))
				{
					float w = max.x - min.x;
					float h = max.y - min.y;
					Rect[] selectBoxSides = new Rect[4];
					selectBoxSides[0].Set(min.x, min.y, w, 1); // top
					selectBoxSides[1].Set(min.x, max.y, w, 1); // bottom
					selectBoxSides[2].Set(min.x, min.y, 1, h); // left
					selectBoxSides[3].Set(max.x, min.y, 1, h);
					Rect background = new Rect(min, new Vector2(w, h));
					Color color = Color.gray;
					color.a = 0.5f;
					EditorGUI.DrawRect(background, color);
					for (int i = 0; i < selectBoxSides.Length; i++)
					{
						EditorGUI.DrawRect(selectBoxSides[i], Color.white);
					}
				}
			}
		}
	}
}