using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{
		public static bool IsInRangeX(int[][] rangeGrids, int x)
		{
			return x >= 0 && x <= rangeGrids.Length - 1;
		}

		public static bool IsInRangeY(int[][] rangeGrids, int y)
		{
			return y >= 0 && y <= rangeGrids[0].Length - 1;
		}

		public static bool IsInRange(int[][] rangeGrids, int x, int y)
		{
			return IsInRangeX(rangeGrids, x) && IsInRangeY(rangeGrids, y);
		}

		public static bool IsInRange(int[][] rangeGrids, Vector2Int p)
		{
			return IsInRange(rangeGrids, p.x, p.y);
		}
	}
}