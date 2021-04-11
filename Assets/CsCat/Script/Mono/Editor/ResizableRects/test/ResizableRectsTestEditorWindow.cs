using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class ResizableRectsTestEditorWindow : EditorWindow
  {
    private HorizontalResizableRects resizableRects;


    void Awake()
    {
      this.resizableRects =
        new HorizontalResizableRects(() => new Rect(0, 0, this.position.width, this.position.height),null,new float[]{ 0.3f,0.6f});
    }


    void OnGUI()
    {
      this.resizableRects.OnGUI();
      for (int i = 0; i < this.resizableRects.rects.Length; i++)
      {
        if (i == 0)
          EditorGUI.DrawRect(this.resizableRects.rects[i], Color.red);
        if (i == 1)
          EditorGUI.DrawRect(this.resizableRects.rects[i], Color.green);
        if (i == 2)
          EditorGUI.DrawRect(this.resizableRects.rects[i], Color.blue);
      }
      Repaint();
    }
  }
}