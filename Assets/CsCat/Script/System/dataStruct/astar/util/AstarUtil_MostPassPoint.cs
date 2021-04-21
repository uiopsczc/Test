using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  //坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
  public static partial class AStarUtil
  {
    //获得轨迹中可通过的最远点
    public static Vector2Int GetMostPassPoint(AStarMapPath astarMapPath, List<Vector2Int> track_list,
      int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types, bool can_out = false)
    {
      Vector2Int lp = track_list[0];
      Vector2Int tp = lp;
      for (int i = 1; i < track_list.Count; i++)
      {
        Vector2Int p = track_list[i];
        if (!CanPass(astarMapPath, p.x, p.y, can_pass_obstacle_types, can_pass_terrain_types, can_out))
          break;

        DirectionInfo directionInfo = DirectionConst.GetDirectionInfo(p.x - lp.x, p.y - lp.y);
        //      directionInfo = DirectionConst.GetDirectionInfo(0, 0); // 不再判断临边障碍 2012-10-29
        //      LogCat.log(directionInfo.name);
        if (directionInfo == DirectionConst.GetDirectionInfo("left_top")) // 左上角
        {

          if (!CanPass(astarMapPath, p.x + 1, p.y, can_pass_obstacle_types, can_pass_terrain_types, can_out))
            break;
          if (!CanPass(astarMapPath, p.x, p.y - 1, can_pass_obstacle_types, can_pass_terrain_types, can_out))
            break;
        }
        else if (directionInfo == DirectionConst.GetDirectionInfo("right_top")) // 右上角
        {
          if (!CanPass(astarMapPath, p.x - 1, p.y, can_pass_obstacle_types, can_pass_terrain_types, can_out))
            break;
          if (!CanPass(astarMapPath, p.x, p.y - 1, can_pass_obstacle_types, can_pass_terrain_types, can_out))
            break;
        }
        else if (directionInfo == DirectionConst.GetDirectionInfo("right_bottom")) // 右下角
        {
          if (!CanPass(astarMapPath, p.x - 1, p.y, can_pass_obstacle_types, can_pass_terrain_types, can_out))
            break;
          if (!CanPass(astarMapPath, p.x, p.y + 1, can_pass_obstacle_types, can_pass_terrain_types, can_out))
            break;
        }
        else if (directionInfo == DirectionConst.GetDirectionInfo("left_bottom")) // 左下角
        {
          if (!CanPass(astarMapPath, p.x + 1, p.y, can_pass_obstacle_types, can_pass_terrain_types, can_out))
            break;
          if (!CanPass(astarMapPath, p.x, p.y + 1, can_pass_obstacle_types, can_pass_terrain_types, can_out))
            break;
        }

        lp = p;
        tp = lp;
      }

      return tp;
    }

    //获得两点间可通过的最远点
    // can_out  是否允许通过场景外
    public static Vector2Int GetMostLinePassPoint(AStarMapPath astarMapPath, Vector2Int lp, Vector2Int tp,
      int[] can_pass_obstacle_types, int[] can_pass_terrain_types, bool can_out = false)
    {
      if (!can_out && !IsInRange(astarMapPath.GetFinalGrids(), lp))
        return lp;
      List<Vector2Int> point_list = GetLinePointList(lp, tp);
      return GetMostPassPoint(astarMapPath, point_list, can_pass_obstacle_types, can_pass_terrain_types, can_out);
    }

  }
}