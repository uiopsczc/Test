using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{
		public static Vector2Int? GetRandomMovePoint(AStarMapPath astarMapPath, Vector2Int basePoint,
		  Vector2Int goalPoint, int maxRadiusBetweenTargetPointAndGoalPoint, int[] canPassObstacleTypes,
		  int[] canPassTerrainTypes, RandomManager randomManager = null)
		{
			randomManager = randomManager ?? Client.instance.randomManager;
			int outCount = randomManager.RandomInt(AStarMapPathConst.Random_Move_Distance_Min,
			  AStarMapPathConst.Random_Move_Distance_Max + 1);
			List<Vector2Int> list = GetAroundFreePointList(astarMapPath, basePoint, outCount, canPassObstacleTypes,
			  canPassTerrainTypes);
			while (list.Count > 0)
			{
				int removeIndex = randomManager.RandomInt(0, list.Count);
				Vector2Int targetPoint = list[removeIndex];
				list.RemoveAt(removeIndex);
				if (Vector2Int.Distance(goalPoint, targetPoint) <= maxRadiusBetweenTargetPointAndGoalPoint)
					return targetPoint;
			}

			return null;
		}


	}
}