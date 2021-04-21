using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  //坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
  public class AStarMapPath
  {
    private int[][] final_grids;
    public int[][] grids;
    public int[][] project_grids; //别的地方投影到该mapPath的grids,调用UpdateFinalGrids进行混合
    public int grid_offset_x;
    public int grid_offset_y;

    public AStarMapPath(string json_conent)
    {
      Hashtable dict = MiniJson.JsonDecode(json_conent).To<Hashtable>();
      int min_grid_x = dict["min_grid_x"].To<int>();
      int min_grid_y = dict["min_grid_y"].To<int>();
      int max_grid_x = dict["max_grid_x"].To<int>();
      int max_grid_y = dict["max_grid_y"].To<int>();
      Dictionary<Vector2Int, int> data_dict = new Dictionary<Vector2Int, int>();
      Hashtable _data_dict = dict["data_dict"].To<Hashtable>();
      foreach (var _key in _data_dict.Keys)
      {
        Vector2 v = _key.To<string>().ToVector2();
        Vector2Int key = new Vector2Int((int)v.x, (int)v.y);
        int value = _data_dict[_key].To<int>();
        data_dict[key] = value;
      }

      int[][] grids = null;
      grids = grids
        .InitArrays(max_grid_y - min_grid_y + 1, max_grid_x - min_grid_x + 1,
          AStarMonoBehaviourConst.Default_Data_Value).ToLeftBottomBaseArrays();
      grid_offset_x = min_grid_x; //用于astarBehaviour的非0的偏移
      grid_offset_y = min_grid_y; //用于astarBehaviour的非0的偏移

      foreach (var key in data_dict.Keys)
        grids[key.x - min_grid_x][key.y - min_grid_y] = data_dict[key];

      Init(grids);
    }

    public AStarMapPath(int[][] grids)
    {
      Init(grids);
    }

    void Init(int[][] grids)
    {
      this.grids = grids;
      this.project_grids = grids.InitArrays(Height(), Width());
    }

    public int Width()
    {
      return grids == null ? 0 : grids[0].Length;
    }

    public int Height()
    {
      return grids == null ? 0 : grids.Length;
    }

    public int[][] GetFinalGrids()
    {
      if (final_grids == null)
        UpdateFinalGrids();
      return final_grids;
    }

    public void UpdateFinalGrids()
    {
      this.final_grids = grids.InitArrays(Height(), Width());
      for (int i = 0; i < grids.Length; i++)
      {
        for (int j = 0; j < grids[0].Length; j++)
        {
          int grid_type = grids[i][j];
          int project_grid_type = project_grids[i][j];
          if (project_grid_type == 0) //没有project_grid_type，则用grid_type
            final_grids[i][j] = grid_type;
          else
          {
            int field = AStarUtil.GetField(grid_type); //用grid_type的field
            int terrain_type = AStarUtil.GetTerrainType(project_grid_type) != 0
              ? AStarUtil.GetTerrainType(project_grid_type)
              : AStarUtil.GetTerrainType(grid_type); //覆盖关系
            int obstacle_type = AStarUtil.GetObstacleType(project_grid_type) != 0
              ? AStarUtil.GetObstacleType(project_grid_type)
              : AStarUtil.GetObstacleType(grid_type); //覆盖关系

            final_grids[i][j] = AStarUtil.ToGridType(field, terrain_type, obstacle_type);
          }
        }
      }
    }

    //先对角线查找，再直角查找
    public List<Vector2Int> DirectFindPath(Vector2Int point_a,
      Vector2Int point_b, int[] can_pass_obstacle_types, int[] can_pass_terrain_types)
    {
      return AStarMapPathUtil.DirectFindPath(this, point_a, point_b, can_pass_obstacle_types,
        can_pass_terrain_types);
    }

    //直角寻路(先横向再纵向寻路)
    public List<Vector2Int> BorderFindPath(Vector2Int point_a, Vector2Int point_b, int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types)
    {
      return AStarMapPathUtil.BorderFindPath(this, point_a, point_b, can_pass_obstacle_types,
        can_pass_terrain_types);
    }


    //对角线寻路
    public List<Vector2Int> DiagonallyFindPath(Vector2Int point_a, Vector2Int point_b, int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types)
    {
      return AStarMapPathUtil.DiagonallyFindPath(this, point_a, point_b, can_pass_obstacle_types,
        can_pass_terrain_types);
    }


    //获取P点四周为+-out_count的可以通过的点列表
    public List<Vector2Int> GetAroundFreePointList(Vector2Int base_point, int out_count,
      int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types)
    {
      return AStarUtil.GetAroundFreePointList(this, base_point, out_count, can_pass_obstacle_types,
        can_pass_obstacle_types);
    }

    //获取P点四周为+-out_count（包含边界）以内的可以通过的点
    public Vector2Int? FindAroundFreePoint(Vector2Int base_point, int out_count, List<Vector2Int> except_point_list,
      int[] can_pass_obstacle_types, int[] can_pass_terrain_types, RandomManager randomManager = null)
    {
      return AStarUtil.FindAroundFreePoint(this, base_point, out_count, except_point_list, can_pass_obstacle_types,
        can_pass_obstacle_types, randomManager);
    }

    //获取P点四周为+-out_count的可以通过的点
    public Vector2Int? FindAroundFreePoint(Vector2Int base_point, int out_count, int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types, RandomManager randomManager = null)
    {
      return AStarUtil.FindAroundFreePoint(this, base_point, out_count, can_pass_obstacle_types,
        can_pass_obstacle_types, randomManager);
    }

    //获得轨迹中可通过的最远点
    public Vector2Int GetMostPassPoint(List<Vector2Int> track_list,
      int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types, bool can_out = false)
    {
      return AStarUtil.GetMostPassPoint(this, track_list,
        can_pass_obstacle_types,
        can_pass_terrain_types, can_out);
    }

    //获得两点间可通过的最远点
    // can_out  是否允许通过场景外
    public Vector2Int GetMostLinePassPoint(Vector2Int lp, Vector2Int tp,
      int[] can_pass_obstacle_types, int[] can_pass_terrain_types, bool can_out = false)
    {
      return AStarUtil.GetMostLinePassPoint(this, lp, tp, can_pass_obstacle_types, can_pass_terrain_types, can_out);
    }

    //获取离a,b最近的点
    public Vector2Int GetNearestPoint(Vector2Int point_a, Vector2Int point_b,
      int[] can_pass_obstacle_types, int[] can_pass_terrain_types)
    {
      return AStarUtil.GetNearestPoint(this, point_a, point_b, can_pass_obstacle_types, can_pass_terrain_types);
    }

    public Vector2Int? GetRandomMovePoint(Vector2Int base_point, Vector2Int goal_point,
      int max_radius_between_target_point_and_goal_point, int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types, RandomManager randomManager = null)
    {
      return AStarUtil.GetRandomMovePoint(this, base_point, goal_point, max_radius_between_target_point_and_goal_point,
        can_pass_obstacle_types, can_pass_terrain_types, randomManager);
    }


    //获取range范围内的可以通过的格子列表
    public List<Vector2Int> GetRangeFreePointList(int x1, int y1, int x2, int y2,
      List<Vector2Int> except_point_list,
      int[] can_pass_obstacle_types, int[] can_pass_terrain_types)
    {
      return AStarUtil.GetRangeFreePointList(this, x1, y1, x2,
        y2, except_point_list, can_pass_obstacle_types, can_pass_terrain_types);
    }


    //获取range范围内的可以通过的格子
    public Vector2Int? FindRangeFreePoint(int x1, int y1, int x2, int y2,
      int[] can_pass_obstacle_types, int[] can_pass_terrain_types, RandomManager randomManager = null)
    {
      return FindRangeFreePoint(x1, y1, x2, y2, null,
        can_pass_obstacle_types, can_pass_terrain_types, randomManager);
    }


    //获取range范围内的可以通过的格子
    public Vector2Int? FindRangeFreePoint(int x1, int y1, int x2, int y2,
      List<Vector2Int> except_point_list,
      int[] can_pass_obstacle_types, int[] can_pass_terrain_types, RandomManager randomManager = null)
    {
      return AStarUtil.FindRangeFreePoint(this, x1, y1, x2, y2, except_point_list, can_pass_obstacle_types,
        can_pass_terrain_types, randomManager);
    }

    //检测某个点是否可通过
    // can_out 是否允许在场景外
    public bool CanPass(int x, int y, int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types, bool can_out = false)
    {
      return AStarUtil.CanPass(this, x, y, can_pass_obstacle_types, can_pass_terrain_types, can_out);
    }

    //检测轨迹是否可通过
    // can_out 是否允许在场景外
    public bool CanPass(List<Vector2Int> track_list, int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types, bool can_out = false)
    {
      return AStarUtil.CanPass(this, track_list, can_pass_obstacle_types, can_pass_terrain_types, can_out);
    }

    //检测两点间直线是否可通过
    public bool CanLinePass(Vector2Int point_a, Vector2Int point_b, int[] can_pass_obstacle_types,
      int[] can_pass_terrain_types, bool can_out = false)
    {
      return AStarUtil.CanLinePass(this, point_a, point_b, can_pass_obstacle_types, can_pass_terrain_types, can_out);
    }

    //是否有效地图坐标（不含填充区域）
    public bool IsValidPoint(int x, int y)
    {
      if (!AStarUtil.IsInRange(grids, x, y))
        return false;
      return AStarUtil.IsValidObstacleType(grids[x][y]);
    }
  }
}

