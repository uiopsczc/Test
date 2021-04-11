using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
  //专门用于写数字
  public class NumberSquare
  {
    #region field

    public Vector3 middle_middle;
    public float radius;
    public Vector3 left_top;
    public Vector3 middle_top;
    public Vector3 right_top;
    public Vector3 left_middle;
    public Vector3 right_middle;
    public Vector3 left_bottom;
    public Vector3 middle_bottom;
    public Vector3 right_bottom;



    private float factor = 0.8f;

    #endregion

    #region ctor

    public NumberSquare(Vector3 middle_middle, float radius = 0.5f)
    {
      this.middle_middle = middle_middle;
      this.radius = radius;

      this.left_top = middle_middle + factor * radius * new Vector3(-1, 0, 1);
      this.middle_top = middle_middle + factor * radius * new Vector3(0, 0, 1);
      this.right_top = middle_middle + factor * radius * new Vector3(1, 0, 1);

      this.left_middle = middle_middle + factor * radius * new Vector3(-1, 0, 0);
      this.right_middle = middle_middle + factor * radius * new Vector3(1, 0, 0);

      this.left_bottom = middle_middle + factor * radius * new Vector3(-1, 0, -1);
      this.middle_bottom = middle_middle + factor * radius * new Vector3(0, 0, -1);
      this.right_bottom = middle_middle + factor * radius * new Vector3(1, 0, -1);
    }

    #endregion

    #region public method

    public List<Vector3> GetPointList(char c)
    {
      List<Vector3> point_list = new List<Vector3>();
      switch (c)
      {
        case '0':
          point_list.Add(left_top);
          point_list.Add(right_top);
          point_list.Add(right_bottom);
          point_list.Add(left_bottom);
          point_list.Add(left_top);
          break;
        case '1':
          point_list.Add(middle_top);
          point_list.Add(middle_bottom);
          break;
        case '2':
          point_list.Add(left_top);
          point_list.Add(right_top);
          point_list.Add(right_middle);
          point_list.Add(left_middle);
          point_list.Add(left_bottom);
          point_list.Add(right_bottom);
          break;
        case '3':
          point_list.Add(left_top);
          point_list.Add(right_top);
          point_list.Add(right_middle);
          point_list.Add(left_middle);
          point_list.Add(right_middle);
          point_list.Add(right_bottom);
          point_list.Add(left_bottom);
          break;
        case '4':
          point_list.Add(middle_top);
          point_list.Add(left_middle);
          point_list.Add(middle_middle);
          point_list.Add(middle_top);
          point_list.Add(middle_bottom);
          point_list.Add(middle_middle);
          point_list.Add(right_middle);
          break;
        case '5':
          point_list.Add(right_top);
          point_list.Add(left_top);
          point_list.Add(left_middle);
          point_list.Add(right_middle);
          point_list.Add(right_bottom);
          point_list.Add(left_bottom);
          break;
        case '6':
          point_list.Add(right_top);
          point_list.Add(left_top);
          point_list.Add(left_middle);
          point_list.Add(right_middle);
          point_list.Add(right_bottom);
          point_list.Add(left_bottom);
          point_list.Add(left_middle);
          break;
        case '7':
          point_list.Add(left_top);
          point_list.Add(right_top);
          point_list.Add(middle_bottom);
          break;
        case '8':
          point_list.Add(left_top);
          point_list.Add(right_top);
          point_list.Add(right_middle);
          point_list.Add(left_middle);
          point_list.Add(left_top);
          point_list.Add(left_bottom);
          point_list.Add(right_bottom);
          point_list.Add(right_middle);
          break;
        case '9':
          point_list.Add(right_middle);
          point_list.Add(left_middle);
          point_list.Add(left_top);
          point_list.Add(right_top);
          point_list.Add(right_bottom);
          point_list.Add(left_bottom);
          break;
        case '.':
          point_list.Add(middle_bottom + factor * radius * 0.1f * new Vector3(-1, 0, 0));
          point_list.Add(middle_bottom + factor * radius * 0.1f * new Vector3(1, 0, 0));
          point_list.Add(middle_bottom);
          point_list.Add(middle_bottom + factor * radius * 0.1f * new Vector3(0, 0, 1));
          point_list.Add(middle_bottom + factor * radius * 0.1f * new Vector3(0, 0, -1));
          break;
        case '-':
          point_list.Add(left_middle + factor * radius * 0.1f * new Vector3(1, 0, 0));
          point_list.Add(right_middle + factor * radius * 0.1f * new Vector3(-1, 0, 0));
          break;
      }

      return point_list;
    }

    #endregion



  }
}