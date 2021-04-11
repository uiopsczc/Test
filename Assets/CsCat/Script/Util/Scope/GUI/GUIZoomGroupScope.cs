using System;
using UnityEngine;

namespace CsCat
{
  public class GUIZoomGroupScope : IDisposable
  {
    private static Matrix4x4 gui_matrix_pre;


    public GUIZoomGroupScope(Rect gui_rect, float zoom_scale)
    {
      Rect rect = gui_rect.ScaleBy(1f / zoom_scale, gui_rect.min);

      GUI.BeginGroup(rect);
      gui_matrix_pre = GUI.matrix;
      var lhs = Matrix4x4.TRS(rect.min, Quaternion.identity, Vector3.one);
      var one = Vector3.one;
      one.y = zoom_scale;
      one.x = zoom_scale;
      var rhs = Matrix4x4.Scale(one);
      GUI.matrix = lhs * rhs * lhs.inverse * GUI.matrix;
    }


    public void Dispose()
    {
      GUI.matrix = gui_matrix_pre;
      GUI.EndGroup();
    }
  }
}