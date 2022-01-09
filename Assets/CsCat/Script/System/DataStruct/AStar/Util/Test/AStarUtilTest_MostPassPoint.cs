using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class AStarUtilTest
	{

		public static Action Test_GetMostPassPoint()
		{
			Vector2Int point = AStarUtil.GetMostPassPoint(new AStarMapPath(grids), new List<Vector2Int>()
	  {
		new Vector2Int(0, 0),
		new Vector2Int(1, 1),
		new Vector2Int(2, 2),
		new Vector2Int(3, 3),
		new Vector2Int(4, 4),
	  }, AStarMapPathConst.Critter_Can_Pass_Obstacle_Types, AStarMapPathConst.User_Can_Pass_Terrain_Types);
			return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, point); };
		}

		public static Action Test_GetMostLinePassPoint()
		{
			Vector2Int point = AStarUtil.GetMostLinePassPoint(new AStarMapPath(grids), new Vector2Int(0, 0),
			  new Vector2Int(4, 4), AStarMapPathConst.Critter_Can_Pass_Obstacle_Types,
			  AStarMapPathConst.User_Can_Pass_Terrain_Types);
			return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, point); };
		}
	}
}