using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class AStarUtilTest
	{

		public static void Test_CanPass1()
		{
			LogCat.log(AStarUtil.CanPass(grids, 2, 2, AStarMapPathConst.Critter_Can_Pass_Obstacle_Types,
			  AStarMapPathConst.User_Can_Pass_Terrain_Types));
		}

		public static void Test_CanPass2()
		{
			LogCat.log(AStarUtil.CanPass(new AStarMapPath(grids), new List<Vector2Int>
	  {
		new Vector2Int(1, 1),
		new Vector2Int(2, 2),
		new Vector2Int(3, 3)
	  }, AStarMapPathConst.Critter_Can_Pass_Obstacle_Types, AStarMapPathConst.User_Can_Pass_Terrain_Types));
		}

	}
}