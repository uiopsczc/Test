using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class AStarUtilTest
  {

    public static Action Test_GetAroundFreePointList()
    {
      List<Vector2Int> list = AStarUtil.GetAroundFreePointList(new AStarMapPath(grids), new Vector2Int(2, 2), 2,
        AStarMapPathConst.Critter_Can_Pass_Obstacle_Types, AStarMapPathConst.User_Can_Pass_Terrain_Types);
      return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, list); };
    }

    public static Action Test_FindAroundFreePoint()
    {
      Vector2Int? point = AStarUtil.FindAroundFreePoint(new AStarMapPath(grids), new Vector2Int(2, 2), 2, null,
        AStarMapPathConst.Critter_Can_Pass_Obstacle_Types, AStarMapPathConst.User_Can_Pass_Terrain_Types);
      return () =>
      {
        if (point != null)
          AStarUtil.GUIShowPointList(0, 0, 9, 9, point.Value);
      };
    }
  }
}