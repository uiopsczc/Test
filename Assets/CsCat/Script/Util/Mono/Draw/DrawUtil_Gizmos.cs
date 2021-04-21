using UnityEngine;

namespace CsCat
{
  public partial class DrawUtil
  {
    public static bool Is_Gizmos_Enable = true;

    #region 画箭头 

    /// <summary>
    ///   画箭头
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    public static void GizmosArrow(Vector3 position, Vector3 direction)
    {
      GizmosArrow(position, direction, DrawUtilConst.GizmosDraw_DefaultColor);
    }

    /// <summary>
    ///   画箭头
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    /// <param name="color"></param>
    public static void GizmosArrow(Vector3 position, Vector3 direction, Color color)
    {
      if (!Is_Gizmos_Enable)
        return;
      using (new GizmosColorScope(color))
      {
        Gizmos.DrawRay(position, direction);
        GizmosCone(position + direction, direction.normalized, 15, color);
      }
    }

    #endregion

    #region 画bounds 

    /// <summary>
    ///   画bounds
    /// </summary>
    /// <param name="bounds"></param>
    public static void GizmosBounds(Bounds bounds)
    {
      GizmosBounds(bounds, DrawUtilConst.GizmosDraw_DefaultColor);
    }

    /// <summary>
    ///   画bounds
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="color"></param>
    public static void GizmosBounds(Bounds bounds, Color color)
    {
      if (!Is_Gizmos_Enable) return;

      using (new GizmosColorScope(color))
      {
        var cube = new Cube3d(bounds.center, bounds.size);
        cube.GetDrawLineList().ForEach(kv => { GizmosLine(kv.Key, kv.Value, color); });
      }
    }

    #endregion

    #region 画Capsule 

    public static void GizmosCapsule(Vector3 start, Vector3 end, float radius)
    {
      GizmosCapsule(start, end, radius, DrawUtilConst.GizmosDraw_DefaultColor);
    }

    public static void GizmosCapsule(Vector3 start, Vector3 end, float radius, Color color)
    {
      if (!Is_Gizmos_Enable) return;

      using (new GizmosColorScope(color))
      {
        GizmosCylinder(start, end, radius, color);
        GizmosSphere(start, radius, color);
        GizmosSphere(end, radius, color);
      }
    }

    #endregion

    #region 画圆 

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    public static void GizmosCircle(Vector3 position, float radius)
    {
      GizmosCircle(position, radius, Matrix4x4.identity, DrawUtilConst.GizmosDraw_DefaultColor);
    }

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    /// <param name="mutiply_matrix"></param>
    public static void GizmosCircle(Vector3 position, float radius, Matrix4x4 mutiply_matrix)
    {
      GizmosCircle(position, radius, mutiply_matrix, DrawUtilConst.GizmosDraw_DefaultColor);
    }

    /// <summary>
    ///   画圆  默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    /// <param name="mutiply_matrix"></param>
    /// <param name="color"></param>
    public static void GizmosCircle(Vector3 position, float radius, Matrix4x4 mutiply_matrix, Color color)
    {
      if (!Is_Gizmos_Enable) return;

      using (new GizmosColorScope(color))
      {
        var circle = new Circle3d(position, radius, mutiply_matrix);

        circle.GetDrawLineList().ForEach(kv => { GizmosLine(kv.Key, kv.Value, color); });
      }
    }

    #endregion


    #region 画圆锥 

    public static void GizmosCone(Vector3 end_point, Vector3 forward)
    {
      GizmosCone(end_point, forward, 45, DrawUtilConst.GizmosDraw_DefaultColor);
    }

    public static void GizmosCone(Vector3 end_point, Vector3 forward, float angle)
    {
      GizmosCone(end_point, forward, angle, DrawUtilConst.GizmosDraw_DefaultColor);
    }

