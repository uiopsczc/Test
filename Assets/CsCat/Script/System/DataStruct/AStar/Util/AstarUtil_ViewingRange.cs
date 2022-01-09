using System;
using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{
		//是否在视图内
		public static bool IsInViewingRange(Vector2Int viewingRangeBasePoint, Vector2Int checkPoint)
		{
			return GetViewingRange(viewingRangeBasePoint, viewingRangeBasePoint).IsInRange(checkPoint);
		}

		//获得left_bottom和right_top的范围两边外延（AStarConst.Client_View_Width_Grid_Count / 2，AStarConst.Client_View_Height_Grid_Count / 2） 可见范围
		public static AStarRange GetViewingRange(Vector2Int leftBottom, Vector2Int rightTop)
		{
			int minX = Math.Min(leftBottom.x, rightTop.x) - AStarMapPathConst.Client_View_Width_Grid_Count / 2;
			int minY = Math.Min(leftBottom.y, rightTop.y) - AStarMapPathConst.Client_View_Height_Grid_Count / 2;
			int maxX = Math.Max(leftBottom.x, rightTop.x) + AStarMapPathConst.Client_View_Width_Grid_Count / 2;
			int maxY = Math.Max(leftBottom.y, rightTop.y) + AStarMapPathConst.Client_View_Height_Grid_Count / 2;
			return new AStarRange(minX, minY, maxX, maxY);
		}

		//获得可见范围
		public static AStarRange GetViewingRange(Vector2Int viewingRangeBasePoint)
		{
			return GetViewingRange(viewingRangeBasePoint, viewingRangeBasePoint);
		}

	}
}