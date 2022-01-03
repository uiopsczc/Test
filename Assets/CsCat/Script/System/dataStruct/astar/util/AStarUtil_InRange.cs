using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{
		public static bool IsInRangeX(int[][] range_grids, int x)
		{
			if (x < 0 || x > range_grids.Length - 1)
				return false;
			return true;
		}

		public static bool IsInRangeY(int[][] range_grids, int y)
		{
			if (y < 0 || y > range_grids[0].Length - 1)
				return false;
			return true;
		}

		public static bool IsInRange(int[][] range_grids, int x, int y)
		{
			if (!IsInRangeX(range_grids, x) || !IsInRangeY(range_grids, y))
				return false;
			return true;
		}

		public static bool IsInRange(int[][] range_grids, Vector2Int p)
		{
			return IsInRange(range_grids, p.x, p.y);
		}
	}
}