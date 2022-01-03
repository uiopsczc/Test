using System.Collections.Generic;

namespace CsCat
{
	public class PolygonUtil
	{
		public static void Systemize(List<Polygon> polygonList)
		{
			for (int i = polygonList.Count - 1; i >= 0; i--)
			{
				if (polygonList[i].vertexList.Count < 3) //删除少于3个顶点的多边形
					polygonList.RemoveAt(i);
				else
					polygonList[i].Systemize();
			}
		}
	}
}