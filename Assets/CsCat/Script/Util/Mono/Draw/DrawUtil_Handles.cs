#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
namespace CsCat
{
  public partial class DrawUtil
  {
    public static bool Is_Handles_Enable = true;

    #region 画射线

    public static void DrawRay(Vector3 position, Vector3 direction, Color color)
    {
      using (new HandlesColorScope(color))
      {
        Handles.DrawLine(position, position + direction);
      }
    }

    public static void DrawRay(Vector3 position, Vector3 direction)
    {
      DrawRay(position, direction, DrawUtilConst.HandlesDraw_DefaultColor);
    }

    #endregion

    #region 画箭头 

    /// <summary>
    ///   画箭头
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    public static void HandlesArrow(Vector3 position, Vector3 direction)
    {
      HandlesArrow(position, direction, DrawUtilConst.HandlesDraw_DefaultColor);
    }

    /// <summary>
    ///   画箭头
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    /// <param name="color"></param>
    public static void HandlesArrow(Vector3 position, Vector3 direction, Color color)
    {
      if (!Is_Handles_Enable)
        return;
      using (new HandlesColorScope(color))
      {
        DrawRay(position, direction, color);
        HandlesCone(position + direction, direction.normalized, 15, color);
      }
    }

    #endregion

    #region 画bounds 

    /// <summary>
    ///   画bounds
    /// </summary>
    /// <param name="bounds"></param>
    public static void HandlesBounds(Bounds bounds)
    {
      HandlesBounds(bounds, DrawUtilConst.HandlesDraw_DefaultColor);
    }

    /// <summary>
    ///   画bounds
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="color"></param>
    public static void HandlesBounds(Bounds bounds, Color color)
    {
      if (!Is_Handles_Enable) return;

      using (new HandlesColorScope(color))
      {
        var cube = new Cube3d(bounds.center, bounds.size);
        cube.GetDrawLineList().ForEach(kv => { HandlesLine(kv.Key, kv.Value, color); });
      }
    }

    #endregion

    #region 画Capsule 

    public static void HandlesCapsule(Vector3 start, Vector3 end, float radius)
    {
      HandlesCapsule(start, end, radius, DrawUtilConst.HandlesDraw_DefaultColor);
    }

    public static void HandlesCapsule(Vector3 start, Vector3 end, float radius, Color color)
    {
      if (!Is_Handles_Enable) return;

      using (new HandlesColorScope(color))
      {
        HandlesCylinder(start, end, radius, color);
        HandlesSphere(start, radius, color);
        HandlesSphere(end, radius, color);
      }
    }

    #endregion

    #region 画圆 

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    public static void HandlesCircle(Vector3 position, float radius)
    {
      HandlesCircle(position, radius, Matrix4x4.identity, DrawUtilConst.HandlesDraw_DefaultColor);
    }

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    /// <param name="mutiply_matrix"></param>
    public static void HandlesCircle(Vector3 position, float radius, Matrix4x4 mutiply_matrix)
    {
      HandlesCircle(position, radius, mutiply_matrix, DrawUtilConst.HandlesDraw_DefaultColor);
    }

    /// <summary>
    ///   画圆
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    /// <param name="mutiply_matrix"></param>
    /// <param name="color"></param>
    public static void HandlesCircle(Vector3 position, float radius, Matrix4x4 mutiply_matrix, Color color)
    {
      if (!Is_Handles_Enable) return;

      using (new HandlesColorScope(color))
      {
        var circle = new Circle3d(position, radius, mutiply_matrix);

        circle.GetDrawLineList().ForEach(kv => { HandlesLine(kv.Key, kv.Value, color); });
      }
    }

    #endregion


    #region 画圆锥 

    public static void HandlesCone(Vector3 end_point, Vector3 forward)
    {
      HandlesCone(end_point, forward, 45, DrawUtilConst.HandlesDraw_DefaultColor);
    }

    public static void HandlesCone(Vector3 end_point, Vector3 forward, float angle)
    {
      HandlesCone(end_point, forward, angle, DrawUtilConst.HandlesDraw_DefaultColor);
    }

