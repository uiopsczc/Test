using UnityEngine;
using System.Collections.Generic;
using System;

namespace CsCat
{
  [Serializable]
  public partial class Polygon : Shape2d
  {
    #region field

    /// <summary>
    /// 是否是顺时针
    /// </summary>
    private bool is_clockwise = true;

    #endregion

    #region property

    /// <summary>
    /// 矩形包围盒
    /// </summary>
    public Rectangle box_rectangle
    {
      get
      {
        if (vertexe_list == null || vertexe_list.Count < 0)
          return null;

        float lx = vertexe_list[0].x;
        float rx = vertexe_list[0].x;
        float ty = vertexe_list[0].y;
        float by = vertexe_list[0].y;

        for (int i = 1; i < vertexe_list.Count; i++)
        {
          var v = vertexe_list[i];
          if (v.x < lx)
          {
            lx = v.x;
          }

          if (v.x > rx)
          {
            rx = v.x;
          }

          if (v.y < by)
          {
            by = v.y;
          }

          if (v.y > ty)
          {
            ty = v.y;
          }
        }

        Vector2 center = new Vector2((lx + rx) / 2, (ty + by) / 2);
        float length = Math.Abs(rx - lx);
        float width = Math.Abs(ty - by);
        return new Rectangle(center, new Vector2(length, width));
      }
    }

    #endregion


    #region ctor

    /// <summary>
    /// 默认顺时针方向,不需要加起始点，最后程序会自动加起始点
    /// </summary>
    /// <param name="local_vertexes"></param>
    public Polygon(params Vector2[] local_vertexes) : base(local_vertexes)
    {
    }



    #endregion

    #region operator

    public static Polygon operator +(Polygon polygon, Vector2 vector)
    {
      Polygon clone = CloneUtil.CloneDeep(polygon);
      clone.AddWorldOffset(vector);
      return clone;
    }

    public static Polygon operator -(Polygon polygon, Vector2 vector)
    {
      Polygon clone = CloneUtil.CloneDeep(polygon);
      clone.AddWorldOffset(-vector);
      return clone;
    }

    #endregion



    #region public method



    #region 多边形和点的关系

    /// <summary>
    /// 点是否在多边形内部
    /// http://www.cnblogs.com/zhangshu/archive/2011/08/08/2130694.html
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public virtual bool Contains(Vector2 point)
    {
      int count = 0;
      //minValue+100000,因为再减去一个数，会越界
      Line L = new Line(new Vector2(float.MinValue + 100000, point.y), point);

      foreach (Line S in this.line_list)
      {
        if (S.ClassifyPoint(point) == PointClassification.ON_SEGMENT)
          return true;

        if (!S.GetDirection().EqualsEPSILON(Vector2.right) &&
            !S.GetDirection().EqualsEPSILON(-Vector2.right))
        {
          bool a_on_L = L.ClassifyPoint(S.point_A) == PointClassification.ON_SEGMENT;
          bool b_on_L = L.ClassifyPoint(S.point_B) == PointClassification.ON_SEGMENT;
          if (a_on_L || b_on_L)
          {
            if (a_on_L)
              if (S.point_A.y > S.point_B.y)
                count++;
            if (b_on_L)
              if (S.point_B.y > S.point_A.y)
                count++;
          }
          else
          {
            Vector2? intersect_point;

            Line.LineClassification line_relation = S.Intersection(L, out intersect_point);
            if (line_relation == Line.LineClassification.SEGMENTS_INTERSECT)
              count++;
          }
        }
      }

      if (count % 2 == 0)
        return false;
      else
        return true;
    }

    #endregion

    #region 该多边形离指定点最近的点

    public Vector2? GetClosestValidPoint(Vector2 p2, List<Line> constaint_line_list, List<Vector2> constaint_point_list,
      bool is_add_constaint_line_points = false)
    {
      List<Vector2> closest_point_list = new List<Vector2>();
      foreach (Line line in this.line_list)
      {
        var closestPoint = line.GetClosestPoint(p2);
        closest_point_list.Add(closestPoint);
      }

      //是否将限制线段的端点加进去进行最近点计算判断
      if (is_add_constaint_line_points)
      {
        foreach (Line constaint_line in constaint_line_list)
        {
          closest_point_list.Add(constaint_line.point_A);
          closest_point_list.Add(constaint_line.point_B);
        }
      }

      //排序
      closest_point_list.Sort(delegate (Vector2 v1, Vector2 v2)
        {
          float distance_sqr1 = (p2 - v1).sqrMagnitude;
          float distance_sqr2 = (p2 - v2).sqrMagnitude;
          if (distance_sqr1 < distance_sqr2)
            return -1;
          else if (distance_sqr1 == distance_sqr2)
            return 0;
          else
            return 1;
        }
      );

      if (constaint_line_list == null || constaint_line_list.Count == 0)
      {
        return closest_point_list[0];
      }

      while (closest_point_list.Count != 0)
      {
        Vector2 cur = closest_point_list.RemoveFirst<Vector2>();
        bool is_goto_next = false;
        foreach (Line constaint_line in constaint_line_list)
        {
          if (constaint_point_list.Contains(cur))
          {
            is_goto_next = true;
            break;
          }
          else if (constaint_line.point_A == cur || constaint_line.point_B == cur)
          {
            return cur;
          }
          else if (constaint_line.Contains(cur))
          {
            is_goto_next = true;
            break;
          }
        }

        if (is_goto_next)
          continue;
        else
          return cur;
      }

      LogCat.LogError("did not find closest point");
      return null;
    }

