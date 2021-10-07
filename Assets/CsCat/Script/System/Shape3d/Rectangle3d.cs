
using UnityEngine;
using System;
namespace CsCat
{
  /// <summary>
  ///  VertexeList;//定点顺时针为:(leftBottom->leftTop->rightTop->rightBottom),逆时针为:(rightBottom->rightTop->leftTop->leftBottom)
  ///  顶点点列表,外边界用按顺时针，内边界用逆时针，区域在方向右侧
  ///  默认Matrix4x4.identity是向上
  /// </summary>
  [Serializable]
  public class Rectangle3d : Polygon3d
  {
    #region field

    protected Vector2 _size;

    #endregion

    #region property

    public Vector3 local_left_bottom
    {
      get { return local_vertex_list[0]; }
    }

    public Vector3 local_left_top
    {
      get { return local_vertex_list[1]; }
    }

    public Vector3 local_right_top
    {
      get { return local_vertex_list[2]; }
    }

    public Vector3 local_right_bottom
    {
      get { return local_vertex_list[3]; }
    }


    public Vector2 size
    {
      get { return _size; }
    }



    public Vector3 left_bottom
    {
      get { return ToWorldSpace(local_vertex_list[0]); }
    }

    public Vector3 left_top
    {
      get { return ToWorldSpace(local_vertex_list[1]); }
    }

    public Vector3 right_top
    {
      get { return ToWorldSpace(local_vertex_list[2]); }
    }

    public Vector3 right_bottom
    {
      get { return ToWorldSpace(local_vertex_list[3]); }
    }


    #endregion

    #region ctor

    /// <summary>
    /// 默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="center"></param>
    /// <param name="size"></param>
    public Rectangle3d(Vector3 center, Vector2 size) : this(center, size, Matrix4x4.identity)
    {
    }

    /// <summary>
    /// leftBottom->leftTop->rightTop->rightBottom
    /// 默认Matrix4x4.identity是向上
    /// </summary>
    /// <param name="center"></param>
    /// <param name="length"></param>
    /// <param name="width"></param>
    /// <param name="angle"></param>
    public Rectangle3d(Vector3 center, Vector2 size, Matrix4x4 matrix)
    {
      MultiplyMatrix(Matrix4x4.Translate(center));
      MultiplyMatrix(matrix);
      this._size = size;


      Vector3 local_left_bottom = new Vector3(-this.size.x / 2, 0, -this.size.y / 2);
      Vector3 local_left_top = new Vector3(-this.size.x / 2, 0, this.size.y / 2);
      Vector3 local_right_top = new Vector3(this.size.x / 2, 0, this.size.y / 2);
      Vector3 local_right_bottom = new Vector3(this.size.x / 2, 0, -this.size.y / 2);


      local_vertex_list.Add(local_left_bottom);
      local_vertex_list.Add(local_left_top);
      local_vertex_list.Add(local_right_top);
      local_vertex_list.Add(local_right_bottom);
    }

    #endregion

    #region operator

    public static Rectangle3d operator +(Rectangle3d rectangle, Vector3 vector)
    {
      Rectangle3d clone = CloneUtil.CloneDeep(rectangle);
      clone.AddWorldOffset(vector);
      return clone;
    }

    public static Rectangle3d operator -(Rectangle3d rectangle, Vector3 vector)
    {
      Rectangle3d clone = CloneUtil.CloneDeep(rectangle);
      clone.AddWorldOffset(-vector);
      return clone;
    }

    #endregion







  }
}

