using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class AStarMapPathUtilTest
  {
    public static int[][] __grids = new int[][]
    {
      new int[] {1, 1, 3, 1, 1, 1},
      new int[] {3, 1, 1, 1, 1, 1},
      new int[] {3, 1, 1, 1, 1, 1},
      new int[] {1, 1, 1, 1, 1, 1},
      new int[] {1, 1, 1, 1, 1, 1},
    };

    public static int[][] _grids;

    //转为左下为原点的坐标系，x增加是向右，y增加是向上（与unity的坐标系一致）
    public static int[][] grids
    {
      get
      {
        if (_grids == null)
          _grids = __grids.ToLeftBottomBaseArrays();
        return _grids;
      }
    }

    public static Action Test_BorderFindPath()
    {
      List<Vector2Int> list = AStarMapPathUtil.BorderFindPath(new AStarMapPath(grids), new Vector2Int(1, 1),
        new Vector2Int(4, 4), AStarMapPathConst.Critter_Can_Pass_Obstacle_Types,
        AStarMapPathConst.User_Can_Pass_Terrain_Types);
      return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, list); };
    }

    public static Action Test_DiagonallyFindPath()
    {
      List<Vector2Int> list = AStarMapPathUtil.DiagonallyFindPath(new AStarMapPath(grids), new Vector2Int(1, 1),
        new Vector2Int(4, 4), AStarMapPathConst.Critter_Can_Pass_Obstacle_Types,
        AStarMapPathConst.User_Can_Pass_Terrain_Types);
      return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, list); };
    }

    public static Action Test_DirectFindPath()
    {
      List<Vector2Int> list = AStarMapPathUtil.DirectFindPath(new AStarMapPath(grids), new Vector2Int(1, 1),
        new Vector2Int(4, 4), AStarMapPathConst.Critter_Can_Pass_Obstacle_Types,
        AStarMapPathConst.User_Can_Pass_Terrain_Types);
      return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, list); };
    }

    public static void Test2()
    {
      int[][] grids = new AStarMapPath(StdioUtil.ReadTextFile("E:/WorkSpace/Unity/Test/Assets/tile/tileSet/fff.txt"))
        .grids;
      LogCat.log(grids);
    }
  }
}