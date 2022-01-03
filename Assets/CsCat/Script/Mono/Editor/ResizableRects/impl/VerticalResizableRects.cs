#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class VerticalResizableRects : ResizableRectsBase
  {
    public VerticalResizableRects(Func<Rect> total_rect_func, float[] split_pixels, float[] splitPcTs = null) : base(
      total_rect_func, split_pixels,
      splitPcTs)
    {
    }

    public override void SetSplitLinePosition(int splitLineIndex, float position)
    {
      this.splitLineRects[splitLineIndex].position = total_rect.position + new Vector2(0, position);
    }

    protected override void UpdateSplitLineRects()
    {
      for (int i = 0; i < this.splitLineRects.Length; i++)
      {
        if (isUsingSplitPixels)
        {
          float split_pixel = splitPixels[i];
          splitLineRects[i].Set(total_rect.x, total_rect.y + split_pixel, total_rect.width, 1);
        }
        else
        {
          float split_pct = splitPCTs[i];
          splitLineRects[i].Set(total_rect.x, total_rect.y + split_pct * total_rect.height, total_rect.width, 1);
        }
      }
    }

    protected override void SetRectListSize()
    {
      for (int i = 0; i < rects.Length; i++)
      {
        if (i == 0)
          rects[i].Set(total_rect.x, total_rect.y, total_rect.width, splitLineRects[0].y - total_rect.y);
        else if (i == rects.Length - 1)
          rects[i].Set(total_rect.x, splitLineRects[i - 1].y,
            total_rect.width, total_rect.y + total_rect.height - splitLineRects[i - 1].y);
        else
          rects[i].Set(total_rect.x, splitLineRects[i - 1].y, total_rect.width,
            splitLineRects[i].y - splitLineRects[i - 1].y
          );
        paddingRects[i].Set(rects[i].x + ResizableRectsConst.Padding, rects[i].y + ResizableRectsConst.Padding,
          rects[i].width - 2 * ResizableRectsConst.Padding, rects[i].height - 2 * ResizableRectsConst.Padding);
      }
    }

    protected override void ResizingSplitLineRects()
    {
      for (int i = 0; i < splitLineRects.Length; i++)
        splitLineRects[i].width = total_rect.width;
    }

    protected override void ResizingResizeSplitRects()
    {
      for (int i = 0; i < splitLineRects.Length; i++)
      {
        resizeSplitRects[i].Set(splitLineRects[i].x, splitLineRects[i].y, splitLineRects[i].width,
          splitLineRects[i].height);
        resizeSplitRects[i].y -= ResizableRectsConst.Resize_Split_Rect_Width / 2;
        resizeSplitRects[i].height = ResizableRectsConst.Resize_Split_Rect_Width;

        EditorGUIUtility.AddCursorRect(resizeSplitRects[i], MouseCursor.SplitResizeUpDown);
      }
    }

    protected override void HandleMouseDragEvent()
    {
      if (Event.current.delta.y > 0)
      {
        if (resizingSplitLineRectIndex + 1 == resizeSplitRects.Length)
        {
          if (splitLineRects[resizingSplitLineRectIndex].y + Event.current.delta.y <
              total_rect.y + total_rect.height - ResizableRectsConst.Resize_Split_Rect_Width)
            UpdateSplitLineSetting(resizingSplitLineRectIndex, Event.current.delta.y);

        }
        else
        {
          if (splitLineRects[resizingSplitLineRectIndex].y + Event.current.delta.y <
              splitLineRects[resizingSplitLineRectIndex + 1].y -
              ResizableRectsConst.Resize_Split_Rect_Width)
            UpdateSplitLineSetting(resizingSplitLineRectIndex, Event.current.delta.y);
        }
      }
      else
      {
        if (resizingSplitLineRectIndex - 1 < 0)
        {
          if (splitLineRects[resizingSplitLineRectIndex].y + Event.current.delta.y >
              total_rect.y + ResizableRectsConst.Resize_Split_Rect_Width)
            UpdateSplitLineSetting(resizingSplitLineRectIndex, Event.current.delta.y);
        }
        else
        {
          if (splitLineRects[resizingSplitLineRectIndex].y + Event.current.delta.y >
              splitLineRects[resizingSplitLineRectIndex - 1].y +
              ResizableRectsConst.Resize_Split_Rect_Width)
            UpdateSplitLineSetting(resizingSplitLineRectIndex, Event.current.delta.y);
        }
      }
    }

    public override void UpdateSplitLineSetting(int splitLineIndex, float delta)
    {
      if (isUsingSplitPixels)
        splitPixels[splitLineIndex] += delta;
      else
        splitPCTs[splitLineIndex] += delta / total_rect.height;
    }

  }
}
#endif