using UnityEngine;
using System.Collections.Generic;
using System;

namespace CsCat
{
  /// <summary>
  ///  VertexeList;//定点顺时针为:(leftBottom->leftTop->rightTop->rightBottom),逆时针为:(rightBottom->rightTop->leftTop->leftBottom)
  ///  顶点点列表,外边界用按顺时针，内边界用逆时针，区域在方向右侧
  /// </summary>
  [Serializable]
  public class Rectangle : Polygon
  {
    #region field

    protected Vector2 size;

    #endregion

    #region property

    public Vector2 local_left_bottom
    {
      get { return local_vertexe_list[0]; }
    }

    public Vector2 local_left_top
    {
      get { return local_vertexe_list[1]; }
    }

    public Vector2 local_right_top
    {
      get { return local_vertexe_list[2]; }
    }

    public Vector2 local_right_bottom
    {
      get { return local_vertexe_list[3]; }
    }





    public Vector2 left_bottom
    {
      get { return ToWorldSpace(local_vertexe_list[0]); }
    }

    public Vector2 left_top
    {
      get { return ToWorldSpace(local_vertexe_list[1]); }
    }

    public Vector2 right_top
    {
      get { return ToWorldSpace(local_vertexe_list[2]); }
    }

    public Vector2 right_bottom
    {
      get { return ToWorldSpace(local_vertexe_list[3]); }
    }


    #endregion

    #region ctor

    /// <summary>
    /// leftBottom->leftTop->rightTop->rightBottom
    /// </summary>
    /// <param name="center"></param>
    /// <param name="length"></param>
    /// <param name="width"></param>
    /// <param name="angle"></param>
    public Rectangle(Vector2 center, Vector2 size)
    {
      MultiplyMatrix(Matrix4x4.Translate(center));
      this.size = size;


      Vector2 local_left_bottom = new Vector2(-size.x / 2, -size.y / 2);
      Vector2 local_left_top = new Vector2(-size.x / 2, size.y / 2);
      Vector2 local_right_top = new Vector2(size.x / 2, size.y / 2);
      Vector2 local_right_bottom = new Vector2(size.x / 2, -size.y / 2);


      local_vertexe_list.Add(local_left_bottom);
      local_vertexe_list.Add(local_left_top);
      local_vertexe_list.Add(local_right_top);
      local_vertexe_list.Add(local_right_bottom);

    }

    public Rectangle(float local_left_top_x, float local_left_top_y, float local_right_bottom_x,
      float local_right_bottom_y) : this(
      new Vector2(local_left_top_x + local_right_bottom_x, local_left_top_y + local_right_bottom_y) / 2,
      new Vector2(Mathf.Abs(local_left_top_x - local_right_bottom_x),
        Mathf.Abs(local_left_top_y - local_right_bottom_y)))
    {
    }

    #endregion

    #region operator

    public static Rectangle operator +(Rectangle rectangle, Vector2 vector)
    {
      Rectangle clone = CloneUtil.CloneDeep(rectangle);
      clone.AddWorldOffset(vector);
      return clone;
    }

    public static Rectangle operator -(Rectangle rectangle, Vector2 vector)
    {
      Rectangle clone = CloneUtil.CloneDeep(rectangle);
      clone.AddWorldOffset(-vector);
      return clone;
    }

    #endregion


    #region public method

    /// <summary>
    /// http://blog.csdn.net/i_dovelemon/article/details/31420749
    /// </summary>
    /// <param name="rectangle1"></param>
    /// <param name="rectangle2"></param>
    /// <param name="isIgnoreV"></param>
    /// <returns></returns>
    public override bool IsIntersect(Rectangle rectangle2)
    {


      Line line1_1 = new Line(vertexe_list[0], vertexe_list[1]);
      Line line1_2 = new Line(vertexe_list[1], vertexe_list[2]);

      Line line2_1 = new Line(rectangle2.vertexe_list[0], rectangle2.vertexe_list[1]);
      Line line2_2 = new Line(rectangle2.vertexe_list[1], rectangle2.vertexe_list[2]);

      Vector2 normal1_1 = line1_1.GetNormal();
      Vector2 normal1_2 = line1_2.GetNormal();

      Vector2 normal2_1 = line2_1.GetNormal();
      Vector2 normal2_2 = line2_2.GetNormal();



      List<Vector2> axis_list = new List<Vector2>();
      axis_list.Add(normal1_1);
      axis_list.Add(normal1_2);

      axis_list.Add(normal2_1);
      axis_list.Add(normal2_2);

      foreach (Vector2 axis in axis_list)
      {
        Projection p1 = Projection.GetProjection(axis, vertexe_list);
        Projection p2 = Projection.GetProjection(axis, rectangle2.vertexe_list);


        if (!Projection.Overlap(p1, p2))
          return false;
      }

      return true;
    }


    public override bool IsIntersect(Circle circle)
    {
      return circle.IsIntersect(this);
    }



    /// <summary>
    /// 矩形和点的关系
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public override bool Contains(Vector2 p)
    {

      float min_x = center.x - size.x / 2;
      float max_x = center.x + size.x / 2;
      float min_y = center.y - size.y / 2;
      float max_y = center.y + size.y / 2;

      if (p.x >= min_x && p.x <= max_x && p.y >= min_y && p.y <= max_y)
        return true;
      else
        return false;
    }







    #endregion







  }
}

