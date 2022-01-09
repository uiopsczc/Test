using System;
using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{
		//是否在视图内
		public static bool IsInViewingRange(Vector2Int viewing_range_base_point, Vector2Int check_point)
		{
			return GetViewingRange(viewing_range_base_point, viewing_range_base_point).IsInRange(check_point);
		}

		//获得left_bottom和right_top的范围两边外延（AStarConst.Client_View_Width_Grid_Count / 2，AStarConst.Client_View_Height_Grid_Count / 2） 可见范围
		public static AStarRange GetViewingRange(Vector2Int left_bottom, Vector2Int right_top)
		{
			int min_x = Math.Min(left_bottom.x, right_top.x) - AStarMapPathConst.Client_View_Width_Grid_Count / 2;
			int min_y = Math.Min(left_bottom.y, right_top.y) - AStarMapPathConst.Client_View_Height_Grid_Count / 2;
			int max_x = Math.Max(left_bottom.x, right_top.x) + AStarMapPathConst.Client_View_Width_Grid_Count / 2;
			int max_y = Math.Max(left_bottom.y, right_top.y) + AStarMapPathConst.Client_View_Height_Grid_Count / 2;
			return new AStarRange(min_x, min_y, max_x, max_y);
		}

		//获得可见范围
		public static AStarRange GetViewingRange(Vector2Int viewing_range_base_point)
		{
			return GetViewingRange(viewing_range_base_point, viewing_range_base_point);
		}

	}
}