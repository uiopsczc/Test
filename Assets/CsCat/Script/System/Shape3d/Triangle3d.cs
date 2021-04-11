using System;
using UnityEngine;

namespace CsCat
{
  [Serializable]
  public class Triangle3d : Polygon3d
  {
    #region field

    public const int Side_AB = 0;
    public const int Side_BC = 1;
    public const int Side_CA = 2;

    #endregion

    #region property


    public Vector3 point_A
    {
      get { return ToWorldSpace(local_vertex_list[0]); }
    }

    public Vector3 point_B
    {
      get { return ToWorldSpace(local_vertex_list[1]); }
    }

    public Vector3 point_C
    {
      get { return ToWorldSpace(local_vertex_list[2]); }
    }

    #endregion


    #region ctor

    /// <summary>
    /// 点按顺时针存放
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    public Triangle3d(Vector3 v1, Vector3 v2, Vector3 v3)
    {
    }

    #endregion


    #region static method

    public static Triangle3d operator +(Triangle3d triangle, Vector3 vector)
    {
      Triangle3d clone = CloneUtil.Clone_Deep(triangle);
      clone.AddWorldOffset(vector);
      return clone;
    }

    public static Triangle3d operator -(Triangle3d triangle, Vector3 vector)
    {
      Triangle3d clone = CloneUtil.Clone_Deep(triangle);
      clone.AddWorldOffset(-vector);
      return clone;
    }

    #endregion





    #region public method

    public override string ToString()
    {
      return string.Format("A:{0},B:{1},C:{2}", point_A, point_B, point_C);
    }





    #endregion




  }
}