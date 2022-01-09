using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{
		//获取P点四周为+-out_count的可以通过的点列表
		public static List<Vector2Int> GetAroundFreePointList(AStarMapPath astarMapPath, Vector2Int base_point,
		  int out_count,
		  int[] can_pass_obstacle_types,
		  int[] can_pass_terrain_types)
		{
			List<Vector2Int> list = new List<Vector2Int>();
			int x, y;
			int field = GetField(astarMapPath.GetFinalGrids()[base_point.x][base_point.y]); // 所属区块值
			bool can_pass = CanPass(astarMapPath, base_point.x, base_point.y, can_pass_obstacle_types,
			  can_pass_terrain_types); // 是否起始在障碍点

			y = base_point.y - out_count; // 下边一行
			if (IsInRangeY(astarMapPath.GetFinalGrids(), y))
				for (x = base_point.x - out_count; x <= base_point.x + out_count; x++)
				{
					if (IsInRange(astarMapPath.GetFinalGrids(), x, y) && (!can_pass ||
																		  field == GetField(astarMapPath.GetFinalGrids()[x][y]))
																	  && CanPass(astarMapPath, x, y, can_pass_obstacle_types,
																		can_pass_terrain_types))
						list.Add(new Vector2Int(x, y));
				}

			x = base_point.x + out_count; // 右边一行
			if (IsInRangeX(astarMapPath.GetFinalGrids(), x))
				for (y = base_point.y - out_count; y <= base_point.y + out_count; y++)
				{
					if (IsInRange(astarMapPath.GetFinalGrids(), x, y) && (!can_pass ||
																		  field == GetField(astarMapPath.GetFinalGrids()[x][y]))
																	  && CanPass(astarMapPath, x, y, can_pass_obstacle_types,
																		can_pass_terrain_types))
						list.Add(new Vector2Int(x, y));
				}

			y = base_point.y + out_count; // 上边一行
			if (IsInRangeY(astarMapPath.GetFinalGrids(), y))
				for (x = base_point.x + out_count; x >= base_point.x - out_count; x--)
				{
					if (IsInRange(astarMapPath.GetFinalGrids(), x, y) && (!can_pass ||
																		  field == GetField(astarMapPath.GetFinalGrids()[x][y]))
																	  && CanPass(astarMapPath, x, y, can_pass_obstacle_types,
																		can_pass_terrain_types))
						list.Add(new Vector2Int(x, y));
				}

			x = base_point.x - out_count; // 左边一行
			if (IsInRangeX(astarMapPath.GetFinalGrids(), x))
				for (y = base_point.y + out_count; y >= base_point.y - out_count; y--)
				{
					if (IsInRange(astarMapPath.GetFinalGrids(), x, y) && (!can_pass ||
																		  field == GetField(astarMapPath.GetFinalGrids()[x][y]))
																	  && CanPass(astarMapPath, x, y, can_pass_obstacle_types,
																		can_pass_terrain_types))
						list.Add(new Vector2Int(x, y));
				}

			return list;
		}

		//获取P点四周为+-out_count（包含边界）以内的可以通过的点
		public static Vector2Int? FindAroundFreePoint(AStarMapPath astarMapPath, Vector2Int base_point, int out_count,
		  List<Vector2Int> except_point_list,
		  int[] can_pass_obstacle_types, int[] can_pass_terrain_types, RandomManager randomManager = null)
		{
			randomManager = randomManager ?? Client.instance.randomManager;
			if (!IsInRange(astarMapPath.GetFinalGrids(), base_point))
				return null;

			List<Vector2Int> list = new List<Vector2Int>();
			if (CanPass(astarMapPath, base_point.x, base_point.y, can_pass_obstacle_types, can_pass_terrain_types) &&
				(except_point_list == null || !except_point_list.Contains(base_point)))
				list.Add(base_point);

			int max = Math.Max(Math.Max(base_point.x, astarMapPath.Width() - base_point.x),
			  Math.Max(base_point.y, astarMapPath.Height() - base_point.y));
			if (max > out_count)
				max = out_count;
			for (int i = 1; i <= max; i++)
			{
				List<Vector2Int> ls = GetAroundFreePointList(astarMapPath, base_point, i, can_pass_obstacle_types,
				  can_pass_terrain_types);
				list.AddRange(ls);
			}

			if (except_point_list != null)
				list.RemoveElementsOfSub(except_point_list);
			if (list.Count > 0)
				return list[randomManager.RandomInt(0, list.Count)];
			return null;
		}

		//获取P点四周为+-out_count的可以通过的点
		public static Vector2Int? FindAroundFreePoint(AStarMapPath astarMapPath, Vector2Int base_point, int out_count,
		  int[] can_pass_obstacle_types,
		  int[] can_pass_terrain_types, RandomManager randomManager = null)
		{
			randomManager = randomManager ?? Client.instance.randomManager;
			if (!IsInRange(astarMapPath.GetFinalGrids(), base_point))
				return null;

			List<Vector2Int> list = GetAroundFreePointList(astarMapPath, base_point, out_count, can_pass_obstacle_types,
			  can_pass_terrain_types);
			if (list.Count > 0)
				return list[randomManager.RandomInt(0, list.Count)];
			return null;
		}

		//获取P点四周为的可以通过的点
		public static Vector2Int? FindAroundFreePoint(AStarMapPath astarMapPath, Vector2Int base_point,
		  List<Vector2Int> except_point_list, int[] can_pass_obstacle_types,
		  int[] can_pass_terrain_types, RandomManager randomManager = null)
		{
			randomManager = randomManager ?? Client.instance.randomManager;
			if (!IsInRange(astarMapPath.GetFinalGrids(), base_point))
				return null;
			if (CanPass(astarMapPath, base_point.x, base_point.y, can_pass_obstacle_types, can_pass_terrain_types) &&
				(except_point_list == null || !except_point_list.Contains(base_point)))
				return base_point;

			int max = Math.Max(Math.Max(base_point.x, astarMapPath.Width() - base_point.x),
			  Math.Max(base_point.y, astarMapPath.Height() - base_point.y));
			for (int i = 1; i <= max; i++)
			{
				List<Vector2Int> list = GetAroundFreePointList(astarMapPath, base_point, i, can_pass_obstacle_types,
				  can_pass_terrain_types);
				if (except_point_list != null)
					list.RemoveElementsOfSub(except_point_list);
				if (list.Count > 0)
					return list[randomManager.RandomInt(0, list.Count)];
			}

			return null;
		}


	}
}