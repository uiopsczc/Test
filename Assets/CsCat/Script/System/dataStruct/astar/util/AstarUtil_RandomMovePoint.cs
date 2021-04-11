using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  //坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
  public static partial class AStarUtil
  {
    public static Vector2Int? GetRandomMovePoint(AStarMapPath astarMapPath, Vector2Int base_point,
      Vector2Int goal_point, int max_radius_between_target_point_and_goal_point, int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types, RandomManager randomManager = null)
    {
      randomManager = randomManager ?? Client.instance.randomManager;
      int out_count = randomManager.RandomInt(AStarMapPathConst.Random_Move_Distance_Min,
        AStarMapPathConst.Random_Move_Distance_Max + 1);
      List<Vector2Int> list = GetAroundFreePointList(astarMapPath, base_point, out_count, can_pass_obstacle_types,
        can_pass_terrain_types);
      while (list.Count > 0)
      {
        int remove_index = randomManager.RandomInt(0, list.Count);
        Vector2Int target_point = list[remove_index];
        list.RemoveAt(remove_index);
        if (Vector2Int.Distance(goal_point, target_point) <= max_radius_between_target_point_and_goal_point)
          return target_point;
      }

      return null;
    }


  }
}