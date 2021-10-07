
using System;
using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
  [Serializable]
  public class Circle3d : Polygon3d
  {
    #region field

    /// <summary>
    /// 半径
    /// </summary>
    public float radius;

    #endregion


    #region ctor

    /// <summary>
    /// 默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="world_offset"></param>
    /// <param name="radius"></param>
    /// <param name="matrix"></param>
    public Circle3d(Vector3 world_offset, float radius, Matrix4x4 matrix)
    {
      AddWorldOffset(world_offset);
      MultiplyMatrix(matrix);
      this.radius = radius;
    }

    /// <summary>
    /// 默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="world_offset"></param>
    /// <param name="radius"></param>
    public Circle3d(Vector3 world_offset, float radius) : this(world_offset, radius, Matrix4x4.identity)
    {
    }

    /// <summary>
    /// 默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="radius"></param>
    public Circle3d(float radius) : this(Vector3.zero, radius, Matrix4x4.identity)
    {

    }

    #endregion

    #region  operator

    public static Circle3d operator +(Circle3d circle, Vector3 vector)
    {
      Circle3d clone = CloneUtil.CloneDeep(circle);
      clone.AddWorldOffset(vector);
      return clone;
    }

    public static Circle3d operator -(Circle3d circle, Vector3 vector)
    {
      Circle3d clone = CloneUtil.CloneDeep(circle);
      clone.AddWorldOffset(-vector);
      return clone;
    }



    #endregion

    #region public method

    public override bool Equals(object obj)
    {
      Circle3d other = obj as Circle3d;
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

    public override List<KeyValuePair<Vector3, Vector3>> GetDrawLineList()
    {
      List<KeyValuePair<Vector3, Vector3>> result = new List<KeyValuePair<Vector3, Vector3>>();


      float each_degree = 4;
      int segment_num = (int)Mathf.Ceil(360 / each_degree);
      for (int i = 0; i <= segment_num; i++)
      {
        float pre_degree = i * each_degree;
        float next_degree = (i + 1) * each_degree;
        Vector3 pre_point =
          ToWorldSpace(new Vector3(Mathf.Cos(pre_degree * Mathf.Deg2Rad), 0, Mathf.Sin(pre_degree * Mathf.Deg2Rad)) *
                       radius);
        Vector3 next_point =
          ToWorldSpace(new Vector3(Mathf.Cos(next_degree * Mathf.Deg2Rad), 0, Mathf.Sin(next_degree * Mathf.Deg2Rad)) *
                       radius);
        result.Add(new KeyValuePair<Vector3, Vector3>(pre_point, next_point));
      }

      return result;
    }



    #endregion

  }
}
