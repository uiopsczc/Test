#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class HorizontalResizableRects : ResizableRectsBase
  {
    public HorizontalResizableRects(Func<Rect> total_rect_func, float[] split_pixels, float[] split_pcts = null) : base(
      total_rect_func, split_pixels,
      split_pcts)
    {
    }

    public override void SetSplitLinePosition(int split_line_index, float position)
    {
      this.split_line_rects[split_line_index].position = total_rect.position + new Vector2(position, 0);
    }

    protected override void UpdateSplitLineRects()
    {
      for (int i = 0; i < this.split_line_rects.Length; i++)
      {
        if (is_using_split_pixels)
        {
          float split_pixel = split_pixels[i];
          split_line_rects[i].Set(total_rect.x + split_pixel, total_rect.y, 1, total_rect.height);
        }
        else
        {
          float split_pct = split_pcts[i];
          split_line_rects[i].Set(total_rect.x + split_pct * total_rect.width, total_rect.y, 1, total_rect.height);
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
            split_line_rects[0].x - total_rect.x,
            total_rect.height);
        }
        else if (i == padding_rects.Length - 1)
        {
          rects[i].Set(split_line_rects[i - 1].x, total_rect.y,
            total_rect.x + total_rect.width - split_line_rects[i - 1].x, total_rect.height);
        }
        else
          rects[i].Set(split_line_rects[i - 1].x, total_rect.y,
            split_line_rects[i].x - split_line_rects[i - 1].x,
            total_rect.height);

        padding_rects[i].Set(rects[i].x + ResizableRectsConst.Padding, rects[i].y + ResizableRectsConst.Padding,
          rects[i].width - 2 * ResizableRectsConst.Padding, rects[i].height - 2 * ResizableRectsConst.Padding);
      }
    }

    protected override void ResizingSplitLineRects()
    {
      for (int i = 0; i < split_line_rects.Length; i++)
        split_line_rects[i].height = total_rect.height;
    }

    protected override void ResizingResizeSplitRects()
    {
      for (int i = 0; i < split_line_rects.Length; i++)
      {
        resize_split_rects[i].Set(split_line_rects[i].x, split_line_rects[i].y, split_line_rects[i].width,
          split_line_rects[i].height);
        resize_split_rects[i].x -= ResizableRectsConst.Resize_Split_Rect_Width / 2;
        resize_split_rects[i].width = ResizableRectsConst.Resize_Split_Rect_Width;

        EditorGUIUtility.AddCursorRect(resize_split_rects[i], MouseCursor.SplitResizeLeftRight);
      }
    }

    protected override void HandleMouseDragEvent()
    {
      if (Event.current.delta.x > 0)
      {
        if (resizing_split_line_rect_index + 1 == resize_split_rects.Length)
        {
          if (split_line_rects[resizing_split_line_rect_index].x + Event.current.delta.x <
              total_rect.x + total_rect.width - ResizableRectsConst.Resize_Split_Rect_Width)
            UpdateSplitLineSetting(resizing_split_line_rect_index, Event.current.delta.x);
        }
        else
        {
          if (split_line_rects[resizing_split_line_rect_index].x + Event.current.delta.x <
              split_line_rects[resizing_split_line_rect_index + 1].x -
              ResizableRectsConst.Resize_Split_Rect_Width)
            UpdateSplitLineSetting(resizing_split_line_rect_index, Event.current.delta.x);
        }
      }
      else
      {
        if (resizing_split_line_rect_index - 1 < 0)
        {
          if (split_line_rects[resizing_split_line_rect_index].x + Event.current.delta.x >
              total_rect.x + ResizableRectsConst.Resize_Split_Rect_Width)
            UpdateSplitLineSetting(resizing_split_line_rect_index, Event.current.delta.x);
        }
        else
        {
          if (split_line_rects[resizing_split_line_rect_index].x + Event.current.delta.x >
              split_line_rects[resizing_split_line_rect_index - 1].x +
              ResizableRectsConst.Resize_Split_Rect_Width)
            UpdateSplitLineSetting(resizing_split_line_rect_index, Event.current.delta.x);
        }
      }
    }

    public override void UpdateSplitLineSetting(int split_line_index, float delta)
    {
      if (is_using_split_pixels)
        split_pixels[split_line_index] += delta;
      else
        split_pcts[split_line_index] += delta / total_rect.width;
    }
  }
}
#endif