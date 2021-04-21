using System;
using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
  [Serializable]
  public partial class Shape3d
  {
    #region field

    public List<Vector3> local_vertex_list = new List<Vector3>();
    protected Matrix4x4 matrix = Matrix4x4.identity;

    /// <summary>
    /// worldOffset不会跟随martix进行位移和旋转或缩放
    /// </summary>
    public Vector3 world_offset = Vector3.zero;

    #endregion

    #region property

    public List<Vector3> vertexe_list
    {
      get
      {
        List<Vector3> result = new List<Vector3>();
        foreach (Vector3 v in local_vertex_list)
        {
          result.Add(ToWorldSpace(v));
        }

        return result;
      }
    }

    public virtual Vector3 center
    {
      get { return ToWorldSpace(Vector3.zero); }
    }

    public Vector3 CenterOfAllPoints()
    {
      Vector3 sum = Vector3.zero;
      foreach (var v in vertexe_list)
      {
        sum += v;
      }

      return sum / vertexe_list.Count;
    }

    public List<Line3d> line_list
    {
      get
      {
        List<Line3d> result = new List<Line3d>();
        for (int i = 0; i < vertexe_list.Count - 1; i++)
          result.Add(new Line3d(vertexe_list[i], vertexe_list[i + 1]));
        if (vertexe_list.Count > 2)
          result.Add(new Line3d(vertexe_list[vertexe_list.Count - 1], vertexe_list[0]));
        return result;
      }
    }



    #endregion



    protected Shape3d(params Vector3[] local_vertexes)
    {
      local_vertex_list.AddRange(local_vertexes);
    }


    #region public method



    public override bool Equals(object obj)
    {
      Shape3d other = obj as Shape3d;
      if (other == null)
        return false;
      if (other.vertexe_list.Count != vertexe_list.Count)
        return false;
      if (!other.world_offset.Equals(world_offset))
        return false;
      for (int i = 0; i < vertexe_list.Count; i++)
      {
        if (vertexe_list[i].Equals(other.vertexe_list[i]))
          return false;
      }

      return true;
    }

    public override int GetHashCode()
    {
      List<Vector3> list = new List<Vector3>(vertexe_list);
      list.Add(world_offset);
      return ObjectUtil.GetHashCode(list.ToArray());
    }

    public virtual List<KeyValuePair<Vector3, Vector3>> GetDrawLineList()
    {
      List<KeyValuePair<Vector3, Vector3>> result = new List<KeyValuePair<Vector3, Vector3>>();
      for (int i = 0; i < vertexe_list.Count - 1; i++)
        result.Add(new KeyValuePair<Vector3, Vector3>(vertexe_list[i], vertexe_list[i + 1]));
      result.Add(new KeyValuePair<Vector3, Vector3>(vertexe_list[vertexe_list.Count - 1], vertexe_list[0]));

      return result;
    }



    public Shape3d AddWorldOffset(Vector3 add_world_offset)
    {
      this.world_offset += add_world_offset;
      return this;
    }

    public T AddWorldOffset<T>(Vector3 add_world_offset) where T : Shape3d
    {
      return (T)AddWorldOffset(add_world_offset);
    }


    public Shape3d MultiplyMatrix(Matrix4x4 martix)
    {
      this.matrix *= martix;
      return this;
    }

    public T MultiplyMatrix<T>(Matrix4x4 martix) where T : Shape3d
    {
      return (T)MultiplyMatrix(martix);
    }

    public Shape3d PreMultiplyMatrix(Matrix4x4 martix)
    {
      this.matrix = martix * this.matrix;
      return this;
    }

    public T PreMultiplyMatrix<T>(Matrix4x4 martix) where T : Shape3d
    {
      return (T)PreMultiplyMatrix(martix);
    }



    public void Reverse()
    {
      this.local_vertex_list.Reverse();
    }



    public Vector3 ToLocalSpace(Vector3 p)
    {
      return matrix.inverse.MultiplyPoint(p - world_offset);
    }



    public Vector3 ToWorldSpace(Vector3 p)
    {
      return matrix.MultiplyPoint(p) + world_offset;
    }


    public Vector3? GetNext(Vector3 cur)
    {
      int cur_index = this.vertexe_list.IndexOf(cur);
      if (cur_index == -1)
        return null;
      return cur_index == vertexe_list.Count - 1 ? vertexe_list[0] : vertexe_list[cur_index + 1];
    }

    public Vector3? GetPre(Vector3 cur)
    {
      int cur_index = this.vertexe_list.IndexOf(cur);
      if (cur_index == -1)
        return null;
      return cur_index == 0 ? vertexe_list[vertexe_list.Count - 1] : vertexe_list[cur_index - 1];
    }

    #endregion





  }


}


