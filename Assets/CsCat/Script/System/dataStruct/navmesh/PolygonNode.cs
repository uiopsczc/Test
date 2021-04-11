using UnityEngine;

namespace CsCat
{
  public class PolygonNode
  {
    #region field

    /// <summary>
    /// 坐标点
    /// </summary>
    public Vector2 vertex;

    /// <summary>
    /// 是否是交点
    /// </summary>
    public bool is_intersect_point;

    /// <summary>
    /// 是否已处理过
    /// </summary>
    public bool is_processed = false;

    /// <summary>
    /// 进点--false； 出点--true
    /// </summary>
    public bool is_out_point = false;

    /// <summary>
    /// 交点的双向引用
    /// </summary>
    public PolygonNode other;

    /// <summary>
    /// 点是否在主多边形中
    /// </summary>
    public bool is_subject;

    /// <summary>
    /// 多边形的下一个点
    /// </summary>
    public PolygonNode next;

    /// <summary>
    /// 用于合并时，表示这个顶点是否处理过
    /// </summary>
    public bool is_checked = false;

    #endregion

    #region ctor

    public PolygonNode(Vector2 point, bool is_intersect_point, bool is_subject)
    {
      this.vertex = point;
      this.is_intersect_point = is_intersect_point;
      this.is_subject = is_subject;
    }

    #endregion




    #region override method

    public override string ToString()
    {
      return string.Format("{0}/n交点：{1}/n出点：{2}/n主：{3}/n处理：{4}", vertex, is_intersect_point, is_out_point, is_subject,
        is_processed);
    }

    #endregion



  }
}