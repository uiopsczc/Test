using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{
		//获取离a,b最近的点
		public static Vector2Int GetNearestPoint(AStarMapPath astarMapPath, Vector2Int point_a, Vector2Int point_b,
		  int[] can_pass_obstacle_types, int[] can_pass_terrain_types)
		{
			Vector2Int p = point_a;
			do
			{
				if (!p.Equals(point_a))
					point_a = p;
				p = GetNearestNearbyPoint(astarMapPath, point_a, point_b, can_pass_obstacle_types, can_pass_terrain_types);
			} while (!p.Equals(point_a));

			return p;
		}

		private static Vector2Int GetNearestNearbyPoint(AStarMapPath astarMapPath, Vector2Int point_a, Vector2Int point_b,
		  int[] can_pass_obstacle_types,
		  int[] can_pass_terrain_types)
		{
			int dx = point_b.x > point_a.x ? 1 : point_b.x < point_a.x ? -1 : 0;
			int dy = point_b.y > point_a.y ? 1 : point_b.y < point_a.y ? -1 : 0;

			int min_distance = GetMapDistance(point_a, point_b);
			Vector2Int min_point = point_a;
			int x, y;

			x = point_a.x + dx;
			y = point_a.y;
			bool s1 = false;
			if (IsInRange(astarMapPath.GetFinalGrids(), x, y))
			{
				Vector2Int p = new Vector2Int(x, y);
				if (CanPass(astarMapPath, x, y, can_pass_obstacle_types, can_pass_terrain_types))
				{
					s1 = true;
					int d = GetMapDistance(p, point_b);
					if (d < min_distance)
					{
						min_point = p;
						min_distance = d;
					}
				}
			}

			x = point_a.x;
			y = point_a.y + dy;
			bool s2 = false;
			if (IsInRange(astarMapPath.GetFinalGrids(), x, y))
			{
				Vector2Int p = new Vector2Int(x, y);
				if (CanPass(astarMapPath, x, y, can_pass_obstacle_types, can_pass_terrain_types))
				{
					s2 = true;
					int d = GetMapDistance(p, point_b);
					if (d < min_distance)
					{
						min_point = p;
						min_distance = d;
					}
				}
			}

			if (s1 || s2)
			{
				x = point_a.x + dx;
				y = point_a.y + dy;
				if (IsInRange(astarMapPath.GetFinalGrids(), x, y))
				{
					Vector2Int p = new Vector2Int(x, y);
					if (CanPass(astarMapPath, x, y, can_pass_obstacle_types, can_pass_terrain_types))
					{
						int d = GetMapDistance(p, point_b);
						if (d < min_distance)
						{
							min_point = p;
							min_distance = d;
						}
					}
				}
			}

			return min_point;
		}


	}
}