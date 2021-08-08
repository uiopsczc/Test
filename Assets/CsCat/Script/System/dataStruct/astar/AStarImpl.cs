using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CsCat
{
  //https://blog.csdn.net/wf471859778/article/details/103608623
  //坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
  // 里面的point是没有offset的
  public class AStarImpl : AStar
  {
    protected AStarHType astarHType;
    protected AStarMapPath astarMapPath; // 地图数组
    protected int[] can_pass_obstacle_types; // 可通过障碍表
    protected int[] can_pass_terrain_types; // 可通过地形表

    public AStarImpl(AStarMapPath astarMapPath, AStarHType astarHType, int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types)
    {
      this.astarMapPath = astarMapPath;
      SetAStarHType(astarHType);
      SetCanPassType(can_pass_obstacle_types, can_pass_terrain_types);
      SetRange(0, 0, astarMapPath.Height() - 1, astarMapPath.Width() - 1);
    }

    public void SetCanPassType(int[] can_pass_obstacle_types, int[] can_pass_terrain_types)
    {
      this.can_pass_obstacle_types = can_pass_obstacle_types;
      this.can_pass_terrain_types = can_pass_terrain_types;
    }

    protected override bool CanPass(int x_without_offset, int y_without_offset)
    {
      if (!AStarUtil.CanPass(astarMapPath.GetFinalGrids(), x_without_offset , y_without_offset, can_pass_obstacle_types, can_pass_terrain_types))
        return false;
      return true;
    }

    int GetHeight()
    {
      return astarMapPath.Height();
    }

    int GetWidth()
    {
      return astarMapPath.Width();
    }

    public void SetAStarHType(AStarHType astarHType)
    {
      this.astarHType = astarHType;
    }

    //返回的是不带offset的point
    public Vector2Int GetRandomCanPassPoint(RandomManager randomManager = null)
    {
      randomManager = randomManager ?? Client.instance.randomManager;
      var can_pass_point_list = new List<Vector2Int>();
      for (int x = 0; x < astarMapPath.Width(); x++)
        for (int y = 0; y < astarMapPath.Height(); y++)
        {
          if (CanPass(x, y))
            can_pass_point_list.Add(new Vector2Int(x, y));
        }

      int random_index = randomManager.RandomInt(0, can_pass_point_list.Count);
      return can_pass_point_list[random_index];
    }

    protected override float GetG(Vector2Int p1, Vector2Int p2)
    {
      int dx = Math.Abs(p1.x - p2.x);
      int dy = Math.Abs(p1.y - p2.y);
      if (dx == 0 || dy == 0) //非斜线方向
        return astarMapPath.GetFinalGrids()[p2.x][p2.y];
      else //斜线方向
        return astarMapPath.GetFinalGrids()[p2.x][p2.y] * 1.414f;
    }


    protected override float GetH(Vector2Int n_point, Vector2Int goal_point)
    {
      switch (astarHType)
      {
        case AStarHType.Euclid_Distance:
          return (float)Math.Sqrt((n_point.x - goal_point.x) * (n_point.x - goal_point.x) + (n_point.y - goal_point.y) * (n_point.y - goal_point.y));
        case AStarHType.Euclid_SqureDistance:
          return (n_point.x - goal_point.x) * (n_point.x - goal_point.x) + (n_point.y - goal_point.y) * (n_point.y - goal_point.y);
        case AStarHType.Manhattan_Distance:
          return Math.Abs(n_point.x - goal_point.x) + Math.Abs(n_point.y - goal_point.y);
        case AStarHType.Diagonal_Distance:
          var h_diagonal = Math.Min(Math.Abs(n_point.x - goal_point.x), Math.Abs(n_point.y - goal_point.y));
          var h_straight = Math.Abs(n_point.x - goal_point.x) + Math.Abs(n_point.y - goal_point.y);
          return 1.414f * h_diagonal + (h_straight - 2 * h_diagonal);
      }

      return 0;
    }

    protected override void SetNeighborOffsetList()
    {
      switch (astarHType)
      {
        case AStarHType.Manhattan_Distance:
          neighbor_offset_list.Add(new Vector2Int(0, 1)); //上方邻居节点
          neighbor_offset_list.Add(new Vector2Int(1, 0)); //右侧邻居节点
          neighbor_offset_list.Add(new Vector2Int(0, -1)); //下方邻居节点
          neighbor_offset_list.Add(new Vector2Int(-1, 0)); //左侧邻居节点
          break;
        default:
          base.SetNeighborOffsetList();
          break;
      }
    }

  }
}
