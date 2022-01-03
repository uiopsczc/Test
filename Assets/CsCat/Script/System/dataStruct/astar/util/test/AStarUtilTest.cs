using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class AStarUtilTest
	{

		public static int[][] __grids = new int[][]
		{
	  new int[] {1, 1, 3, 1, 1, 1},
	  new int[] {3, 1, 1, 1, 3, 1},
	  new int[] {3, 1, 1, 3, 1, 1},
	  new int[] {1, 1, 1, 1, 3, 1},
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

		public static Action Test_GetArcPointList()
		{
			Vector2Int center_point = Vector2Int.zero;
			int radius = 3;
			List<Vector2Int> point_list = AStarUtil.GetArcPointList(center_point, radius);
			return () => { AStarUtil.GUIShowPointList(-5, -5, 5, 5, point_list); };
		}

		public static Action Test_GetArcPointList2()
		{
			Vector2Int center_point = Vector2Int.zero;
			int radius = 3;
			List<Vector2Int> point_list = AStarUtil.GetArcPointList2(center_point, radius);
			return () => { AStarUtil.GUIShowPointList(-5, -5, 5, 5, point_list); };
		}

		public static Action Test_GetLinePointList()
		{
			Vector2Int a_point = new Vector2Int(-4, -4);
			Vector2Int b_point = new Vector2Int(3, 2);
			List<Vector2Int> point_list = AStarUtil.GetLinePointList(a_point, b_point);
			return () => { AStarUtil.GUIShowPointList(-5, -5, 5, 5, point_list); };
		}

		public static Action Test_GetExtendPoint()
		{
			Vector2Int a_point = new Vector2Int(-4, -4);
			Vector2Int b_point = new Vector2Int(3, 2);
			Vector2Int target_point = AStarUtil.GetExtendPoint(a_point, b_point, 2);
			return () => { AStarUtil.GUIShowPointList(-5, -5, 5, 5, new List<Vector2Int>() { target_point }); };
		}

		public static void Test_GetNearestDistance()
		{
			LogCat.log(AStarUtil.GetNearestDistance(new Vector2Int(0, 0), new Vector2Int(2, 2), new Vector2Int(2, 3)));
		}
	}
}