    /// <summary>
    /// </summary>
    /// <param name="end_point">箭头的终点</param>
    /// <param name="forward">箭头的方向</param>
    /// <param name="angle">角度</param>
    /// <param name="color"></param>
    public static void GizmosCone(Vector3 end_point, Vector3 forward, float angle, Color color)
    {
      if (!Is_Gizmos_Enable) return;

      using (new GizmosColorScope(color))
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

        Gizmos.DrawRay(end_point, (-angle_slerp_right).normalized * distance);
        Gizmos.DrawRay(end_point, (-angle_slerp_left).normalized * distance);
        Gizmos.DrawRay(end_point, (-angle_slerp_up).normalized * distance);
        Gizmos.DrawRay(end_point, (-angle_slerp_down).normalized * distance);

        GizmosCircle(end_point - forward, (forward + -angle_slerp_right.normalized * distance).magnitude,
          Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, forward)), color);
        GizmosCircle(end_point - forward / 2, (forward + -angle_slerp_right.normalized * distance).magnitude / 2,
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
    public static void GizmosCylinder(Vector3 start, Vector3 end, float radius)
    {
      GizmosCylinder(start, end, radius, DrawUtilConst.GizmosDraw_DefaultColor);
    }

    /// <summary>
    ///   画圆柱体
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="color"></param>
    /// <param name="radius"></param>
    public static void GizmosCylinder(Vector3 start, Vector3 end, float radius, Color color)
    {
      if (!Is_Gizmos_Enable) return;

      using (new GizmosColorScope(color))
      {
        var up_dir = (end - start).normalized;

        var to_up_matrix = Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, up_dir));
        //顶部的圆形
        GizmosCircle(start, radius, to_up_matrix, color);
        ////底部的圆形
        GizmosCircle(end, radius, to_up_matrix, color);

        GizmosLine(start, end, Color.yellow);

        //四个矩形
        var rect_center = (start + end) / 2;
        var size = new Vector2(radius * 2, (start - end).magnitude);

        var to_right_matrix = Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0));

        GizmosCenterRect(rect_center, size,
          to_up_matrix * to_right_matrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 0)), color);
        GizmosCenterRect(rect_center, size,
          to_up_matrix * to_right_matrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 45)), color);
        GizmosCenterRect(rect_center, size,
          to_up_matrix * to_right_matrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), color);
        GizmosCenterRect(rect_center, size,
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
    public static void GizmosLocalCube(Transform transform, Vector3 offset, Vector3 size)
    {
      GizmosLocalCube(transform, offset, size, DrawUtilConst.GizmosDraw_DefaultColor);
    }


    /// <summary>
    ///   画LocalCube
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    /// <param name="color"></param>
    public static void GizmosLocalCube(Transform transform, Vector3 offset, Vector3 size, Color color)
    {
      if (!Is_Gizmos_Enable) return;

      using (new GizmosColorScope(color))
      {
        var cube = new Cube3d(Vector3.zero, Vector3.one);
        cube.MultiplyMatrix(Matrix4x4.TRS(offset, Quaternion.identity, size));
        cube.PreMultiplyMatrix(transform.localToWorldMatrix);
        cube.GetDrawLineList().ForEach(kv => { GizmosLine(kv.Key, kv.Value, color); });
      }
    }

    /// <summary>
    ///   画LocalCube
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    public static void GizmosLocalCube(Matrix4x4 matrix, Vector3 offset, Vector3 size)
    {
      GizmosLocalCube(matrix, offset, size, DrawUtilConst.GizmosDraw_DefaultColor);
    }

    /// <summary>
    ///   画LocalCube
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    /// <param name="color"></param>
    public static void GizmosLocalCube(Matrix4x4 matrix, Vector3 offset, Vector3 size, Color color)
    {
      if (!Is_Gizmos_Enable) return;

      using (new GizmosColorScope(color))
      {
        var cube = new Cube3d(Vector3.zero, Vector3.one);
        cube.MultiplyMatrix(Matrix4x4.TRS(offset, Quaternion.identity, size));
        cube.PreMultiplyMatrix(matrix);
        cube.GetDrawLineList().ForEach(kv => { GizmosLine(kv.Key, kv.Value, color); });
      }
    }

    #endregion

    public static void GizmosCube(Vector3 min, Vector3 max, Color color)
    {
      if (!Is_Gizmos_Enable) return;

      using (new GizmosColorScope(color))
      {
        var cube = new Cube3d((min + max) / 2, (max - min).Abs());
        cube.GetDrawLineList().ForEach(kv => { GizmosLine(kv.Key, kv.Value, color); });
      }
    }

    #region 画点 

    /// <summary>
    ///   画点
    /// </summary>
    /// <param name="position"></param>
    /// <param name="scale"></param>
    public static void GizmosPoint(Vector3 position, float scale = 1)
    {
      GizmosPoint(position, DrawUtilConst.GizmosDraw_DefaultColor, scale);
    }

    /// <summary>
    ///   画点
    /// </summary>
    /// <param name="position"></param>
    /// <param name="color"></param>
    /// <param name="scale"></param>
    public static void GizmosPoint(Vector3 position, Color color, float scale = 1)
    {
      if (!Is_Gizmos_Enable) return;

      using (new GizmosColorScope(color))
      {
        Gizmos.DrawRay(position + Vector3.up * (scale * 0.5f), -Vector3.up * scale);
        Gizmos.DrawRay(position + Vector3.right * (scale * 0.5f), -Vector3.right * scale);
        Gizmos.DrawRay(position + Vector3.forward * (scale * 0.5f), -Vector3.forward * scale);
      }
    }

    #endregion

    #region 画球 

    /// <summary>
    ///   画球
    /// </summary>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    public static void GizmosSphere(Vector3 position, float radius)
    {
      GizmosSphere(position, radius, DrawUtilConst.GizmosDraw_DefaultColor);
    }

    /// <summary>
    ///   画球
    /// </summary>
    /// <param name="position"></param>
    /// <param name="color"></param>
    /// <param name="radius"></param>
    public static void GizmosSphere(Vector3 position, float radius, Color color)
    {
      if (!Is_Gizmos_Enable) return;

      using (new GizmosColorScope(color))
      {
        var sphere = new Sphere3d(position, radius);
        sphere.GetDrawLineList().ForEach(kv => { GizmosLine(kv.Key, kv.Value, color); });
      }
    }

    #endregion

    #region 画线 

    public static void GizmosLine(Vector3 start, Vector3 end)
    {
      GizmosLine(start, end, DrawUtilConst.GizmosDraw_DefaultColor);
    }

    /// <summary>
    ///   画线
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public static void GizmosLine(Vector3 start, Vector3 end, Color color)
    {
      if (!Is_Gizmos_Enable) return;

      using (new GizmosColorScope(color))
      {
        Gizmos.DrawLine(start, end);
      }

      //LogCat.LogErrorFormat("{0} {1}", start, end);
    }

    #endregion

    #region 画多边形 

    public static void GizmosPolygon(params Vector3[] points)
    {
      GizmosPolygon(Matrix4x4.identity, DrawUtilConst.GizmosDraw_DefaultColor, points);
    }

    public static void GizmosPolygon(Color color, params Vector3[] points)
    {
      GizmosPolygon(Matrix4x4.identity, color, points);
    }

    /// <summary>
    ///   画多边形
    /// </summary>
    /// <param name="mutiply_matrix"></param>
    /// <param name="color"></param>
    /// <param name="points"></param>
    public static void GizmosPolygon(Matrix4x4 mutiply_matrix, Color color, params Vector3[] points)
    {
      if (!Is_Gizmos_Enable)
        return;

      using (new GizmosColorScope(color))
      {
        var polygon = new Polygon3d(points);
        polygon.MultiplyMatrix(mutiply_matrix);

        polygon.GetDrawLineList().ForEach(kv => { GizmosLine(kv.Key, kv.Value, color); });
      }
    }

    #endregion

    #region 画矩形 

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="center"></param>
    /// <param name="size"></param>
    public static void GizmosCenterRect(Vector3 center, Vector2 size)
    {
      GizmosCenterRect(center, size, Matrix4x4.identity, DrawUtilConst.GizmosDraw_DefaultColor);
    }

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="center"></param>
    /// <param name="size"></param>
    /// <param name="mutiply_matrix"></param>
    public static void GizmosCenterRect(Vector3 center, Vector2 size, Matrix4x4 mutiply_matrix)
    {
      GizmosCenterRect(center, size, mutiply_matrix, DrawUtilConst.GizmosDraw_DefaultColor);
    }

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="rect"></param>
    public static void GizmosCenterRect(Rect rect)
    {
      GizmosCenterRect(rect.center.ToVector3("x,0,y"), rect.center.ToVector3("x,0,y"), Matrix4x4.identity,
        DrawUtilConst.GizmosDraw_DefaultColor);
    }

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="mutiply_matrix"></param>
    public static void GizmosCenterRect(Rect rect, Matrix4x4 mutiply_matrix)
    {
      GizmosCenterRect(rect.center.ToVector3("x,0,y"), rect.center.ToVector3("x,0,y"), mutiply_matrix,
        DrawUtilConst.GizmosDraw_DefaultColor);
    }


    /// <summary>
    ///   画中心点在center的矩形
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="center"></param>
    /// <param name="size"></param>
    /// <param name="mutiply_matrix"></param>
    /// <param name="color"></param>
    public static void GizmosCenterRect(Vector3 center, Vector2 size, Matrix4x4 mutiply_matrix, Color color)
    {
      using (new GizmosColorScope(color))
      {
        var rectangle = new Rectangle3d(center, size, mutiply_matrix);
        rectangle.GetDrawLineList().ForEach(kv => { GizmosLine(kv.Key, kv.Value, color); });
      }
    }

    #endregion
  }
}