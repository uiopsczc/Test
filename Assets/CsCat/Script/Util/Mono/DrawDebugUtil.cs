using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
  public class DrawDebugUtil
  {


    public static void Draw(List<Vector2> point_list, float h, Color color)
    {
      Vector3 t_start = new Vector3(point_list[0].x, h, point_list[0].y);
      for (int i = 1; i < point_list.Count; i++)
      {
        Vector3 t_end = new Vector3(point_list[i].x, h, point_list[i].y);
        Debug.DrawLine(t_start, t_end, color);
        t_start = t_end;
      }
    }

    public static void Draw(List<Vector3> point_list, Color color)
    {
      Vector3 t_start = point_list[0];
      for (int i = 1; i < point_list.Count; i++)
      {
        Vector3 t_end = point_list[i];
        Debug.DrawLine(t_start, t_end, color);
        t_start = t_end;
      }
    }

    public static void DrawNumber(string s, Vector3 center, Color color, float size = 1)
    {
      char[] cs = s.ToCharArray();
      for (int i = 0; i < cs.Length; i++)
      {
        char c = cs[i];

        Vector3 center_c = Vector3.zero;
        int offset = i - cs.Length / 2;
        if (cs.Length % 2 == 1)
        {
          center_c = new Vector3((center.x + offset * size), center.y, center.z);
        }
        else
        {
          center_c = new Vector3((center.x + (offset + 0.5f) * size), center.y, center.z);
        }

        DrawNumber(c, center_c, color, size);
      }
    }


    public static void DrawNumber(char c, Vector3 center, Color color, float size = 1)
    {
      NumberSquare square = new NumberSquare(center, size / 2);
      List<Vector3> points = square.GetPointList(c);
      Draw(points, color);

    }

    public static void DrawCellIndex(List<Cell> cell_list, float h, Color color, float number_size = 1f)
    {
      foreach (Cell cell in cell_list)
      {
        //cellIndex
        DrawDebugUtil.DrawNumber(cell.index + "", new Vector3(cell.center.x, h, cell.center.y), color, number_size);
      }
    }

    public static void DrawTrianglesSidesIndex(List<Triangle> triangle_list, float h, Color color,
      float number_size = 1f)
    {
      foreach (Triangle triangle in triangle_list)
      {
        foreach (Line side in triangle.line_list)
        {
          Vector2 normal = side.GetNormal();
          Vector2 p = side.center;
          p = p - normal * 0.4f; //0.4：离法线方向*0.4的地方写index数字
          DrawDebugUtil.DrawNumber(triangle.line_list.IndexOf(side) + "", new Vector3(p.x, h, p.y), color, number_size);
        }
      }
    }
  }
}
