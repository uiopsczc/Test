using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class AStarUtilTest
	{

		public static Action Test_GetRangeFreePointList()
		{
			List<Vector2Int> pointList = AStarUtil.GetRangeFreePointList(new AStarMapPath(grids), 0, 0, 4, 4, null,
			  AStarMapPathConst.Critter_Can_Pass_Obstacle_Types, AStarMapPathConst.User_Can_Pass_Terrain_Types);
			return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, pointList); };
		}

		public static Action Test_FindRangeFreePoint()
		{
			Vector2Int? point = AStarUtil.FindRangeFreePoint(new AStarMapPath(grids), 0, 0, 4, 4, null,
			  AStarMapPathConst.Critter_Can_Pass_Obstacle_Types, AStarMapPathConst.User_Can_Pass_Terrain_Types);
			return () =>
			{
				if (point != null)
					AStarUtil.GUIShowPointList(0, 0, 9, 9, new List<Vector2Int> { point.Value });
			};
		}
	}
}