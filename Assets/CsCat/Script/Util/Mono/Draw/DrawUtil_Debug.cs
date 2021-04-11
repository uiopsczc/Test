using System;
using UnityEngine;

namespace CsCat
{
  public partial class DrawUtil
  {
    public static bool Is_Debug_Enable = true;

    #region 画箭头 

    /// <summary>
    ///   画箭头
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugArrow(Vector3 position, Vector3 direction, float duration = 0, bool is_depth_test = true)
    {
      DebugArrow(position, direction, DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test);
    }

    /// <summary>
    ///   画箭头
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    /// <param name="color"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugArrow(Vector3 position, Vector3 direction, Color color, float duration = 0,
      bool is_depth_test = true)
    {
      if (!Is_Debug_Enable)
        return;
      Debug.DrawRay(position, direction, color, duration, is_depth_test);
      DebugCone(position + direction, direction.normalized, 15, color, duration, is_depth_test);
    }

    #endregion

    #region 画bounds 

    /// <summary>
    ///   画bounds
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugBounds(Bounds bounds, float duration = 0, bool is_depth_test = true)
    {
      DebugBounds(bounds, DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test);
    }

    /// <summary>
    ///   画bounds
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="color"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugBounds(Bounds bounds, Color color, float duration = 0, bool is_depth_test = true)
    {
      if (!Is_Debug_Enable) return;
      var cube = new Cube3d(bounds.center, bounds.size);
      cube.GetDrawLineList().ForEach(kv => { DebugLine(kv.Key, kv.Value, color, duration, is_depth_test); });
    }

    #endregion

    #region 画Capsule 

    public static void DebugCapsule(Vector3 start, Vector3 end, float radius, float duration = 0,
      bool is_depth_test = true)
    {
      DebugCapsule(start, end, radius, DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test);
    }

    public static void DebugCapsule(Vector3 start, Vector3 end, float radius, Color color, float duration = 0,
      bool is_depth_test = true)
    {
      if (!Is_Debug_Enable) return;

      DebugCylinder(start, end, radius, color, duration, is_depth_test);
      DebugSphere(start, radius, color, duration, is_depth_test);
      DebugSphere(end, radius, color, duration, is_depth_test);
    }

    #endregion

    #region 画圆 

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    /// <param name="duration"></param>
    /// <param name="is_depth_test"></param>
    public static void DebugCircle(Vector3 position, float radius,
      float duration = 0, bool is_depth_test = true)
    {
      DebugCircle(position, radius, Matrix4x4.identity, DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test);
    }

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    /// <param name="mutiply_matrix"></param>
    /// <param name="duration"></param>
    /// <param name="is_depth_test"></param>
    public static void DebugCircle(Vector3 position, float radius, Matrix4x4 mutiply_matrix,
      float duration = 0, bool is_depth_test = true)
    {
      DebugCircle(position, radius, mutiply_matrix, DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test);
    }

