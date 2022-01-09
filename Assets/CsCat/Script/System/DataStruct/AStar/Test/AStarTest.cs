using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class AStarTest
	{
		public static int[][] __grids = new int[][]
		{
	  new int[] {1, 1, 3, 1, 1, 1},
	  new int[] {3, 1, 1, 1, 3, 1},
	  new int[] {3, 1, 1, 3, 1, 1},
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

		public static Action Test_Find()
		{
			AStarImpl astar = new AStarImpl(new AStarMapPath(grids), default(AStarHType),
			  AStarMapPathConst.Critter_Can_Pass_Obstacle_Types,
			  AStarMapPathConst.User_Can_Pass_Terrain_Types);
			List<Vector2Int> list = astar.Find(1, 1, 3, 4);
			return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, list); };
		}


	}
}