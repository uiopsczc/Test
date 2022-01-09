using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public partial class AStarMapPathUtil
	{
		//先对角线查找，再直角查找
		public static List<Vector2Int> DirectFindPath(AStarMapPath astarMapPath, Vector2Int pointA, Vector2Int pointB,
			int[] canPassObstacleTypes, int[] canPassTerrainTypes)
		{
			if (!AStarUtil.IsInRange(astarMapPath.GetFinalGrids(), pointA) ||
				!AStarUtil.IsInRange(astarMapPath.GetFinalGrids(), pointB))
				return null;
			List<Vector2Int> list = null;
			if (pointA.Equals(pointB)) // 同一点
			{
				list = new List<Vector2Int> { pointA };
			}
			else if (pointA.x == pointB.x)
			{
				list = new List<Vector2Int> { pointA };
				int dv = pointB.y > pointA.y ? 1 : -1;
				for (int y = pointA.y + dv; y * dv < pointB.y * dv; y += dv)
				{
					if (!AStarUtil.CanPass(astarMapPath, pointA.x, y, canPassObstacleTypes, canPassTerrainTypes))
						return null;
					list.Add(new Vector2Int(pointA.x, y));
				}

				list.Add(pointB);
			}
			else if (pointA.y == pointB.y)
			{
				list = new List<Vector2Int> { pointA };
				int dv = pointB.x > pointA.x ? 1 : -1;
				for (int x = pointA.x + dv; x * dv < pointB.x * dv; x += dv)
				{
					if (!AStarUtil.CanPass(astarMapPath, x, pointA.y, canPassObstacleTypes, canPassTerrainTypes))
						return null;
					list.Add(new Vector2Int(x, pointA.y));
				}

				list.Add(pointB);
			}
			else
			{
				//先对角线查找，再直角查找
				list = DiagonallyFindPath(astarMapPath, pointA, pointB, canPassObstacleTypes,
					canPassTerrainTypes);
				if (list == null)
				{
					list = DiagonallyFindPath(astarMapPath, pointB, pointA, canPassObstacleTypes,
						canPassTerrainTypes);
					if (list == null)
					{
						list = BorderFindPath(astarMapPath, pointA, pointB, canPassObstacleTypes,
							canPassTerrainTypes);
						if (list == null)
						{
							list = BorderFindPath(astarMapPath, pointB, pointA, canPassObstacleTypes,
								canPassTerrainTypes);
							list?.Reverse();
						}
					}
					else
						list.Reverse();
				}
			}

			return list;
		}


		//直角寻路(先横向再纵向寻路)
		public static List<Vector2Int> BorderFindPath(AStarMapPath astarMapPath, Vector2Int pointA, Vector2Int pointB,
			int[] can_pass_obstacle_types, int[] can_pass_terrain_types)
		{
			if (!AStarUtil.IsInRange(astarMapPath.GetFinalGrids(), pointA) ||
				!AStarUtil.IsInRange(astarMapPath.GetFinalGrids(), pointB))
				return null;
			List<Vector2Int> list = new List<Vector2Int> { pointA };
			int dv = pointB.x > pointA.x ? 1 : -1;
			for (int x = pointA.x + dv; x * dv <= pointB.x * dv; x += dv)
			{
				//      LogCat.log(x, point_a.y);
				if (!AStarUtil.CanPass(astarMapPath, x, pointA.y, can_pass_obstacle_types, can_pass_terrain_types))
					return null;
				list.Add(new Vector2Int(x, pointA.y));
			}

			dv = pointB.y > pointA.y ? 1 : -1;
			for (int y = pointA.y + dv; y * dv < pointB.y * dv; y += dv)
			{
				if (!AStarUtil.CanPass(astarMapPath, pointB.x, y, can_pass_obstacle_types, can_pass_terrain_types))
					return null;
				list.Add(new Vector2Int(pointB.x, y));
			}

			list.Add(pointB);
			return list;
		}

		//对角线寻路
		public static List<Vector2Int> DiagonallyFindPath(AStarMapPath astarMapPath, Vector2Int pointA,
			Vector2Int pointB,
			int[] canPassObstacleTypes,
			int[] canPassTerrainTypes)
		{
			if (!AStarUtil.IsInRange(astarMapPath.GetFinalGrids(), pointA) ||
				!AStarUtil.IsInRange(astarMapPath.GetFinalGrids(), pointB))
				return null;
			List<Vector2Int> list = new List<Vector2Int>();

			int dx = pointB.x - pointA.x;
			int dy = pointB.y - pointA.y;
			if (Math.Abs(dx) > Math.Abs(dy))
			{
				int x1;
				if (dx > 0)
					x1 = pointA.x + Math.Abs(dy);
				else
					x1 = pointA.x - Math.Abs(dy);
				Vector2Int p = new Vector2Int(x1, pointB.y);
				if (!AStarUtil.CanPass(astarMapPath, p.x, p.y, canPassObstacleTypes, canPassTerrainTypes))
					return null;
				List<Vector2Int> list1 = AStarUtil.GetLinePointList(pointA, p);
				if (!AStarUtil.CanPass(astarMapPath, list1, canPassObstacleTypes, canPassTerrainTypes))
					return null;
				List<Vector2Int> list2 = AStarUtil.GetLinePointList(p, pointB);
				if (!AStarUtil.CanPass(astarMapPath, list2, canPassObstacleTypes, canPassTerrainTypes))
					return null;
				list.AddRange(list1);
				list.RemoveLast(); //删掉p
				list.AddRange(list2);
			}
			else
			{
				int y1;
				if (dy > 0)
					y1 = pointA.y + Math.Abs(dx);
				else
					y1 = pointA.y - Math.Abs(dx);
				Vector2Int p = new Vector2Int(pointB.x, y1);
				if (!AStarUtil.CanPass(astarMapPath, p.x, p.y, canPassObstacleTypes, canPassTerrainTypes))
					return null;
				List<Vector2Int> list1 = AStarUtil.GetLinePointList(pointA, p);
				if (!AStarUtil.CanPass(astarMapPath, list1, canPassObstacleTypes, canPassTerrainTypes))
					return null;
				List<Vector2Int> list2 = AStarUtil.GetLinePointList(p, pointB);
				if (!AStarUtil.CanPass(astarMapPath, list2, canPassObstacleTypes, canPassTerrainTypes))
					return null;
				list.AddRange(list1);
				list.RemoveLast(); //删掉p
				list.AddRange(list2);
			}

			return list;
		}
	}
}