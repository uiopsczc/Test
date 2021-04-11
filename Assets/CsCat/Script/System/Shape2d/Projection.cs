using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
  public class Projection
  {
    #region field

    public float min;
    public float max;

    #endregion

    #region ctor

    public Projection(float min, float max)
    {
      this.min = min;
      this.max = max;
    }

    #endregion

    #region static method

    public static bool Overlap(Projection p1, Projection p2)
    {
      if (p1.min > p2.max)
        return false;
      if (p1.max < p2.min)
        return false;
      return true;
    }

    public static Projection GetProjection(Vector2 axie, List<Vector2> vertex_list)
    {
      float min = Vector2.Dot(vertex_list[0], axie);
      float max = min;

      for (int i = 1; i < vertex_list.Count; i++)
      {
        Vector2 verctex = vertex_list[i];
        float tmp = Vector2.Dot(verctex, axie);
        if (tmp > max)
          max = tmp;
        else if (tmp < min)
          min = tmp;
      }

      return new Projection(min, max);
    }

    #endregion



  }
}
