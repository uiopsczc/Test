using UnityEngine;

namespace CsCat
{
  public class ClassifyResult
  {
    #region ctor

    #endregion


    #region override method

    public override string ToString()
    {
      return result + " " + cellIndex;
    }

    #endregion

    #region field

    /// <summary>
    ///   直线与cell（三角形）的关系
    /// </summary>
    public PathResult result = PathResult.NO_RELATIONSHIP;

    /// <summary>
    ///   相交边的索引
    /// </summary>
    public int side = 0;

    /// <summary>
    ///   下一个邻接cell的索引
    /// </summary>
    public int cellIndex = -1;

    /// <summary>
    ///   交点
    /// </summary>
    public Vector2 intersection = new Vector2();

    #endregion
  }
}