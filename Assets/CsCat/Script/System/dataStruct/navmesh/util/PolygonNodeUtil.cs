using System.Collections.Generic;

namespace CsCat
{
    public class PolygonNodeUtil
    {
        /// <summary>
        ///   创建polygon的nodes，最后一个node的next指向null
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="isIntersectPoint"></param>
        /// <param name="isSubject"></param>
        /// <returns></returns>
        public static List<PolygonNode> CreateNodes(Polygon polygon, bool isIntersectPoint, bool isSubject)
        {
            var nodes = new List<PolygonNode>();
            PolygonNode node;
            for (var i = 0; i < polygon.vertexList.Count; i++)
            {
                node = new PolygonNode(polygon.vertexList[i], isIntersectPoint, isSubject);
                if (i > 0) nodes[i - 1].next = node;
                nodes.Add(node);
            }

            return nodes;
        }
    }
}