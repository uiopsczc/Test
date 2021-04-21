using System;
using UnityEngine;

namespace CsCat
{
  public partial class AStarUtilTest
  {
    public static void Test_IsValidObstacleType()
    {
      LogCat.log(AStarUtil.IsValidObstacleType(255));
    }

    public static void Test_ToGridType()
    {
      LogCat.log(AStarUtil.GetObstacleType(AStarUtil.ToGridType(1, 1, 2)));
    }

    public static void Test_GetObstacleType()
    {
      LogCat.log(AStarUtil.GetObstacleType(2));
    }

    public static void Test_GetTerrainType()
    {
      LogCat.log(AStarUtil.GetTerrainType(24));
    }

    public static void Test_GetField()
    {
      LogCat.log(AStarUtil.GetField((int)Math.Pow(2, 9)));
    }

    public static void Test_IsSameField()
    {
      LogCat.log(AStarUtil.IsSameField((int)Math.Pow(2, 9), (int)Math.Pow(2, 8)));
    }

    public static void Test_GetBlockPoint()
    {
      LogCat.log(AStarUtil.GetBlockPoint(new Vector2Int(4, 7)));
    }

    public static void Test_IsSameBlock()
    {
      LogCat.log(AStarUtil.IsSameBlock(new Vector2Int(2, 2), new Vector2Int(2, 3)));
    }

    public static void Test_IsNeighborBlock()
    {
      LogCat.log(AStarUtil.IsNeighborBlock(new Vector2Int(2, 2), new Vector2Int(2, 3)));
    }


  }
}