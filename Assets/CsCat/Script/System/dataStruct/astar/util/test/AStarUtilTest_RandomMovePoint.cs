using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class AStarUtilTest
  {
    public static Action Test_GetRandomMovePoint()
    {
      Vector2Int? point = AStarUtil.GetRandomMovePoint(new AStarMapPath(grids), new Vector2Int(0, 0),
        new Vector2Int(3, 3)
        , 2, AStarMapPathConst.Critter_Can_Pass_Obstacle_Types, AStarMapPathConst.User_Can_Pass_Terrain_Types);
      return () =>
      {
        if (point != null)
          AStarUtil.GUIShowPointList(0, 0, 9, 9, new List<Vector2Int>() {point.Value});
      };
    }
  }
}