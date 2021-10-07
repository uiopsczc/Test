using UnityEngine;
using System;

namespace CsCat
{
  [Serializable]
  public partial class Polygon3d : Shape3d
  {




    #region ctor

    /// <summary>
    /// 默认顺时针方向,不需要加起始点，最后程序会自动加起始点
    /// </summary>
    /// <param name="local_vertexes"></param>
    public Polygon3d(params Vector3[] local_vertexes) : base(local_vertexes)
    {
    }



    #endregion

    #region operator

    public static Polygon3d operator +(Polygon3d polygon, Vector3 vector)
    {
      Polygon3d clone = CloneUtil.CloneDeep(polygon);
      clone.AddWorldOffset(vector);
      return clone;
    }

    public static Polygon3d operator -(Polygon3d polygon, Vector3 vector)
    {
      Polygon3d clone = CloneUtil.CloneDeep(polygon);
      clone.AddWorldOffset(-vector);
      return clone;
    }

    #endregion



  }
}
