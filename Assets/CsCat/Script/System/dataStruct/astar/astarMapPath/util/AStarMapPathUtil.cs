using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  //坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
  public partial class AStarMapPathUtil
  {
    //先对角线查找，再直角查找
    public static List<Vector2Int> DirectFindPath(AStarMapPath astarMapPath, Vector2Int point_a, Vector2Int point_b,
      int[] can_pass_obstacle_types, int[] can_pass_terrain_types)
    {
      if (!AStarUtil.IsInRange(astarMapPath.GetFinalGrids(), point_a) ||
          !AStarUtil.IsInRange(astarMapPath.GetFinalGrids(), point_b))
        return null;
      List<Vector2Int> list = null;
      if (point_a.Equals(point_b)) // 同一点
      {
        list = new List<Vector2Int>();
        list.Add(point_a);
      }
      else if (point_a.x == point_b.x)
      {
        list = new List<Vector2Int>();
        list.Add(point_a);
        int dv = point_b.y > point_a.y ? 1 : -1;
        for (int y = point_a.y + dv; y * dv < point_b.y * dv; y += dv)
        {
          if (!AStarUtil.CanPass(astarMapPath, point_a.x, y, can_pass_obstacle_types, can_pass_terrain_types))
            return null;
          list.Add(new Vector2Int(point_a.x, y));
        }

        list.Add(point_b);
      }
      else if (point_a.y == point_b.y)
      {
        list = new List<Vector2Int>();
        list.Add(point_a);
        int dv = point_b.x > point_a.x ? 1 : -1;
        for (int x = point_a.x + dv; x * dv < point_b.x * dv; x += dv)
        {
          if (!AStarUtil.CanPass(astarMapPath, x, point_a.y, can_pass_obstacle_types, can_pass_terrain_types))
            return null;
          list.Add(new Vector2Int(x, point_a.y));
        }

        list.Add(point_b);
      }
      else
      {
        //先对角线查找，再直角查找
        list = DiagonallyFindPath(astarMapPath, point_a, point_b, can_pass_obstacle_types, can_pass_terrain_types);
        if (list == null)
        {
          list = DiagonallyFindPath(astarMapPath, point_b, point_a, can_pass_obstacle_types, can_pass_terrain_types);
          if (list == null)
          {
            list = BorderFindPath(astarMapPath, point_a, point_b, can_pass_obstacle_types, can_pass_terrain_types);
            if (list == null)
            {
              list = BorderFindPath(astarMapPath, point_b, point_a, can_pass_obstacle_types, can_pass_terrain_types);
              list?.Reverse();
            }
          }
          else
            list.Reverse();
        }
      }

      return list;
    }


    //直角寻路(先横向再纵向寻路)
    public static List<Vector2Int> BorderFindPath(AStarMapPath astarMapPath, Vector2Int point_a, Vector2Int point_b,
      int[] can_pass_obstacle_types, int[] can_pass_terrain_types)
    {
      if (!AStarUtil.IsInRange(astarMapPath.GetFinalGrids(), point_a) ||
          !AStarUtil.IsInRange(astarMapPath.GetFinalGrids(), point_b))
        return null;
      List<Vector2Int> list = new List<Vector2Int>();
      list.Add(point_a);
      int dv = point_b.x > point_a.x ? 1 : -1;
      for (int x = point_a.x + dv; x * dv <= point_b.x * dv; x += dv)
      {
        //      LogCat.log(x, point_a.y);
        if (!AStarUtil.CanPass(astarMapPath, x, point_a.y, can_pass_obstacle_types, can_pass_terrain_types))
          return null;
        list.Add(new Vector2Int(x, point_a.y));
      }

      dv = point_b.y > point_a.y ? 1 : -1;
      for (int y = point_a.y + dv; y * dv < point_b.y * dv; y += dv)
      {
        if (!AStarUtil.CanPass(astarMapPath, point_b.x, y, can_pass_obstacle_types, can_pass_terrain_types))
          return null;
        list.Add(new Vector2Int(point_b.x, y));
      }

      list.Add(point_b);
      return list;
    }

    //对角线寻路
    public static List<Vector2Int> DiagonallyFindPath(AStarMapPath astarMapPath, Vector2Int point_a, Vector2Int point_b,
      int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types)
    {
      if (!AStarUtil.IsInRange(astarMapPath.GetFinalGrids(), point_a) ||
          !AStarUtil.IsInRange(astarMapPath.GetFinalGrids(), point_b))
        return null;
      List<Vector2Int> list = new List<Vector2Int>();

      int dx = point_b.x - point_a.x;
      int dy = point_b.y - point_a.y;
      if (Math.Abs(dx) > Math.Abs(dy))
      {
        int x1;
        if (dx > 0)
          x1 = point_a.x + Math.Abs(dy);
        else
          x1 = point_a.x - Math.Abs(dy);
        Vector2Int p = new Vector2Int(x1, point_b.y);
        if (!AStarUtil.CanPass(astarMapPath, p.x, p.y, can_pass_obstacle_types, can_pass_terrain_types))
          return null;
        List<Vector2Int> list1 = AStarUtil.GetLinePointList(point_a, p);
        if (!AStarUtil.CanPass(astarMapPath, list1, can_pass_obstacle_types, can_pass_terrain_types))
          return null;
        List<Vector2Int> list2 = AStarUtil.GetLinePointList(p, point_b);
        if (!AStarUtil.CanPass(astarMapPath, list2, can_pass_obstacle_types, can_pass_terrain_types))
          return null;
        list.AddRange(list1);
        list.RemoveLast(); //删掉p
        list.AddRange(list2);
      }
      else
      {
        int y1;
        if (dy > 0)
          y1 = point_a.y + Math.Abs(dx);
        else
          y1 = point_a.y - Math.Abs(dx);
        Vector2Int p = new Vector2Int(point_b.x, y1);
        if (!AStarUtil.CanPass(astarMapPath, p.x, p.y, can_pass_obstacle_types, can_pass_terrain_types))
          return null;
        List<Vector2Int> list1 = AStarUtil.GetLinePointList(point_a, p);
        if (!AStarUtil.CanPass(astarMapPath, list1, can_pass_obstacle_types, can_pass_terrain_types))
          return null;
        List<Vector2Int> list2 = AStarUtil.GetLinePointList(p, point_b);
        if (!AStarUtil.CanPass(astarMapPath, list2, can_pass_obstacle_types, can_pass_terrain_types))
          return null;
        list.AddRange(list1);
        list.RemoveLast(); //删掉p
        list.AddRange(list2);
      }

      return list;
    }

  }
}