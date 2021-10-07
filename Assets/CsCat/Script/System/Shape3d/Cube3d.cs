using System;
using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
  [Serializable]
  public class Cube3d : Shape3d
  {
    #region field

    public Vector3 center;
    public Vector3 size;

    #endregion

    #region property

    //顶部的点
    public Vector3 right_top_forward
    {
      get { return ToWorldSpace(local_right_top_forward); }
    }

    public Vector3 right_top_back
    {
      get { return ToWorldSpace(local_right_top_back); }
    }

    public Vector3 left_top_back
    {
      get { return ToWorldSpace(local_left_top_back); }
    }

    public Vector3 left_top_forward
    {
      get { return ToWorldSpace(local_left_top_forward); }
    }


    //底部的点
    public Vector3 right_bottom_forward
    {
      get { return ToWorldSpace(local_right_bottom_forward); }
    }

    public Vector3 right_bottom_back
    {
      get { return ToWorldSpace(local_right_bottom_back); }
    }

    public Vector3 left_bottom_back
    {
      get { return ToWorldSpace(local_left_bottom_back); }
    }

    public Vector3 left_bottom_forward
    {
      get { return ToWorldSpace(local_left_bottom_forward); }
    }

    //顶部的点
    public Vector3 local_right_top_forward
    {
      get { return local_vertex_list[0]; }
    }

    public Vector3 local_right_top_back
    {
      get { return local_vertex_list[1]; }
    }

    public Vector3 local_left_top_back
    {
      get { return local_vertex_list[2]; }
    }

    public Vector3 local_left_top_forward
    {
      get { return local_vertex_list[3]; }
    }


    //底部的点
    public Vector3 local_right_bottom_forward
    {
      get { return local_vertex_list[4]; }
    }

    public Vector3 local_right_bottom_back
    {
      get { return local_vertex_list[5]; }
    }

    public Vector3 local_left_bottom_back
    {
      get { return local_vertex_list[6]; }
    }

    public Vector3 local_left_bottom_forward
    {
      get { return local_vertex_list[7]; }
    }

    #endregion

    #region ctor

    public Cube3d(Vector3 center, Vector3 size)
    {
      MultiplyMatrix(Matrix4x4.Translate(center));
      this.size = size;

      //顶部的点
      Vector3 local_right_top_forward = size * 0.5f;
      Vector3 local_right_top_back = new Vector3(size.x, size.y, -size.z) * 0.5f;
      Vector3 local_left_top_back = new Vector3(-size.x, size.y, -size.z) * 0.5f;
      Vector3 local_left_top_forward = new Vector3(-size.x, size.y, size.z) * 0.5f;


      //底部的点
      Vector3 local_right_bottom_forward = new Vector3(size.x, -size.y, size.z) * 0.5f;
      Vector3 local_right_bottom_back = new Vector3(size.x, -size.y, -size.z) * 0.5f;
      Vector3 local_left_bottom_back = -size * 0.5f;
      Vector3 local_left_bottom_forward = new Vector3(-size.x, -size.y, size.z) * 0.5f;


      local_vertex_list.Add(local_right_top_forward);
      local_vertex_list.Add(local_right_top_back);
      local_vertex_list.Add(local_left_top_back);
      local_vertex_list.Add(local_left_top_forward);

      local_vertex_list.Add(local_right_bottom_forward);
      local_vertex_list.Add(local_right_bottom_back);
      local_vertex_list.Add(local_left_bottom_back);
      local_vertex_list.Add(local_left_bottom_forward);
    }

    #endregion

    #region  operator

    public static Cube3d operator +(Cube3d cube, Vector3 vector)
    {
      Cube3d clone = CloneUtil.CloneDeep(cube);
      clone.AddWorldOffset(vector);
      return clone;
    }

    public static Cube3d operator -(Cube3d cube, Vector3 vector)
    {
      Cube3d clone = CloneUtil.CloneDeep(cube);
      clone.AddWorldOffset(-vector);
      return clone;
    }



    #endregion

    public override List<KeyValuePair<Vector3, Vector3>> GetDrawLineList()
    {

      List<KeyValuePair<Vector3, Vector3>> result = new List<KeyValuePair<Vector3, Vector3>>();



      //顶部面
      result.Add(new KeyValuePair<Vector3, Vector3>(right_top_forward, right_top_back));
      result.Add(new KeyValuePair<Vector3, Vector3>(right_top_back, left_top_back));
      result.Add(new KeyValuePair<Vector3, Vector3>(left_top_back, left_top_forward));
      result.Add(new KeyValuePair<Vector3, Vector3>(left_top_forward, right_top_forward));



      //底部面
      result.Add(new KeyValuePair<Vector3, Vector3>(right_bottom_forward, right_bottom_back));
      result.Add(new KeyValuePair<Vector3, Vector3>(right_bottom_back, left_bottom_back));
      result.Add(new KeyValuePair<Vector3, Vector3>(left_bottom_back, left_bottom_forward));
      result.Add(new KeyValuePair<Vector3, Vector3>(left_bottom_forward, right_bottom_forward));


      //其余四条线
      result.Add(new KeyValuePair<Vector3, Vector3>(right_top_forward, right_bottom_forward));
      result.Add(new KeyValuePair<Vector3, Vector3>(right_top_back, right_bottom_back));
      result.Add(new KeyValuePair<Vector3, Vector3>(left_top_forward, left_bottom_forward));
      result.Add(new KeyValuePair<Vector3, Vector3>(left_top_back, left_bottom_back));

      return result;
    }











  }

}

