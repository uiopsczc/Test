using System;
using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{

  [Serializable]
  public partial class Shape2d
  {
    #region field

    public List<Vector2> local_vertexe_list = new List<Vector2>();
    public Matrix4x4 matrix = Matrix4x4.identity;

    /// <summary>
    /// worldOffset不会跟随martix进行位移和旋转或缩放
    /// </summary>
    protected Vector2 world_offset = Vector2.zero;

    #endregion

    #region property



    public List<Vector2> vertexe_list
    {
      get
      {
        List<Vector2> result = new List<Vector2>();
        foreach (Vector2 v in local_vertexe_list)
        {
          result.Add(ToWorldSpace(v));
        }

        return result;
      }
    }

    public virtual Vector2 center
    {
      get { return ToWorldSpace(Vector2.zero); }
    }

    public Vector2 CenterOfAllPoints()
    {
      Vector2 sum = Vector2.zero;
      foreach (var v in vertexe_list)
      {
        sum += v;
      }

      return sum / vertexe_list.Count;
    }

    public List<Line> line_list
    {
      get
      {
        List<Line> result = new List<Line>();
        for (int i = 0; i < vertexe_list.Count - 1; i++)
          result.Add(new Line(vertexe_list[i], vertexe_list[i + 1]));
        if (vertexe_list.Count > 2)
          result.Add(new Line(vertexe_list[vertexe_list.Count - 1], vertexe_list[0]));
        return result;
      }
    }


    #endregion



    protected Shape2d(params Vector2[] local_vertexes)
    {
      local_vertexe_list.AddRange(local_vertexes);

    }

    #region public method

    public virtual bool IsIntersect(Rectangle rectangle2)
    {
      return false;
    }

    public virtual bool IsIntersect(Circle circle)
    {
      return false;
    }

    public override bool Equals(object obj)
    {
      Shape2d other = obj as Shape2d;
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
      List<Vector2> list = new List<Vector2>(vertexe_list);
      list.Add(world_offset);
      return ObjectUtil.GetHashCode(list.ToArray());
    }

    public virtual List<KeyValuePair<Vector2, Vector2>> GetDrawLineList()
    {
      List<KeyValuePair<Vector2, Vector2>> result = new List<KeyValuePair<Vector2, Vector2>>();
      for (int i = 0; i < vertexe_list.Count - 1; i++)
        result.Add(new KeyValuePair<Vector2, Vector2>(vertexe_list[i], vertexe_list[i + 1]));
      result.Add(new KeyValuePair<Vector2, Vector2>(vertexe_list[vertexe_list.Count - 1], vertexe_list[0]));

      return result;
    }


    public Shape2d AddWorldOffset(Vector2 add_world_offset)
    {
      this.world_offset += add_world_offset;
      return this;
    }

    public T AddWorldOffset<T>(Vector2 add_world_offset) where T : Shape2d
    {
      return (T)AddWorldOffset(add_world_offset);
    }

    public Shape2d MultiplyMatrix(Matrix4x4 martix)
    {
      this.matrix *= martix;
      return this;
    }

    public T MultiplyMatrix<T>(Matrix4x4 martix) where T : Shape2d
    {
      return (T)MultiplyMatrix(martix);
    }


    public Shape2d PreMultiplyMatrix(Matrix4x4 martix)
    {
      this.matrix = martix * this.matrix;
      return this;
    }

    public T PreMultiplyMatrix<T>(Matrix4x4 martix) where T : Shape2d
    {
      return (T)PreMultiplyMatrix(martix);
    }


    public void Reverse()
    {
      this.local_vertexe_list.Reverse();
    }


    public Vector2 ToLocalSpace(Vector2 p)
    {
      return matrix.inverse.MultiplyPoint3x4(p - world_offset);
    }



    public Vector2 ToWorldSpace(Vector2 p)
    {
      return matrix.MultiplyPoint3x4(p).ToVector2() + world_offset;
    }


    public Vector2? GetNext(Vector2 cur)
    {
      int cur_index = this.vertexe_list.IndexOf(cur);
      if (cur_index == -1)
        return null;
      return cur_index == vertexe_list.Count - 1 ? vertexe_list[0] : vertexe_list[cur_index + 1];
    }

    public Vector2? GetPre(Vector2 cur)
    {
      int cur_index = this.vertexe_list.IndexOf(cur);
      if (cur_index == -1)
        return null;
      return cur_index == 0 ? vertexe_list[vertexe_list.Count - 1] : vertexe_list[cur_index - 1];
    }

    #endregion






  }

}


