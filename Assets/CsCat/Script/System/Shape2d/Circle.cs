using System;
using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
  [Serializable]
  public class Circle : Polygon
  {
    #region field

    /// <summary>
    /// 半径
    /// </summary>
    public float radius;

    #endregion


    #region ctor

    public Circle(Vector2 world_offset, float radius)
    {
      AddWorldOffset(world_offset);
      this.radius = radius;
    }

    #endregion

    #region  operator

    public static Circle operator +(Circle circle, Vector2 vector)
    {
      Circle clone = CloneUtil.Clone_Deep(circle);
      clone.AddWorldOffset(vector);
      return clone;
    }

    public static Circle operator -(Circle circle, Vector2 vector)
    {
      Circle clone = CloneUtil.Clone_Deep(circle);
      clone.AddWorldOffset(-vector);
      return clone;
    }



    #endregion

    #region public method

    public override bool Equals(object obj)
    {
      Circle other = obj as Circle;
      if (other == null)
        return false;
      if (radius != other.radius)
        return false;
      if (world_offset != other.world_offset)
        return false;

      if (!other.matrix.Equals(this.matrix))
        return false;

      return base.Equals(obj);
    }

    public override string ToString()
    {
      return string.Format("Center:{0},radius:{1}", center, radius);
    }

    public override List<KeyValuePair<Vector2, Vector2>> GetDrawLineList()
    {

      List<KeyValuePair<Vector2, Vector2>> result = new List<KeyValuePair<Vector2, Vector2>>();


      float each_degree = 4;
      int segment_num = (int)Mathf.Ceil(360 / each_degree);
      for (int i = 0; i <= segment_num; i++)
      {
        float pre_degree = i * each_degree;
        float nextDegree = (i + 1) * each_degree;
        Vector2 pre_point =
          ToWorldSpace(new Vector2(Mathf.Cos(pre_degree * Mathf.Deg2Rad), Mathf.Sin(pre_degree * Mathf.Deg2Rad)) *
                       radius);
        Vector2 next_point =
          ToWorldSpace(new Vector2(Mathf.Cos(nextDegree * Mathf.Deg2Rad), Mathf.Sin(nextDegree * Mathf.Deg2Rad)) *
                       radius);
        result.Add(new KeyValuePair<Vector2, Vector2>(pre_point, next_point));
      }

      return result;
    }

    public override bool IsIntersect(Circle circle2)
    {
      float d1 = (center - circle2.center).sqrMagnitude;
      float d2 = (radius + circle2.radius) * (radius + circle2.radius);
      if (d1 <= d2)
      {
        return true;
      }
      else
        return false;
    }

    //https://www.zhihu.com/question/24251545
    public override bool IsIntersect(Rectangle rectangle)
    {
      Vector2 local_center_circle_of_rectangle = rectangle.ToLocalSpace(center);

      Vector2 v = new Vector2(Math.Abs(local_center_circle_of_rectangle.x),
        Math.Abs(local_center_circle_of_rectangle.y));
      Vector2 h = rectangle.right_top;
      Vector2 u = v - h;
      u = new Vector2(Math.Max(u.x, 0), Math.Max(u.y, 0));
      return Vector2.Dot(u, u) <= radius * radius;
    }


    #endregion


  }
}