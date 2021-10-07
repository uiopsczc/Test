using System;
using UnityEngine;

namespace CsCat
{
  [Serializable]
  public class Triangle : Polygon
  {
    #region field

    public const int Side_AB = 0;
    public const int Side_BC = 1;
    public const int Side_CA = 2;

    #endregion

    #region property

    public Vector2 point_A
    {
      get { return ToWorldSpace(local_vertexe_list[0]); }
    }

    public Vector2 point_B
    {
      get { return ToWorldSpace(local_vertexe_list[1]); }
    }

    public Vector2 point_C
    {
      get { return ToWorldSpace(local_vertexe_list[2]); }
    }

    #endregion


    #region ctor

    /// <summary>
    /// 点按顺时针存放
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    public Triangle(Vector2 v1, Vector2 v2, Vector2 v3)
    {
    }

    #endregion


    #region static method

    public static Triangle operator +(Triangle triangle, Vector2 vector)
    {
      Triangle clone = CloneUtil.CloneDeep(triangle);
      clone.AddWorldOffset(vector);
      return clone;
    }

    public static Triangle operator -(Triangle triangle, Vector2 vector)
    {
      Triangle clone = CloneUtil.CloneDeep(triangle);
      clone.AddWorldOffset(-vector);
      return clone;
    }

    #endregion





    #region public method

    public override string ToString()
    {
      return string.Format("A:{0},B:{1},C:{2}", point_A, point_B, point_C);
    }




    //测试给定点是否在三角型中(包括这个点在边界上)
    public bool IsPointIn(Vector2 test_point)
    {
      // 点在所有边的右面
      int count = 0;
      for (int i = 0; i < 3; i++)
      {
        if (line_list[i].ClassifyPoint(test_point) != PointClassification.LEFT_SIDE)
          count++;
      }

      return (count == 3);
    }


    #endregion


  }

}
