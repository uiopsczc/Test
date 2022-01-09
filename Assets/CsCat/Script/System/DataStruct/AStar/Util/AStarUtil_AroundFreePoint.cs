using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{
		//获取P点四周为+-out_count的可以通过的点列表
		public static List<Vector2Int> GetAroundFreePointList(AStarMapPath astarMapPath, Vector2Int basePoint,
		  int outCount,
		  int[] canPassObstacleTypes,
		  int[] canPassTerrainTypes)
		{
			List<Vector2Int> list = new List<Vector2Int>();
			int x, y;
			int field = GetField(astarMapPath.GetFinalGrids()[basePoint.x][basePoint.y]); // 所属区块值
			bool canPass = CanPass(astarMapPath, basePoint.x, basePoint.y, canPassObstacleTypes,
			  canPassTerrainTypes); // 是否起始在障碍点

			y = basePoint.y - outCount; // 下边一行
			if (IsInRangeY(astarMapPath.GetFinalGrids(), y))
				for (x = basePoint.x - outCount; x <= basePoint.x + outCount; x++)
				{
					if (IsInRange(astarMapPath.GetFinalGrids(), x, y) && (!canPass ||
																		  field == GetField(astarMapPath.GetFinalGrids()[x][y]))
																	  && CanPass(astarMapPath, x, y, canPassObstacleTypes,
																		canPassTerrainTypes))
						list.Add(new Vector2Int(x, y));
				}

			x = basePoint.x + outCount; // 右边一行
			if (IsInRangeX(astarMapPath.GetFinalGrids(), x))
				for (y = basePoint.y - outCount; y <= basePoint.y + outCount; y++)
				{
					if (IsInRange(astarMapPath.GetFinalGrids(), x, y) && (!canPass ||
																		  field == GetField(astarMapPath.GetFinalGrids()[x][y]))
																	  && CanPass(astarMapPath, x, y, canPassObstacleTypes,
																		canPassTerrainTypes))
						list.Add(new Vector2Int(x, y));
				}

			y = basePoint.y + outCount; // 上边一行
			if (IsInRangeY(astarMapPath.GetFinalGrids(), y))
				for (x = basePoint.x + outCount; x >= basePoint.x - outCount; x--)
				{
					if (IsInRange(astarMapPath.GetFinalGrids(), x, y) && (!canPass ||
																		  field == GetField(astarMapPath.GetFinalGrids()[x][y]))
																	  && CanPass(astarMapPath, x, y, canPassObstacleTypes,
																		canPassTerrainTypes))
						list.Add(new Vector2Int(x, y));
				}

			x = basePoint.x - outCount; // 左边一行
			if (IsInRangeX(astarMapPath.GetFinalGrids(), x))
				for (y = basePoint.y + outCount; y >= basePoint.y - outCount; y--)
				{
					if (IsInRange(astarMapPath.GetFinalGrids(), x, y) && (!canPass ||
																		  field == GetField(astarMapPath.GetFinalGrids()[x][y]))
																	  && CanPass(astarMapPath, x, y, canPassObstacleTypes,
																		canPassTerrainTypes))
						list.Add(new Vector2Int(x, y));
				}

			return list;
		}

		//获取P点四周为+-out_count（包含边界）以内的可以通过的点
		public static Vector2Int? FindAroundFreePoint(AStarMapPath astarMapPath, Vector2Int basePoint, int outCount,
		  List<Vector2Int> exceptPointList,
		  int[] canPassObstacleTypes, int[] canPassTerrainTypes, RandomManager randomManager = null)
		{
			randomManager = randomManager ?? Client.instance.randomManager;
			if (!IsInRange(astarMapPath.GetFinalGrids(), basePoint))
				return null;

			List<Vector2Int> list = new List<Vector2Int>();
			if (CanPass(astarMapPath, basePoint.x, basePoint.y, canPassObstacleTypes, canPassTerrainTypes) &&
				(exceptPointList == null || !exceptPointList.Contains(basePoint)))
				list.Add(basePoint);

			int max = Math.Max(Math.Max(basePoint.x, astarMapPath.Width() - basePoint.x),
			  Math.Max(basePoint.y, astarMapPath.Height() - basePoint.y));
			if (max > outCount)
				max = outCount;
			for (int i = 1; i <= max; i++)
			{
				List<Vector2Int> ls = GetAroundFreePointList(astarMapPath, basePoint, i, canPassObstacleTypes,
				  canPassTerrainTypes);
				list.AddRange(ls);
			}

			if (exceptPointList != null)
				list.RemoveElementsOfSub(exceptPointList);
			if (list.Count > 0)
				return list[randomManager.RandomInt(0, list.Count)];
			return null;
		}

		//获取P点四周为+-out_count的可以通过的点
		public static Vector2Int? FindAroundFreePoint(AStarMapPath astarMapPath, Vector2Int basePoint, int outCount,
		  int[] canPassObstacleTypes,
		  int[] canPassTerrainTypes, RandomManager randomManager = null)
		{
			randomManager = randomManager ?? Client.instance.randomManager;
			if (!IsInRange(astarMapPath.GetFinalGrids(), basePoint))
				return null;

			List<Vector2Int> list = GetAroundFreePointList(astarMapPath, basePoint, outCount, canPassObstacleTypes,
			  canPassTerrainTypes);
			if (list.Count > 0)
				return list[randomManager.RandomInt(0, list.Count)];
			return null;
		}

		//获取P点四周为的可以通过的点
		public static Vector2Int? FindAroundFreePoint(AStarMapPath astarMapPath, Vector2Int basePoint,
		  List<Vector2Int> exceptPointList, int[] canPassObstacleTypes,
		  int[] canPassTerrainTypes, RandomManager randomManager = null)
		{
			randomManager = randomManager ?? Client.instance.randomManager;
			if (!IsInRange(astarMapPath.GetFinalGrids(), basePoint))
				return null;
			if (CanPass(astarMapPath, basePoint.x, basePoint.y, canPassObstacleTypes, canPassTerrainTypes) &&
				(exceptPointList == null || !exceptPointList.Contains(basePoint)))
				return basePoint;

			int max = Math.Max(Math.Max(basePoint.x, astarMapPath.Width() - basePoint.x),
			  Math.Max(basePoint.y, astarMapPath.Height() - basePoint.y));
			for (int i = 1; i <= max; i++)
			{
				List<Vector2Int> list = GetAroundFreePointList(astarMapPath, basePoint, i, canPassObstacleTypes,
				  canPassTerrainTypes);
				if (exceptPointList != null)
					list.RemoveElementsOfSub(exceptPointList);
				if (list.Count > 0)
					return list[randomManager.RandomInt(0, list.Count)];
			}

			return null;
		}


	}
}