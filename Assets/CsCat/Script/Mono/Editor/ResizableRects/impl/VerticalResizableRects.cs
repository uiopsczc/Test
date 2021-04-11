#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class VerticalResizableRects : ResizableRectsBase
  {
    public VerticalResizableRects(Func<Rect> total_rect_func, float[] split_pixels, float[] split_pcts = null) : base(
      total_rect_func, split_pixels,
      split_pcts)
    {
    }

    public override void SetSplitLinePosition(int split_line_index, float position)
    {
      this.split_line_rects[split_line_index].position = total_rect.position + new Vector2(0, position);
    }

    protected override void UpdateSplitLineRects()
    {
      for (int i = 0; i < this.split_line_rects.Length; i++)
      {
        if (is_using_split_pixels)
        {
          float split_pixel = split_pixels[i];
          split_line_rects[i].Set(total_rect.x, total_rect.y + split_pixel, total_rect.width, 1);
        }
        else
        {
          float split_pct = split_pcts[i];
          split_line_rects[i].Set(total_rect.x, total_rect.y + split_pct * total_rect.height, total_rect.width, 1);
        }
      }
    }

    protected override void SetRectListSize()
    {
      for (int i = 0; i < rects.Length; i++)
      {
        if (i == 0)
          rects[i].Set(total_rect.x, total_rect.y, total_rect.width, split_line_rects[0].y - total_rect.y);
        else if (i == rects.Length - 1)
          rects[i].Set(total_rect.x, split_line_rects[i - 1].y,
            total_rect.width, total_rect.y + total_rect.height - split_line_rects[i - 1].y);
        else
          rects[i].Set(total_rect.x, split_line_rects[i - 1].y, total_rect.width,
            split_line_rects[i].y - split_line_rects[i - 1].y
          );
        padding_rects[i].Set(rects[i].x + ResizableRectsConst.Padding, rects[i].y + ResizableRectsConst.Padding,
          rects[i].width - 2 * ResizableRectsConst.Padding, rects[i].height - 2 * ResizableRectsConst.Padding);
      }
    }

    protected override void ResizingSplitLineRects()
    {
      for (int i = 0; i < split_line_rects.Length; i++)
        split_line_rects[i].width = total_rect.width;
    }

    protected override void ResizingResizeSplitRects()
    {
      for (int i = 0; i < split_line_rects.Length; i++)
      {
        resize_split_rects[i].Set(split_line_rects[i].x, split_line_rects[i].y, split_line_rects[i].width,
          split_line_rects[i].height);
        resize_split_rects[i].y -= ResizableRectsConst.Resize_Split_Rect_Width / 2;
        resize_split_rects[i].height = ResizableRectsConst.Resize_Split_Rect_Width;

        EditorGUIUtility.AddCursorRect(resize_split_rects[i], MouseCursor.SplitResizeUpDown);
      }
    }

    protected override void HandleMouseDragEvent()
    {
      if (Event.current.delta.y > 0)
      {
        if (resizing_split_line_rect_index + 1 == resize_split_rects.Length)
        {
          if (split_line_rects[resizing_split_line_rect_index].y + Event.current.delta.y <
              total_rect.y + total_rect.height - ResizableRectsConst.Resize_Split_Rect_Width)
            UpdateSplitLineSetting(resizing_split_line_rect_index, Event.current.delta.y);

        }
        else
        {
          if (split_line_rects[resizing_split_line_rect_index].y + Event.current.delta.y <
              split_line_rects[resizing_split_line_rect_index + 1].y -
              ResizableRectsConst.Resize_Split_Rect_Width)
            UpdateSplitLineSetting(resizing_split_line_rect_index, Event.current.delta.y);
        }
      }
      else
      {
        if (resizing_split_line_rect_index - 1 < 0)
        {
          if (split_line_rects[resizing_split_line_rect_index].y + Event.current.delta.y >
              total_rect.y + ResizableRectsConst.Resize_Split_Rect_Width)
            UpdateSplitLineSetting(resizing_split_line_rect_index, Event.current.delta.y);
        }
        else
        {
          if (split_line_rects[resizing_split_line_rect_index].y + Event.current.delta.y >
              split_line_rects[resizing_split_line_rect_index - 1].y +
              ResizableRectsConst.Resize_Split_Rect_Width)
            UpdateSplitLineSetting(resizing_split_line_rect_index, Event.current.delta.y);
        }
      }
    }

    public override void UpdateSplitLineSetting(int split_line_index, float delta)
    {
      if (is_using_split_pixels)
        split_pixels[split_line_index] += delta;
      else
        split_pcts[split_line_index] += delta / total_rect.height;
    }

  }
}
#endif