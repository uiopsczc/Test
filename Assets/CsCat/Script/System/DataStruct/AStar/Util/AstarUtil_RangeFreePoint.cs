using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{

		//获取range范围内的可以通过的格子列表
		public static List<Vector2Int> GetRangeFreePointList(AStarMapPath astarMapPath, int x1, int y1, int x2, int y2,
		  List<Vector2Int> exceptPointList,
		  int[] canPassObstacleTypes, int[] canPassTerrainTypes)
		{
			List<Vector2Int> list = new List<Vector2Int>();
			for (int x = x1; x <= x2; x++)
			{
				for (int y = y1; y <= y2; y++)
				{
					if (!IsInRange(astarMapPath.GetFinalGrids(), x, y))
						continue;
					bool canPass = CanPass(astarMapPath, x, y, canPassObstacleTypes, canPassTerrainTypes);
					if (canPass)
					{
						Vector2Int p = new Vector2Int(x, y);
						if (exceptPointList == null || !exceptPointList.Contains(p))
							list.Add(p);
					}
				}
			}

			return list;
		}


		//获取range范围内的可以通过的格子
		public static Vector2Int? FindRangeFreePoint(AStarMapPath astarMapPath, int x1, int y1, int x2, int y2,
		  int[] canPassObstacleTypes, int[] canPassTerrainTypes, RandomManager randomManager = null)
		{
			return FindRangeFreePoint(astarMapPath, x1, y1, x2, y2, null,
			  canPassObstacleTypes, canPassTerrainTypes, randomManager);
		}


		//获取range范围内的可以通过的格子
		public static Vector2Int? FindRangeFreePoint(AStarMapPath astarMapPath, int x1, int y1, int x2, int y2,
		  List<Vector2Int> exceptPointList,
		  int[] canPassObstacleTypes, int[] canPassTerrainTypes, RandomManager randomManager = null)
		{
			randomManager = randomManager ?? Client.instance.randomManager;
			List<Vector2Int> list = GetRangeFreePointList(astarMapPath, x1, y1, x2, y2, exceptPointList,
			  canPassObstacleTypes, canPassTerrainTypes);

			if (exceptPointList != null)
			{
				list.RemoveElementsOfSub(exceptPointList);
			}

			if (list.Count > 0)
				return list[randomManager.RandomInt(0, list.Count)];
			return null;
		}


	}
}