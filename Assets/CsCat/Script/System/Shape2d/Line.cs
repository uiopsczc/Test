using System;
using UnityEngine;

namespace CsCat
{
  [Serializable]
  public partial class Line : Polygon
  {
    #region property



    /// <summary>
    /// 起始点
    /// </summary>
    public Vector2 point_A
    {
      get { return ToWorldSpace(local_vertexe_list[0]); }
    }

    /// <summary>
    /// 终点
    /// </summary>
    public Vector2 point_B
    {
      get { return ToWorldSpace(local_vertexe_list[1]); }
    }

    #endregion

    #region ctor

    public Line(Vector2 start, Vector2 end) : base(start, end)
    {
    }

    #endregion

    #region static method

    public static Line operator +(Line line, Vector2 vector)
    {
      Line clone = CloneUtil.CloneDeep(line);
      clone.AddWorldOffset(vector);
      return clone;
    }

    public static Line operator -(Line line, Vector2 vector)
    {
      Line clone = CloneUtil.CloneDeep(line);
      clone.AddWorldOffset(-vector);
      return clone;
    }

    #endregion

    #region override method




    #region ToString

    public override string ToString()
    {
      return string.Format("A:{0} B:{1}", this.point_A, this.point_B);
    }

    #endregion

    #endregion

    #region public method

    #region 通用方法

    /// <summary>
    /// 线段的方向
    /// </summary>
    /// <returns></returns>
    public Vector2 GetDirection()
    {
      Vector2 direction = point_B - point_A;
      return direction.normalized;
    }

    /// <summary>
    /// 线段的法向量
    /// </summary>
    /// <returns></returns>
    public Vector2 GetNormal()
    {
      Vector2 dir = GetDirection();
      float old_y_value = dir.y;
      Vector2 normal;
      normal.y = dir.x;
      normal.x = -old_y_value;
      return normal;
    }

    /// <summary>
    /// 给定点到直线的带符号距离，从a点朝向b点，左向为正，右向为负
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public float SignedDistance(Vector2 p)
    {
      Vector2 ap = p - point_A;
      return Vector2.Dot(ap, this.GetNormal());

    }

    #endregion

    #region 线段和点的关系

    public override bool Contains(Vector2 point)
    {
      if (point.EqualsEPSILON(this.point_A) || point.EqualsEPSILON(this.point_B))
        return true;
      Vector2 dir1 = point - this.point_A;
      dir1.Normalize();
      Vector2 dir2 = point - this.point_B;
      dir2.Normalize();
      if (SignedDistance(point).EqualsEPSILON(0) && Vector2.Dot(dir1, dir2) < 0)
        return true;
      else
        return false;

    }

    /// <summary>
    /// 判断点与直线的关系，假设你站在a点朝向b点，
    /// </summary>
    /// <param name="p"></param>
    /// <param name="epsilon"></param>
    /// <returns></returns>
    public PointClassification ClassifyPoint(Vector2 p, float epsilon = FloatConst.Epsilon)
    {
      if (this.Contains(p))
        return PointClassification.ON_SEGMENT;

      float distance = SignedDistance(p);

      if (distance > epsilon)
        return PointClassification.LEFT_SIDE;
      else if (distance < -epsilon)
        return PointClassification.RIGHT_SIDE;
      else
        return PointClassification.ON_LINE;
    }

    #endregion

    #region 线段和线段的关系

    public override bool Contains(Line line2)
    {
      if (this.Contains(line2.point_A) && this.Contains(line2.point_B))
        return true;
      else
        return false;
    }

    public LineClassification Intersection_Line(Line line2, out Vector2? intersectPoint)
    {
      Vector2 P1 = line2.point_A;
      Vector2 P2 = line2.point_B;
      float extend = 10000;
      Line line2_line = new Line(P1 - line2.GetDirection() * extend, P2 + line2.GetDirection() * extend);
      return Intersection(line2_line, out intersectPoint);
    }

