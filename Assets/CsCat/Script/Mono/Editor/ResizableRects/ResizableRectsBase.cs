#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class ResizableRectsBase
  {
    protected Func<Rect> total_rect_func;
    protected Rect[] split_line_rects;
    protected Rect[] resize_split_rects;
    protected int resizing_split_line_rect_index = ResizableRectsConst.Not_Resizing_Split_Line_Rect_Index;


    public Rect[] rects;
    public Rect[] padding_rects;
    public Rect total_rect { get { return total_rect_func(); } }

    public bool is_resizing { get { return resizing_split_line_rect_index != ResizableRectsConst.Not_Resizing_Split_Line_Rect_Index; } }
    private bool can_resizable = true;

    protected bool is_using_split_pixels = false;
    protected float[] split_pixels;
    protected float[] split_pcts;

    public string name;

    public ResizableRectsBase(Func<Rect> total_rect_func, float[] split_pixels, float[] split_pcts = null)
    {
      this.total_rect_func = total_rect_func;
      this.split_pixels = split_pixels;
      this.split_pcts = split_pcts;
      this.is_using_split_pixels = !split_pixels.IsNullOrEmpty();
      if (!is_using_split_pixels && split_pcts.IsNullOrEmpty())
        this.split_pcts = new float[] { 0.3f };

      int split_count = is_using_split_pixels ? this.split_pixels.Length : this.split_pcts.Length;
      this.resize_split_rects = new Rect[split_count];


      this.split_line_rects = new Rect[split_count];
      UpdateSplitLineRects();


      this.rects = new Rect[split_count + 1];
      this.padding_rects = new Rect[split_count + 1];
      SetRectListSize();
    }

    public virtual void UpdateSplitLineSetting(int split_line_index, float delta)
    {

    }

    public virtual void SetSplitLinePosition(int split_line_index, float position)
    {
    }

    protected virtual void UpdateSplitLineRects()
    {
    }

    protected virtual void SetRectListSize()
    {
    }

    public void SetCanResizable(bool can_resizable)
    {
      this.can_resizable = can_resizable;
    }

    public void OnGUI()
    {
      UpdateSplitLineRects();
      foreach (var split_line_rect in split_line_rects)
        EditorGUI.DrawRect(split_line_rect, Color.grey);
      SetRectListSize();
      Resizing();
    }

    void Resizing()
    {
      ResizingSplitLineRects();
      if (can_resizable)
      {
        ResizingResizeSplitRects();
        HandleMouseResizingEvent();
      }
    }

    protected virtual void ResizingSplitLineRects()
    {
    }

    protected virtual void ResizingResizeSplitRects()
    {
    }

    protected virtual void HandleMouseDragEvent()
    {
    }

    protected void HandleMouseResizingEvent()
    {
      if (Event.current.isMouse)
      {
        if (Event.current.type == EventType.MouseDown)
        {
          for (int i = 0; i < resize_split_rects.Length; i++)
          {
            if (resize_split_rects[i].Contains(Event.current.mousePosition))
            {
              resizing_split_line_rect_index = i;
              Event.current.Use();
              break;
            }
          }
        }
        else if (Event.current.type == EventType.MouseDrag)
        {
          if (is_resizing)
          {
            HandleMouseDragEvent();
            Event.current.Use();
          }
        }
        else if (Event.current.type == EventType.MouseUp)
        {
          if (is_resizing)
          {
            resizing_split_line_rect_index = ResizableRectsConst.Not_Resizing_Split_Line_Rect_Index;
            Event.current.Use();
          }
        }
      }
    }
  }
}
#endif