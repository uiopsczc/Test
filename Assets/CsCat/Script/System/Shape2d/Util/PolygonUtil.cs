using System.Collections.Generic;

namespace CsCat
{
  public class PolygonUtil
  {
    public static void Systemize(List<Polygon> polygon_list)
    {
      for (int i = polygon_list.Count - 1; i >= 0; i--)
      {
        if (polygon_list[i].vertexe_list.Count < 3) //删除少于3个顶点的多边形
          polygon_list.RemoveAt(i);
        else
          polygon_list[i].Systemize();
      }
    }
  }





}

