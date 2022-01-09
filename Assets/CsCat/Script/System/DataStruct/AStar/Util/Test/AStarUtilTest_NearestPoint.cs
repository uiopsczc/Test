using System;
using UnityEngine;

namespace CsCat
{
	public partial class AStarUtilTest
	{
		public static Action Test_GetNearestPoint()
		{
			Vector2Int point = AStarUtil.GetNearestPoint(new AStarMapPath(grids), new Vector2Int(0, 0),
				new Vector2Int(3, 3)
				, AStarMapPathConst.Critter_Can_Pass_Obstacle_Types, AStarMapPathConst.User_Can_Pass_Terrain_Types);
			LogCat.log(point);
			return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, point); };
		}
	}
}