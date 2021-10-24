using System.Collections.Generic;

namespace CsCat
{
  public class PolygonNodeUtil
  {
    /// <summary>
    ///   创建polygon的nodes，最后一个node的next指向null
    /// </summary>
    /// <param name="polygon"></param>
    /// <param name="is_intersect_point"></param>
    /// <param name="is_subject"></param>
    /// <returns></returns>
    public static List<PolygonNode> CreateNodes(Polygon polygon, bool is_intersect_point, bool is_subject)
    {
      var nodes = new List<PolygonNode>();
      PolygonNode node;
      for (var i = 0; i < polygon.vertexList.Count; i++)
      {
        node = new PolygonNode(polygon.vertexList[i], is_intersect_point, is_subject);
        if (i > 0) nodes[i - 1].next = node;
        nodes.Add(node);
      }

      return nodes;
    }
  }
}