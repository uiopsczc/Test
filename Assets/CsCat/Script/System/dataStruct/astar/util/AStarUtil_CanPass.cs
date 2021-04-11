using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  //坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
  public static partial class AStarUtil
  {
    //检测某个点是否可通过
    public static bool CanPass(int[][] grids, int x, int y, int[] can_pass_obstacle_types, int[] can_pass_terrain_types)
    {
      int grid_type = grids[x][y];
      int grid_obstacle_type = GetObstacleType(grid_type);
      if (grid_obstacle_type != 0 && can_pass_obstacle_types[grid_obstacle_type] == 0) // 障碍
        return false;
      int grid_terrain_type = GetTerrainType(grid_type);
      if (grid_terrain_type != 0 && can_pass_terrain_types[grid_terrain_type] == 0) // 地形
        return false;
      return true;
    }

    //检测某个点是否可通过
    // can_out 是否允许在场景外
    public static bool CanPass(AStarMapPath astarMapPath, int x, int y, int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types, bool can_out = false)
    {
      if (!IsInRange(astarMapPath.GetFinalGrids(), x, y))
        return can_out;
      int grid_type = astarMapPath.GetFinalGrids()[x][y]; // 固有地形+障碍
      if (!IsValidObstacleType(grid_type)) // 填充区域
        return can_out;
      if (!CanPass(astarMapPath.GetFinalGrids(), x, y, can_pass_obstacle_types, can_pass_terrain_types))
        return false;
      return true;
    }

    //检测轨迹是否可通过
    // can_out 是否允许在场景外
    public static bool CanPass(AStarMapPath astarMapPath, List<Vector2Int> track_list, int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types, bool can_out = false)
    {
      if (track_list.Count == 0)
        return true;
      Vector2Int lp = track_list[0];
      if (track_list.Count == 1)
        return CanPass(astarMapPath, lp.x, lp.y, can_pass_obstacle_types, can_pass_terrain_types, can_out);
      for (int i = 1; i < track_list.Count; i++)
      {
        Vector2Int p = track_list[i];
        if (!CanPass(astarMapPath, p.x, p.y, can_pass_obstacle_types, can_pass_terrain_types, can_out))
          return false;

        DirectionInfo directionInfo = DirectionConst.GetDirectionInfo(p.x - lp.x, p.y - lp.y);
//      directionInfo = DirectionConst.GetDirectionInfo(0, 0);
        if (directionInfo == DirectionConst.GetDirectionInfo("left_top")) // 左上角
        {
          if (!CanPass(astarMapPath, p.x + 1, p.y, can_pass_obstacle_types, can_pass_terrain_types, can_out))
            return false;
          if (!CanPass(astarMapPath, p.x, p.y - 1, can_pass_obstacle_types, can_pass_terrain_types, can_out))
            return false;
        }
        else if (directionInfo == DirectionConst.GetDirectionInfo("right_top")) // 右上角
        {
          if (!CanPass(astarMapPath, p.x - 1, p.y, can_pass_obstacle_types, can_pass_terrain_types, can_out))
            return false;
          if (!CanPass(astarMapPath, p.x, p.y - 1, can_pass_obstacle_types, can_pass_terrain_types, can_out))
            return false;
        }
        else if (directionInfo == DirectionConst.GetDirectionInfo("right_bottom")) // 右下角
        {
          if (!CanPass(astarMapPath, p.x - 1, p.y, can_pass_obstacle_types, can_pass_terrain_types, can_out))
            return false;
          if (!CanPass(astarMapPath, p.x, p.y + 1, can_pass_obstacle_types, can_pass_terrain_types, can_out))
            return false;
        }
        else if (directionInfo == DirectionConst.GetDirectionInfo("left_bottom")) // 左下角
        {
          if (!CanPass(astarMapPath, p.x + 1, p.y, can_pass_obstacle_types, can_pass_terrain_types, can_out))
            return false;
          if (!CanPass(astarMapPath, p.x, p.y + 1, can_pass_obstacle_types, can_pass_terrain_types, can_out))
            return false;
        }

        lp = p;
      }

      return true;
    }

    //检测两点间直线是否可通过
    public static bool CanLinePass(AStarMapPath astarMapPath, Vector2Int point_a, Vector2Int point_b,
      int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types, bool can_out = false)
    {
      if (!can_out && (!IsInRange(astarMapPath.GetFinalGrids(), point_a) ||
                       !IsInRange(astarMapPath.GetFinalGrids(), point_b)))
        return false;
      var line_point_list = GetLinePointList(point_a, point_b);
      if (!CanPass(astarMapPath, line_point_list, can_pass_obstacle_types, can_pass_terrain_types, can_out))
        return false;
      return true;
    }




  }
}