using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{
		//获取离a,b最近的点
		public static Vector2Int GetNearestPoint(AStarMapPath astarMapPath, Vector2Int pointA, Vector2Int pointB,
		  int[] canPassObstacleTypes, int[] canPassTerrainTypes)
		{
			Vector2Int p = pointA;
			do
			{
				if (!p.Equals(pointA))
					pointA = p;
				p = GetNearestNearbyPoint(astarMapPath, pointA, pointB, canPassObstacleTypes, canPassTerrainTypes);
			} while (!p.Equals(pointA));

			return p;
		}

		private static Vector2Int GetNearestNearbyPoint(AStarMapPath astarMapPath, Vector2Int pointA, Vector2Int pointB,
		  int[] canPassObstacleTypes,
		  int[] canPassTerrainTypes)
		{
			int dx = pointB.x > pointA.x ? 1 : pointB.x < pointA.x ? -1 : 0;
			int dy = pointB.y > pointA.y ? 1 : pointB.y < pointA.y ? -1 : 0;

			int minDistance = GetMapDistance(pointA, pointB);
			Vector2Int minPoint = pointA;
			int x, y;

			x = pointA.x + dx;
			y = pointA.y;
			bool s1 = false;
			if (IsInRange(astarMapPath.GetFinalGrids(), x, y))
			{
				Vector2Int p = new Vector2Int(x, y);
				if (CanPass(astarMapPath, x, y, canPassObstacleTypes, canPassTerrainTypes))
				{
					s1 = true;
					int d = GetMapDistance(p, pointB);
					if (d < minDistance)
					{
						minPoint = p;
						minDistance = d;
					}
				}
			}

			x = pointA.x;
			y = pointA.y + dy;
			bool s2 = false;
			if (IsInRange(astarMapPath.GetFinalGrids(), x, y))
			{
				Vector2Int p = new Vector2Int(x, y);
				if (CanPass(astarMapPath, x, y, canPassObstacleTypes, canPassTerrainTypes))
				{
					s2 = true;
					int d = GetMapDistance(p, pointB);
					if (d < minDistance)
					{
						minPoint = p;
						minDistance = d;
					}
				}
			}

			if (s1 || s2)
			{
				x = pointA.x + dx;
				y = pointA.y + dy;
				if (IsInRange(astarMapPath.GetFinalGrids(), x, y))
				{
					Vector2Int p = new Vector2Int(x, y);
					if (CanPass(astarMapPath, x, y, canPassObstacleTypes, canPassTerrainTypes))
					{
						int d = GetMapDistance(p, pointB);
						if (d < minDistance)
						{
							minPoint = p;
							minDistance = d;
						}
					}
				}
			}

			return minPoint;
		}


	}
}