    /// <summary>
    ///   画圆
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    /// <param name="mutiply_matrix"></param>
    /// <param name="color"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugCircle(Vector3 position, float radius, Matrix4x4 mutiply_matrix, Color color,
      float duration = 0, bool is_depth_test = true)
    {
      if (!Is_Debug_Enable) return;

      var circle = new Circle3d(position, radius, mutiply_matrix);

      circle.GetDrawLineList().ForEach(kv => { DebugLine(kv.Key, kv.Value, color, duration, is_depth_test); });
    }

    #endregion


    #region 画圆锥 

    public static void DebugCone(Vector3 end_point, Vector3 forward, float duration = 0,
      bool is_depth_test = true)
    {
      DebugCone(end_point, forward, 45, DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test);
    }

    public static void DebugCone(Vector3 end_point, Vector3 forward, float angle, float duration = 0,
      bool is_depth_test = true)
    {
      DebugCone(end_point, forward, angle, DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test);
    }

    /// <summary>
    /// </summary>
    /// <param name="end_point">箭头的终点</param>
    /// <param name="forward">箭头的方向</param>
    /// <param name="angle">角度</param>
    /// <param name="color"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugCone(Vector3 end_point, Vector3 forward, float angle, Color color, float duration = 0,
      bool is_depth_test = true)
    {
      if (!Is_Debug_Enable) return;

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

      Debug.DrawRay(end_point, (-angle_slerp_right).normalized * distance, color, duration, is_depth_test);
      Debug.DrawRay(end_point, (-angle_slerp_left).normalized * distance, color, duration, is_depth_test);
      Debug.DrawRay(end_point, (-angle_slerp_up).normalized * distance, color, duration, is_depth_test);
      Debug.DrawRay(end_point, (-angle_slerp_down).normalized * distance, color, duration, is_depth_test);

      DebugCircle(end_point - forward, (forward + -angle_slerp_right.normalized * distance).magnitude,
        Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, forward)), color, duration, is_depth_test);
      DebugCircle(end_point - forward / 2, (forward + -angle_slerp_right.normalized * distance).magnitude / 2,
        Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, forward)), color, duration, is_depth_test);
    }

    #endregion


    #region 画圆柱体 

    /// <summary>
    ///   画圆柱体
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="radius"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugCylinder(Vector3 start, Vector3 end, float radius, float duration = 0,
      bool is_depth_test = true)
    {
      DebugCylinder(start, end, radius, DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test);
    }

    /// <summary>
    ///   画圆柱体
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="color"></param>
    /// <param name="radius"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugCylinder(Vector3 start, Vector3 end, float radius, Color color, float duration = 0,
      bool is_depth_test = true)
    {
      if (!Is_Debug_Enable) return;

      var upDir = (end - start).normalized;

      var to_up_matrix = Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up, upDir));
      //顶部的圆形
      DebugCircle(start, radius, to_up_matrix, color, duration, is_depth_test);
      ////底部的圆形
      DebugCircle(end, radius, to_up_matrix, color, duration, is_depth_test);

      DebugLine(start, end, Color.yellow);

      //四个矩形
      var rect_center = (start + end) / 2;
      var size = new Vector2(radius * 2, (start - end).magnitude);

      var to_right_matrix = Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0));

      DebugCenterRect(rect_center, size,
        to_up_matrix * to_right_matrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 0)), color, duration, is_depth_test);
      DebugCenterRect(rect_center, size,
        to_up_matrix * to_right_matrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 45)), color, duration, is_depth_test);
      DebugCenterRect(rect_center, size,
        to_up_matrix * to_right_matrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90)), color, duration, is_depth_test);
      DebugCenterRect(rect_center, size,
        to_up_matrix * to_right_matrix * Matrix4x4.Rotate(Quaternion.Euler(0, 0, 135)), color, duration, is_depth_test);
    }

    #endregion


    #region 画LocalCube 

    /// <summary>
    ///   画LocalCube
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugLocalCube(Transform transform, Vector3 offset, Vector3 size, float duration = 0,
      bool is_depth_test = true)
    {
      DebugLocalCube(transform, offset, size, DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test);
    }


    /// <summary>
    ///   画LocalCube
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    /// <param name="color"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugLocalCube(Transform transform, Vector3 offset, Vector3 size, Color color,
      float duration = 0,
      bool is_depth_test = true)
    {
      if (!Is_Debug_Enable) return;
      var cube = new Cube3d(Vector3.zero, Vector3.one);
      cube.MultiplyMatrix(Matrix4x4.TRS(offset, Quaternion.identity, size));
      cube.PreMultiplyMatrix(transform.localToWorldMatrix);


      cube.GetDrawLineList().ForEach(kv => { DebugLine(kv.Key, kv.Value, color, duration, is_depth_test); });
    }

    /// <summary>
    ///   画LocalCube
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugLocalCube(Matrix4x4 matrix, Vector3 offset, Vector3 size, float duration = 0,
      bool is_depth_test = true)
    {
      DebugLocalCube(matrix, offset, size, DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test);
    }

    /// <summary>
    ///   画LocalCube
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    /// <param name="color"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugLocalCube(Matrix4x4 matrix, Vector3 offset, Vector3 size, Color color, float duration = 0,
      bool is_depth_test = true)
    {
      if (!Is_Debug_Enable) return;

      var cube = new Cube3d(Vector3.zero, Vector3.one);
      cube.MultiplyMatrix(Matrix4x4.TRS(offset, Quaternion.identity, size));
      cube.PreMultiplyMatrix(matrix);
      cube.GetDrawLineList().ForEach(kv => { DebugLine(kv.Key, kv.Value, color, duration, is_depth_test); });
    }

    #endregion

    #region 画Cube 
    public static void DebugCube(Vector3 min, Vector3 max, Color color,
      float duration = 0,
      bool is_depth_test = true)
    {
      if (!Is_Debug_Enable) return;
      var cube = new Cube3d((min+max)/2, (max-min).Abs());
      cube.GetDrawLineList().ForEach(kv => { DebugLine(kv.Key, kv.Value, color, duration, is_depth_test); });
    }
    #endregion

    #region 画点 

    /// <summary>
    ///   画点
    /// </summary>
    /// <param name="position"></param>
    /// <param name="scale"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugPoint(Vector3 position, float scale = 1, float duration = 0, bool is_depth_test = true)
    {
      DebugPoint(position, DrawUtilConst.DebugDraw_DefaultColor, scale, duration, is_depth_test);
    }

    /// <summary>
    ///   画点
    /// </summary>
    /// <param name="position"></param>
    /// <param name="color"></param>
    /// <param name="scale"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugPoint(Vector3 position, Color color, float scale = 1, float duration = 0,
      bool is_depth_test = true)
    {
      if (!Is_Debug_Enable) return;
      Debug.DrawRay(position + Vector3.up * (scale * 0.5f), -Vector3.up * scale, color, duration, is_depth_test);
      Debug.DrawRay(position + Vector3.right * (scale * 0.5f), -Vector3.right * scale, color, duration, is_depth_test);
      Debug.DrawRay(position + Vector3.forward * (scale * 0.5f), -Vector3.forward * scale, color, duration,
        is_depth_test);
    }

    #endregion

    #region 画球 

    /// <summary>
    ///   画球
    /// </summary>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugSphere(Vector3 position, float radius, float duration = 0, bool is_depth_test = true)
    {
      DebugSphere(position, radius, DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test);
    }

    /// <summary>
    ///   画球
    /// </summary>
    /// <param name="position"></param>
    /// <param name="color"></param>
    /// <param name="radius"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugSphere(Vector3 position, float radius, Color color, float duration = 0,
      bool is_depth_test = true)
    {
      if (!Is_Debug_Enable) return;

      var sphere = new Sphere3d(position, radius);
      sphere.GetDrawLineList().ForEach(kv => { DebugLine(kv.Key, kv.Value, color, duration, is_depth_test); });
    }

    #endregion

    #region 画线 

    public static void DebugLine(Vector3 start, Vector3 end, float duration = 0, bool is_depth_test = true)
    {
      DebugLine(start, end, DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test);
    }

    /// <summary>
    ///   画线
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="color"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    public static void DebugLine(Vector3 start, Vector3 end, Color color, float duration = 0, bool is_depth_test = true)
    {
      if (!Is_Debug_Enable) return;
      Debug.DrawLine(start, end, color, duration, is_depth_test);

      //LogCat.LogErrorFormat("{0} {1}", start, end);
    }

    #endregion

    #region 画多边形 

    public static void DebugPolygon(params Vector3[] points)
    {
      DebugPolygon(Matrix4x4.identity, DrawUtilConst.DebugDraw_DefaultColor, 0, true, points);
    }


    public static void DebugPolygon(bool is_depth_test, params Vector3[] points)
    {
      DebugPolygon(Matrix4x4.identity, DrawUtilConst.DebugDraw_DefaultColor, 0, is_depth_test, points);
    }

    public static void DebugPolygon(float duration, bool is_depth_test, params Vector3[] points)
    {
      DebugPolygon(Matrix4x4.identity, DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test, points);
    }

    public static void DebugPolygon(Color color, float duration, bool is_depth_test, params Vector3[] points)
    {
      DebugPolygon(Matrix4x4.identity, color, duration, is_depth_test, points);
    }

    /// <summary>
    ///   画多边形
    /// </summary>
    /// <param name="mutiply_matrix"></param>
    /// <param name="color"></param>
    /// <param name="duration">持续时间</param>
    /// <param name="is_depth_test"></param>
    /// <param name="points"></param>
    public static void DebugPolygon(Matrix4x4 mutiply_matrix, Color color, float duration, bool is_depth_test,
      params Vector3[] points)
    {
      if (!Is_Debug_Enable)
        return;


      var polygon = new Polygon3d(points);
      polygon.MultiplyMatrix(mutiply_matrix);

      polygon.GetDrawLineList().ForEach(kv => { DebugLine(kv.Key, kv.Value, color, duration, is_depth_test); });
    }

    #endregion

    #region 画矩形 

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="center"></param>
    /// <param name="size"></param>
    /// <param name="duration"></param>
    /// <param name="is_depth_test"></param>
    public static void DebugCenterRect(Vector3 center, Vector2 size, float duration = 0, bool is_depth_test = true)
    {
      DebugCenterRect(center, size, Matrix4x4.identity, DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test);
    }

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="center"></param>
    /// <param name="size"></param>
    /// <param name="mutiply_matrix"></param>
    /// <param name="duration"></param>
    /// <param name="is_depth_test"></param>
    public static void DebugCenterRect(Vector3 center, Vector2 size, Matrix4x4 mutiply_matrix, float duration = 0,
      bool is_depth_test = true)
    {
      DebugCenterRect(center, size, mutiply_matrix, DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test);
    }

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="duration"></param>
    /// <param name="is_depth_test"></param>
    public static void DebugCenterRect(Rect rect, float duration = 0, bool is_depth_test = true)
    {
      DebugCenterRect(rect.center.ToVector3("x,0,y"), rect.size.ToVector3("x,0,y"), Matrix4x4.identity,
        DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test);
    }

    /// <summary>
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="mutiply_matrix"></param>
    /// <param name="duration"></param>
    /// <param name="is_depth_test"></param>
    public static void DebugCenterRect(Rect rect, Matrix4x4 mutiply_matrix, float duration = 0,
      bool is_depth_test = true)
    {
      DebugCenterRect(rect.center.ToVector3("x,0,y"), rect.size.ToVector3("x,0,y"), mutiply_matrix,
        DrawUtilConst.DebugDraw_DefaultColor, duration, is_depth_test);
    }


    /// <summary>
    ///   画中心点在center的矩形
    ///   默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="center"></param>
    /// <param name="size"></param>
    /// <param name="upDir"></param>
    /// <param name="mutiply_matrix"></param>
    /// <param name="color"></param>
    /// <param name="duration"></param>
    /// <param name="is_depth_test"></param>
    public static void DebugCenterRect(Vector3 center, Vector2 size, Matrix4x4 mutiply_matrix, Color color,
      float duration = 0, bool is_depth_test = true)
    {
      var rectangle = new Rectangle3d(center, size, mutiply_matrix);
      rectangle.GetDrawLineList().ForEach(kv => { DebugLine(kv.Key, kv.Value, color, duration, is_depth_test); });
    }

    #endregion
  }
}