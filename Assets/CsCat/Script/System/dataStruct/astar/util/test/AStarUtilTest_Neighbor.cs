using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class AStarUtilTest
	{

		public static Action Test_GetNeighborList()
		{
			List<Vector2Int> point_list = AStarUtil.GetNeighborList(new Vector2Int(2, 2));
			return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, point_list); };
		}

		public static Action Test_GetLeftTopNeighborList()
		{
			List<Vector2Int> point_list = AStarUtil.GetLeftTopNeighborList(new Vector2Int(2, 2));
			return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, point_list); };
		}

		public static Action Test_GetLeftBottomNeighborList()
		{
			List<Vector2Int> point_list = AStarUtil.GetLeftBottomNeighborList(new Vector2Int(2, 2));
			return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, point_list); };
		}

		public static Action Test_GetRightTopNeighborList()
		{
			List<Vector2Int> point_list = AStarUtil.GetRightTopNeighborList(new Vector2Int(2, 2));
			return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, point_list); };
		}

		public static Action Test_GetRightBottomNeighborList()
		{
			List<Vector2Int> point_list = AStarUtil.GetRightBottomNeighborList(new Vector2Int(2, 2));
			return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, point_list); };
		}


	}
}