#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class HorizontalResizableRects : ResizableRectsBase
	{
		public HorizontalResizableRects(Func<Rect> totalRectFunc, float[] splitPixels, float[] splitPCTs = null) :
			base(
				totalRectFunc, splitPixels,
				splitPCTs)
		{
		}

		public override void SetSplitLinePosition(int splitLineIndex, float position)
		{
			this.splitLineRects[splitLineIndex].position = total_rect.position + new Vector2(position, 0);
		}

		protected override void UpdateSplitLineRects()
		{
			for (int i = 0; i < this.splitLineRects.Length; i++)
			{
				if (isUsingSplitPixels)
				{
					float splitPixel = splitPixels[i];
					splitLineRects[i].Set(total_rect.x + splitPixel, total_rect.y, 1, total_rect.height);
				}
				else
				{
					float splitPCT = splitPCTs[i];
					splitLineRects[i].Set(total_rect.x + splitPCT * total_rect.width, total_rect.y, 1,
						total_rect.height);
				}
			}
		}

		protected override void SetRectListSize()
		{
			for (int i = 0; i < rects.Length; i++)
			{
				if (i == 0)
				{
					rects[i].Set(total_rect.x, total_rect.y,
						splitLineRects[0].x - total_rect.x,
						total_rect.height);
				}
				else if (i == paddingRects.Length - 1)
				{
					rects[i].Set(splitLineRects[i - 1].x, total_rect.y,
						total_rect.x + total_rect.width - splitLineRects[i - 1].x, total_rect.height);
				}
				else
					rects[i].Set(splitLineRects[i - 1].x, total_rect.y,
						splitLineRects[i].x - splitLineRects[i - 1].x,
						total_rect.height);

				paddingRects[i].Set(rects[i].x + ResizableRectsConst.Padding, rects[i].y + ResizableRectsConst.Padding,
					rects[i].width - 2 * ResizableRectsConst.Padding,
					rects[i].height - 2 * ResizableRectsConst.Padding);
			}
		}

		protected override void ResizingSplitLineRects()
		{
			for (int i = 0; i < splitLineRects.Length; i++)
				splitLineRects[i].height = total_rect.height;
		}

		protected override void ResizingResizeSplitRects()
		{
			for (int i = 0; i < splitLineRects.Length; i++)
			{
				resizeSplitRects[i].Set(splitLineRects[i].x, splitLineRects[i].y, splitLineRects[i].width,
					splitLineRects[i].height);
				resizeSplitRects[i].x -= ResizableRectsConst.Resize_Split_Rect_Width / 2;
				resizeSplitRects[i].width = ResizableRectsConst.Resize_Split_Rect_Width;

				EditorGUIUtility.AddCursorRect(resizeSplitRects[i], MouseCursor.SplitResizeLeftRight);
			}
		}

		protected override void HandleMouseDragEvent()
		{
			if (Event.current.delta.x > 0)
			{
				if (resizingSplitLineRectIndex + 1 == resizeSplitRects.Length)
				{
					if (splitLineRects[resizingSplitLineRectIndex].x + Event.current.delta.x <
					    total_rect.x + total_rect.width - ResizableRectsConst.Resize_Split_Rect_Width)
						UpdateSplitLineSetting(resizingSplitLineRectIndex, Event.current.delta.x);
				}
				else
				{
					if (splitLineRects[resizingSplitLineRectIndex].x + Event.current.delta.x <
					    splitLineRects[resizingSplitLineRectIndex + 1].x -
					    ResizableRectsConst.Resize_Split_Rect_Width)
						UpdateSplitLineSetting(resizingSplitLineRectIndex, Event.current.delta.x);
				}
			}
			else
			{
				if (resizingSplitLineRectIndex - 1 < 0)
				{
					if (splitLineRects[resizingSplitLineRectIndex].x + Event.current.delta.x >
					    total_rect.x + ResizableRectsConst.Resize_Split_Rect_Width)
						UpdateSplitLineSetting(resizingSplitLineRectIndex, Event.current.delta.x);
				}
				else
				{
					if (splitLineRects[resizingSplitLineRectIndex].x + Event.current.delta.x >
					    splitLineRects[resizingSplitLineRectIndex - 1].x +
					    ResizableRectsConst.Resize_Split_Rect_Width)
						UpdateSplitLineSetting(resizingSplitLineRectIndex, Event.current.delta.x);
				}
			}
		}

		public override void UpdateSplitLineSetting(int splitLineIndex, float delta)
		{
			if (isUsingSplitPixels)
				splitPixels[splitLineIndex] += delta;
			else
				splitPCTs[splitLineIndex] += delta / total_rect.width;
		}
	}
}
#endif