    /// <summary>
    /// 判断两个直线关系 intersectPoint交点
    /// http://blog.163.com/caty_nuaa/blog/static/90390720104252210791/
    /// </summary>
    /// <param name="line2"></param>
    /// <param name="intersect_point"></param>
    /// <returns></returns>
    public LineClassification Intersection(Line line2, out Vector2? intersect_point)
    {
      Vector2 P1 = this.point_A;
      Vector2 P2 = this.point_B;
      Vector2 Q1 = line2.point_A;
      Vector2 Q2 = line2.point_B;
      intersect_point = null;
      //快速排斥试验
      float min_x_1 = Math.Min(P1.x, P2.x);
      float max_x_1 = Math.Max(P1.x, P2.x);
      float min_y_1 = Math.Min(P1.y, P2.y);
      float max_y_1 = Math.Max(P1.y, P2.y);
      float min_x_2 = Math.Min(Q1.x, Q2.x);
      float max_x_2 = Math.Max(Q1.x, Q2.x);
      float min_y_2 = Math.Min(Q1.y, Q2.y);
      float max_y_2 = Math.Max(Q1.y, Q2.y);
      if (min_x_1 > max_x_2 || max_x_1 < min_x_2 || min_y_1 > max_y_2 || max_y_1 < min_y_2)
        return LineClassification.SEGMENTS_NOT_INTERSECT;

      //跨立实验
      //P1P2跨立Q1Q2: ( P1 - Q1 ) × ( Q2 - Q1 ) * ( Q2 - Q1 ) × ( P2 - Q1 ) >= 0
      float f1 = (P1 - Q1).Cross(Q2 - Q1) * (Q2 - Q1).Cross(P2 - Q1);
      //Q1Q2跨立P1P2:( Q1 - P1 ) × ( P2 - P1 ) * ( P2 - P1 ) × ( Q2 - P1 ) >= 0
      float f2 = (Q1 - P1).Cross(P2 - P1) * (P2 - P1).Cross(Q2 - P1);



      if (f1 >= 0 && f2 >= 0)
      {
        //求交点http://blog.csdn.net/dgq8211/article/details/7952825
        float s1 = MathUtil.Area(P1, P2, Q1);
        float s2 = MathUtil.Area(P1, P2, Q2);

        if (s1.EqualsEPSILON(0) && s2.EqualsEPSILON(0)) //重叠
        {
          //判断端点相同的情况
          if (line2.point_A.EqualsEPSILON(this.point_A))
          {
            bool is_same_dir = Vector2.Dot(line2.GetDirection(), this.GetDirection()) > 0;
            if (is_same_dir)
              return LineClassification.COLLINEAR;
            intersect_point = line2.point_A;
            return LineClassification.SEGMENTS_INTERSECT;
          }
          else if (line2.point_A.EqualsEPSILON(this.point_B))
          {
            bool is_same_dir = Vector2.Dot(line2.GetDirection(), this.GetDirection()) > 0;
            if (!is_same_dir)
              return LineClassification.COLLINEAR;
            intersect_point = line2.point_A;
            return LineClassification.SEGMENTS_INTERSECT;
          }
          else if (line2.point_B.EqualsEPSILON(this.point_A))
          {
            bool is_same_dir = Vector2.Dot(line2.GetDirection(), this.GetDirection()) > 0;
            if (!is_same_dir)
              return LineClassification.COLLINEAR;
            intersect_point = line2.point_B;
            return LineClassification.SEGMENTS_INTERSECT;
          }
          else if (line2.point_B.EqualsEPSILON(this.point_B))
          {
            bool sameDir = Vector2.Dot(line2.GetDirection(), this.GetDirection()) > 0;
            if (sameDir)
              return LineClassification.COLLINEAR;
            intersect_point = line2.point_B;
            return LineClassification.SEGMENTS_INTERSECT;
          }

          return LineClassification.COLLINEAR;
        }

        float x = (Q2.x * s1 + Q1.x * s2) / (s1 + s2);
        float y = (Q2.y * s1 + Q1.y * s2) / (s1 + s2);
        intersect_point = new Vector2(x, y);
        return LineClassification.SEGMENTS_INTERSECT;
      }

      return LineClassification.SEGMENTS_NOT_INTERSECT;
    }

    #endregion

    #region 该线段离指定点最近的点

    /// <summary>
    /// 到线段最近的点
    /// 搜索“计算集合常用算法概览”中的“计算点到线段的最近点”
    /// </summary>
    /// <param name="p2"></param>
    /// <returns></returns>
    public Vector2 GetClosestPoint(Vector2 p2)
    {
      Vector2 tmp;
      Vector2 dir1 = this.GetDirection();

      if (this.Contains(p2))
        return p2;


      if (dir1.EqualsEPSILON(Vector2.right) || dir1.EqualsEPSILON(-Vector2.right)) //水平方向
        tmp = new Vector2(p2.x, this.point_A.y);
      else if (dir1.EqualsEPSILON(Vector2.up) || dir1.EqualsEPSILON(-Vector2.up)) //垂直方向
        tmp = new Vector2(this.point_A.x, p2.y);
      else //该线段不平行于X轴也不平行于Y轴
      {
        float k = (this.point_B.y - this.point_A.y) / (this.point_B.x - this.point_A.x);
        float x = (k * k * this.point_A.x + k * (p2.y - this.point_A.y) + p2.x) / (k * k + 1);
        float y = k * (x - this.point_A.x) + this.point_A.y;
        tmp = new Vector2(x, y);
      }

      var closest_point = CalculateClosestPoint(tmp);
      return closest_point;
    }


    #endregion

    #endregion

    #region private method

    /// <summary>
    /// 如果点在线段上，返回该点,否则返回到该点最近的端点
    /// </summary>
    /// <param name="p2"></param>
    /// <returns></returns>
    private Vector2 CalculateClosestPoint(Vector2 p2)
    {
      if (this.Contains(p2))
        return p2;
      else
      {
        float d1 = (p2 - this.point_A).sqrMagnitude;
        float d2 = (p2 - this.point_B).sqrMagnitude;
        if (d1 < d2)
          return this.point_A;
        else
          return this.point_B;
      }
    }

    #endregion


  }
}
