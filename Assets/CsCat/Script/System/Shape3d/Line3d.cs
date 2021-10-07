using System;
using UnityEngine;

namespace CsCat
{
  [Serializable]
  public partial class Line3d : Polygon3d
  {
    #region property


    public Vector3 point_A
    {
      get { return ToWorldSpace(local_vertex_list[0]); }
    }

    public Vector3 point_B
    {
      get { return ToWorldSpace(local_vertex_list[1]); }
    }

    #endregion

    #region ctor

    public Line3d(Vector3 start, Vector3 end) : base(start, end)
    {
    }

    #endregion

    #region static method

    public static Line3d operator +(Line3d line, Vector3 vector)
    {
      Line3d clone = CloneUtil.CloneDeep(line);
      clone.AddWorldOffset(vector);
      return clone;
    }

    public static Line3d operator -(Line3d line, Vector3 vector)
    {
      Line3d clone = CloneUtil.CloneDeep(line);
      clone.AddWorldOffset(-vector);
      return clone;
    }

    #endregion

    #region override method




    #region ToString

    public override string ToString()
    {
      return string.Format("A:{0} B:{1}", this.point_A, this.point_B);
    }

    #endregion

    #endregion





  }
}

