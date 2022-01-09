using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class AStarUtilTest
	{

		public static Action Test_GetNeighborList()
		{
			List<Vector2Int> pointList = AStarUtil.GetNeighborList(new Vector2Int(2, 2));
			return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, pointList); };
		}

		public static Action Test_GetLeftTopNeighborList()
		{
			List<Vector2Int> pointList = AStarUtil.GetLeftTopNeighborList(new Vector2Int(2, 2));
			return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, pointList); };
		}

		public static Action Test_GetLeftBottomNeighborList()
		{
			List<Vector2Int> pointList = AStarUtil.GetLeftBottomNeighborList(new Vector2Int(2, 2));
			return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, pointList); };
		}

		public static Action Test_GetRightTopNeighborList()
		{
			List<Vector2Int> pointList = AStarUtil.GetRightTopNeighborList(new Vector2Int(2, 2));
			return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, pointList); };
		}

		public static Action Test_GetRightBottomNeighborList()
		{
			List<Vector2Int> pointList = AStarUtil.GetRightBottomNeighborList(new Vector2Int(2, 2));
			return () => { AStarUtil.GUIShowPointList(0, 0, 9, 9, pointList); };
		}


	}
}