    #endregion

    #region 线段和多边形的关系



    /// <summary>
    /// 线段是否在多边形内,整个线段都在多边形内，只有一部分在多边形内的算作在多边形外
    /// http://www.cnblogs.com/zhangshu/archive/2011/08/08/2130694.html
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    public virtual bool Contains(Line line)
    {
      Line PQ = line;
      //bool a = this.Contains(PQ.PointA);
      //bool b = this.Contains(PQ.PointB);
      if (!this.Contains(PQ.point_A) || !this.Contains(PQ.point_B))
        return false;
      List<Vector2> point_set = new List<Vector2>();
      foreach (Line S in line_list)
      {
        bool a_on_S = S.ClassifyPoint(PQ.point_A) == PointClassification.ON_SEGMENT;
        bool b_on_S = S.ClassifyPoint(PQ.point_B) == PointClassification.ON_SEGMENT;
        bool a_on_PQ = PQ.ClassifyPoint(S.point_A) == PointClassification.ON_SEGMENT;
        bool b_on_PQ = PQ.ClassifyPoint(S.point_B) == PointClassification.ON_SEGMENT;
        if (a_on_S || b_on_S)
        {
          if (a_on_S)
            point_set.Add(PQ.point_A);
          if (b_on_S)
            point_set.Add(PQ.point_B);
        }
        else if (a_on_PQ || b_on_PQ)
        {
          if (a_on_PQ)
            point_set.Add(S.point_A);
          if (b_on_PQ)
            point_set.Add(S.point_B);
        }
        else
        {
          Vector2? intersect_point;
          if (PQ.Intersection(S, out intersect_point) == Line.LineClassification.SEGMENTS_INTERSECT)
            return false;
        }
      }

      //排序,先比较x，如果相同再比较y
      point_set.Sort(delegate (Vector2 v1, Vector2 v2)
        {
          if (v1.x < v2.x)
            return -1;
          else if (v1.x == v2.x && v1.y < v2.y)
            return -1;
          else if (v1 == v2)
            return 0;
          else
            return 1;
        }
      );

      for (int i = 1; i < point_set.Count; i++)
      {
        Vector2 v1 = point_set[i - 1];
        Vector2 v2 = point_set[i];
        Vector2 v = (v1 + v2) / 2;
        if (!Contains(v))
          return false;
      }

      return true;

    }

    #endregion

    #region 和另一个多边形的关系

    public Classification ClassifyPolygon(Polygon other)
    {
      foreach (Line other_line in other.line_list)
      {
        foreach (Line this_line in line_list)
        {
          Vector2? out_point;
          if (this_line.Intersection(other_line, out out_point) != Line.LineClassification.SEGMENTS_NOT_INTERSECT) //相交
          {
            return Classification.INTERSECT;
          }
        }
      }

      bool is_contain = true;
      foreach (Vector2 other_point in other.vertexe_list)
      {
        if (!this.Contains(other_point))
        {
          is_contain = false;
          break;
        }
      }

      if (is_contain)
        return Classification.CONTAIN;

      bool is_contained = true;
      foreach (Vector2 this_point in this.vertexe_list)
      {
        if (!other.Contains(this_point))
        {
          is_contained = false;
          break;
        }
      }

      if (is_contained)
        return Classification.CONTAINED;

      return Classification.ISOLATE;
    }

    #endregion

    /// <summary>
    /// 设置为顶点方向;true:顺时针方向,false:逆时针方向
    /// </summary>
    /// <param name="value"></param>
    public void SetClockWise(bool value)
    {
      if (this.is_clockwise != value)
        Reverse();
      this.is_clockwise = value;
    }

    public bool GetClockWise()
    {
      return this.is_clockwise;
    }

    /// <summary>
    /// 将同一条线段上的多个顶点转化为该线段的两个端点，从而减少不必要的顶点
    /// </summary>
    public void Systemize()
    {
      List<Vector2> ret = new List<Vector2>();
      Vector2 last_dir = Vector2.zero;
      for (int i = 0; i < line_list.Count; i++)
      {
        Line line = line_list[i];
        Vector2 cur_dir = line.GetDirection();
        if (cur_dir == Vector2.zero)
        {
          if (i == 0)
            ret.Add(line.local_vertexe_list[0]);
        }
        else
        {
          if (cur_dir.EqualsEPSILON(last_dir))
            continue;
          else
          {
            last_dir = cur_dir;
            ret.Add(line.local_vertexe_list[0]);
          }
        }
      }

      this.local_vertexe_list = ret;
    }

    #endregion

  }
}