    /// <summary>
    /// </summary>
    /// <param name="end_point">箭头的终点</param>
    /// <param name="forward">箭头的方向</param>
    /// <param name="angle">角度</param>
    /// <param name="color"></param>
    public static void HandlesCone(Vector3 end_point, Vector3 forward, float angle, Color color)
    {
      if (!Is_Handles_Enable) return;

      using (new HandlesColorScope(color))
      {
        var slerp_right = Vector3.Slerp(-forward, forward, 0.5f);
        var slerp_up = Vector3.Cross(forward, slerp_right).normalized * forward.magnitude;

        var angle_slerp_right = Vector3.Slerp(forward, slerp_right, angle / 90);
        var angle_slerp_left = Vector3.Slerp(forward, -slerp_right, angle / 90);

        var angle_slerp_up = Vector3.Slerp(forward, slerp_up, angle / 90);
        var angle_slerp_down = Vector3.Slerp(forward, -slerp_up, angle / 90);

        var plane = new Plane(forward, end_point - forward);
        var ray = new Ray(end_point, -angle_slerp_right);
        float distance;
        plane.Raycast(ray, out distance);

        DrawRay(end_point, (-angle_slerp_right).normalized * distance, color);
        DrawRay(end_point, (-angle_slerp_left).normalized * distance, color);
        DrawRay(end_point, (-angle_slerp_up).normalized * distance, color);
        DrawRay(end_point, (-angle_slerp_down).normalized * distance, color);

        HandlesCircle(end_point - forward, (forward + -angle_slerp_right.normalized * distance).magnitude,
          Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, forward)), color);
        HandlesCircle(end_point - forward / 2, (forward + -angle_slerp_right.normalized * distance).magnitude / 2,
          Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, forward)), color);
      }
    }

    #endregion


    #region 画圆柱体 

    /// <summary>
    ///   画圆柱体
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="radius"></param>
    public static void HandlesCylinder(Vector3 start, Vector3 end, float radius)
    {
      HandlesCylinder(start, end, radius, DrawUtilConst.HandlesDraw_DefaultColor);
    }

    /// <summary>
    ///   画圆柱体
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="color"></param>
    /// <param name="radius"></param>
    public static void HandlesCylinder(Vector3 start, Vector3 end, float radius, Color color)
    {
      if (!Is_Handles_Enable) return;

      using (new HandlesColorScope(color))
      {
        var up_dir = (end - start).normalized;

        var to_up_matrix = Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, up_dir));
        //顶部的圆形
        HandlesCircle(start, radius, to_up_matrix, color);
        ////底部的圆形
        HandlesCircle(end, radius, to_up_matrix, color);

        HandlesLine(start, end, Color.yellow);

        //四个矩形
        var rect_center = (start + end) / 2;
        var size = new Vector2(radius * 2, (start - end).magnitude);

        var to_right_matrix = Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0));

        HandlesCenterRect(rect_center, size,
          to_up_matrix * to_right_matrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 0)), color);
        HandlesCenterRect(rect_center, size,
          to_up_matrix * to_right_matrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 45)), color);
        HandlesCenterRect(rect_center, size,
          to_up_matrix * to_right_matrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), color);
        HandlesCenterRect(rect_center, size,
          to_up_matrix * to_right_matrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 135)), color);
      }
    }

    #endregion


    #region 画LocalCube 

    /// <summary>
    ///   画LocalCube
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    public static void HandlesLocalCube(Transform transform, Vector3 offset, Vector3 size)
    {
      HandlesLocalCube(transform, offset, size, DrawUtilConst.HandlesDraw_DefaultColor);
    }


    /// <summary>
    ///   画LocalCube
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    /// <param name="color"></param>
    public static void HandlesLocalCube(Transform transform, Vector3 offset, Vector3 size, Color color)
    {
      if (!Is_Handles_Enable) return;

      using (new HandlesColorScope(color))
      {
        var cube = new Cube3d(Vector3.zero, Vector3.one);
        cube.MultiplyMatrix(Matrix4x4.TRS(offset, Quaternion.identity, size));
        cube.PreMultiplyMatrix(transform.localToWorldMatrix);
        cube.GetDrawLineList().ForEach(kv => { HandlesLine(kv.Key, kv.Value, color); });
      }
    }

    /// <summary>
    ///   画LocalCube
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    public static void HandlesLocalCube(Matrix4x4 matrix, Vector3 offset, Vector3 size)
    {
      HandlesLocalCube(matrix, offset, size, DrawUtilConst.HandlesDraw_DefaultColor);
    }

    /// <summary>
    ///   画LocalCube
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    /// <param name="color"></param>
    public static void HandlesLocalCube(Matrix4x4 matrix, Vector3 offset, Vector3 size, Color color)
    {
      if (!Is_Handles_Enable) return;

      using (new HandlesColorScope(color))
      {
        var cube = new Cube3d(Vector3.zero, Vector3.one);
        cube.MultiplyMatrix(Matrix4x4.TRS(offset, Quaternion.identity, size));
        cube.PreMultiplyMatrix(matrix);
        cube.GetDrawLineList().ForEach(kv => { HandlesLine(kv.Key, kv.Value, color); });
      }
    }

    #endregion

    #region 画Cube 
    public static void HandlesCube(Vector3 min, Vector3 max, Color color,
      float duration = 0,
      bool is_depth_test = true)
    {

      if (!Is_Handles_Enable) return;

      using (new HandlesColorScope(color))
      {
        var cube = new Cube3d((min + max) / 2, (max - min).Abs());
        cube.GetDrawLineList().ForEach(kv => { HandlesLine(kv.Key, kv.Value, color); });
      }
    }
    #endregion

    #region 画点 

    /// <summary>
    ///   画点
    /// </summary>
    /// <param name="position"></param>
    /// <param name="scale"></param>
    public static void HandlesPoint(Vector3 position, float scale = 1)
    {
      HandlesPoint(position, DrawUtilConst.HandlesDraw_DefaultColor, scale);
    }

    /// <summary>
    ///   画点
    /// </summary>
    /// <param name="position"></param>
    /// <param name="color"></param>
    /// <param name="scale"></param>
    public static void HandlesPoint(Vector3 position, Color color, float scale = 1)
    {
      if (!Is_Handles_Enable) return;

      using (new HandlesColorScope(color))
      {
        DrawRay(position + Vector3.up * (scale * 0.5f), -Vector3.up * scale, color);
        DrawRay(position + Vector3.right * (scale * 0.5f), -Vector3.right * scale, color);
        DrawRay(position + Vector3.forward * (scale * 0.5f), -Vector3.forward * scale, color);
      }
    }

    #endregion

    #region 画球 

    /// <summary>
    ///   画球
    /// </summary>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    public static void HandlesSphere(Vector3 position, float radius)
    {
      HandlesSphere(position, radius, DrawUtilConst.HandlesDraw_DefaultColor);
    }

    /// <summary>
    ///   画球
    /// </summary>
    /// <param name="position"></param>
    /// <param name="color"></param>
    /// <param name="radius"></param>
    public static void HandlesSphere(Vector3 position, float radius, Color color)
    {
      if (!Is_Handles_Enable) return;

      using (new HandlesColorScope(color))
      {
        var sphere = new Sphere3d(position, radius);
        sphere.GetDrawLineList().ForEach(kv => { HandlesLine(kv.Key, kv.Value, color); });
      }
    }

    #endregion

    #region 画线 

    public static void HandlesLine(Vector3 start, Vector3 end, bool is_dotted = false)
    {
      HandlesLine(start, end, DrawUtilConst.HandlesDraw_DefaultColor, is_dotted);
    }

    /// <summary>
    ///   画线
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public static void HandlesLine(Vector3 start, Vector3 end, Color color, bool is_dotted = false)
    {
      if (!Is_Handles_Enable) return;

      using (new HandlesColorScope(color))
      {
        if (!is_dotted)
          Handles.DrawLine(start, end);
        else
          Handles.DrawDottedLine(start, end, 1);
      }

      //LogCat.LogErrorFormat("{0} {1}", start, end);
    }

    #endregion

    #region 画多边形 

    public static void HandlesPolygon(params Vector3[] points)
    {
      HandlesPolygon(Matrix4x4.identity, DrawUtilConst.HandlesDraw_DefaultColor, points);
    }

    public static void HandlesPolygon(Color color, params Vector3[] points)
    {
      HandlesPolygon(Matrix4x4.identity, color, points);
    }

    /// <summary>
    ///   画多边形
    /// </summary>
    /// <param name="mutiply_matrix"></param>
    /// <param name="color"></param>
    /// <param name="points"></param>
    public static void HandlesPolygon(Matrix4x4 mutiply_matrix, Color color, params Vector3[] points)
    {
      if (!Is_Handles_Enable)
        return;

      using (new HandlesColorScope(color))
      {
        var polygon = new Polygon3d(points);
        polygon.MultiplyMatrix(mutiply_matrix);

        polygon.GetDrawLineList().ForEach(kv => { HandlesLine(kv.Key, kv.Value, color); });
      }
    }

    #endregion

    #region 画矩形 

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="center"></param>
    /// <param name="size"></param>
    /// <param name="is_dotted"></param>
    public static void HandlesCenterRect(Vector3 center, Vector2 size, bool is_dotted = false)
    {
      HandlesCenterRect(center, size, Matrix4x4.identity, DrawUtilConst.HandlesDraw_DefaultColor, is_dotted);
    }

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="center"></param>
    /// <param name="size"></param>
    /// <param name="mutiply_matrix"></param>
    /// <param name="is_dotted"></param>
    public static void HandlesCenterRect(Vector3 center, Vector2 size, Matrix4x4 mutiply_matrix, bool is_dotted = false)
    {
      HandlesCenterRect(center, size, mutiply_matrix, DrawUtilConst.HandlesDraw_DefaultColor, is_dotted);
    }

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="is_dotted"></param>
    public static void HandlesCenterRect(Rect rect, bool is_dotted = false)
    {
      HandlesCenterRect(rect.center.ToVector3("x,0,y"), rect.center.ToVector3("x,0,y"), Matrix4x4.identity,
        DrawUtilConst.HandlesDraw_DefaultColor, is_dotted);
    }

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="mutiply_matrix"></param>
    /// <param name="is_dotted"></param>
    public static void HandlesCenterRect(Rect rect, Matrix4x4 mutiply_matrix, bool is_dotted = false)
    {
      HandlesCenterRect(rect.center.ToVector3("x,0,y"), rect.center.ToVector3("x,0,y"), mutiply_matrix,
        DrawUtilConst.HandlesDraw_DefaultColor, is_dotted);
    }


    /// <summary>
    ///   画中心点在center的矩形
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="center"></param>
    /// <param name="size"></param>
    /// <param name="mutiply_matrix"></param>
    /// <param name="color"></param>
    public static void HandlesCenterRect(Vector3 center, Vector2 size, Matrix4x4 mutiply_matrix, Color color,
      bool is_dotted = false)
    {
      using (new HandlesColorScope(color))
      {
        var rectangle = new Rectangle3d(center, size, mutiply_matrix);
        rectangle.GetDrawLineList().ForEach(kv => { HandlesLine(kv.Key, kv.Value, color, is_dotted); });
      }
    }

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="center"></param>
    /// <param name="size"></param>
    /// <param name="mutiply_matrix"></param>
    /// <param name="faceColor"></param>
    /// <param name="outline_color"></param>
    public static void HandlesCenterRect(Vector3 center, Vector2 size, Matrix4x4 mutiply_matrix, Color faceColor,
      Color outline_color)
    {
      var rectangle = new Rectangle3d(center, size, mutiply_matrix);
      Handles.DrawSolidRectangleWithOutline(rectangle.vertexe_list.ToArray(), faceColor, outline_color);
    }

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="mutiply_matrix"></param>
    /// <param name="face_color"></param>
    /// <param name="outline_color"></param>
    public static void HandlesCenterRect(Rect rect, Matrix4x4 mutiply_matrix, Color face_color, Color outline_color)
    {
      HandlesCenterRect(rect.center.ToVector3("x,0,y"), rect.size.ToVector3("x,0,y"), mutiply_matrix, face_color,
        outline_color);
    }

    #endregion

    #region Handles自带的Rect

    public static void HandlesDrawSolidRectangleWithOutline(Rect rect, Color face_color, Color outline_color,
      Transform transform = null)
    {
      Vector3[] rect_vertexs =
      {
        new Vector3(rect.x, rect.y, 0),
        new Vector3(rect.x + rect.width, rect.y, 0),
        new Vector3(rect.x + rect.width, rect.y + rect.height, 0),
        new Vector3(rect.x, rect.y + rect.height, 0)
      };
      if (transform != null)
      {
        for (int i = 0; i < rect_vertexs.Length; i++)
          rect_vertexs[i] = transform.TransformPoint(rect_vertexs[i]);
      }

      Handles.DrawSolidRectangleWithOutline(rect_vertexs, face_color, outline_color);
    }

    #endregion

    #region DrawString

    public static void HandlesDrawString(Vector3 position, string text, Color? colour = null)
    {
      var save_color = GUI.color;

      if (colour.HasValue)
        GUI.color = colour.Value;

//    Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
      Handles.Label(position, text);
      GUI.color = save_color;
      Handles.EndGUI();
    }

    #endregion
  }
}
#endif