using UnityEngine;

namespace CsCat
{
	public partial class AStarUtilTest
	{

		public static void Test_IsInRangeX()
		{
			LogCat.log(AStarUtil.IsInRangeX(grids, 6));
		}

		public static void Test_IsInRangeY()
		{
			LogCat.log(AStarUtil.IsInRangeY(grids, 5));
		}

		public static void Test_IsInRange()
		{
			LogCat.log(AStarUtil.IsInRange(grids, new Vector2Int(4, 5)));
		}



	}
}