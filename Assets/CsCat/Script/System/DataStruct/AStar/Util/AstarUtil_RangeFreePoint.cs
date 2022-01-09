using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{

		//获取range范围内的可以通过的格子列表
		public static List<Vector2Int> GetRangeFreePointList(AStarMapPath astarMapPath, int x1, int y1, int x2, int y2,
		  List<Vector2Int> except_point_list,
		  int[] can_pass_obstacle_types, int[] can_pass_terrain_types)
		{
			List<Vector2Int> list = new List<Vector2Int>();
			for (int x = x1; x <= x2; x++)
			{
				for (int y = y1; y <= y2; y++)
				{
					if (!IsInRange(astarMapPath.GetFinalGrids(), x, y))
						continue;
					bool can_pass = CanPass(astarMapPath, x, y, can_pass_obstacle_types, can_pass_terrain_types);
					if (can_pass)
					{
						Vector2Int p = new Vector2Int(x, y);
						if (except_point_list == null || !except_point_list.Contains(p))
							list.Add(p);
					}
				}
			}

			return list;
		}


		//获取range范围内的可以通过的格子
		public static Vector2Int? FindRangeFreePoint(AStarMapPath astarMapPath, int x1, int y1, int x2, int y2,
		  int[] can_pass_obstacle_types, int[] can_pass_terrain_types, RandomManager randomManager = null)
		{
			return FindRangeFreePoint(astarMapPath, x1, y1, x2, y2, null,
			  can_pass_obstacle_types, can_pass_terrain_types, randomManager);
		}


		//获取range范围内的可以通过的格子
		public static Vector2Int? FindRangeFreePoint(AStarMapPath astarMapPath, int x1, int y1, int x2, int y2,
		  List<Vector2Int> except_point_list,
		  int[] can_pass_obstacle_types, int[] can_pass_terrain_types, RandomManager randomManager = null)
		{
			randomManager = randomManager ?? Client.instance.randomManager;
			List<Vector2Int> list = GetRangeFreePointList(astarMapPath, x1, y1, x2, y2, except_point_list,
			  can_pass_obstacle_types, can_pass_terrain_types);

			if (except_point_list != null)
			{
				list.RemoveElementsOfSub(except_point_list);
			}

			if (list.Count > 0)
				return list[randomManager.RandomInt(0, list.Count)];
			return null;
		}


	}
}