using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{
		//获取以center_point为中心，半径为radius的圆弧形格子列表
		public static List<Vector2Int> GetArcPointList(Vector2Int center_point, int radius)
		{
			List<Vector2Int> list = new List<Vector2Int>();

			Vector2Int left = new Vector2Int(center_point.x - radius, center_point.y);
			Vector2Int top = new Vector2Int(center_point.x, center_point.y + radius);
			Vector2Int right = new Vector2Int(center_point.x + radius, center_point.y);
			Vector2Int bottom = new Vector2Int(center_point.x, center_point.y - radius);

			Vector2Int a = left;
			list.Add(a);
			while (true)
			{
				a = GetArcFitPoint(center_point, a, radius, 1, 1);
				if (a.x > top.x || a.y > top.y)
					break;
				if (!list.Contains(a))
					list.Add(a);
			}

			a = top;
			list.Add(a);
			while (true)
			{
				a = GetArcFitPoint(center_point, a, radius, 1, -1);
				if (a.x > right.x || a.y < right.y)
					break;
				if (!list.Contains(a))
					list.Add(a);
			}

			a = right;
			list.Add(a);
			while (true)
			{
				a = GetArcFitPoint(center_point, a, radius, -1, -1);
				if (a.x < bottom.x || a.y < bottom.y)
					break;
				if (!list.Contains(a))
					list.Add(a);
			}

			a = bottom;
			list.Add(a);
			while (true)
			{
				a = GetArcFitPoint(center_point, a, radius, -1, 1);
				if (a.x < left.x || a.y > left.y)
					break;
				if (!list.Contains(a))
					list.Add(a);
			}

			return list;
		}

		private static Vector2Int GetArcFitPoint(Vector2Int center_point, Vector2Int base_point, int radius, int dx, int dy)
		{
			Vector2Int p1 = new Vector2Int(base_point.x + dx, base_point.y);
			Vector2Int p2 = new Vector2Int(base_point.x, base_point.y + dy);
			Vector2Int p3 = new Vector2Int(base_point.x + dx, base_point.y + dy);

			double d1 = Vector2Int.Distance(center_point, p1);
			double d2 = Vector2Int.Distance(center_point, p2);
			double d3 = Vector2Int.Distance(center_point, p3);
			double dd1 = Math.Abs(d1 - radius);
			double dd2 = Math.Abs(d2 - radius);
			double dd3 = Math.Abs(d3 - radius);
			if (dd1 < dd2)
			{
				if (dd1 < dd3)
					return p1;
				else
					return p3;
			}
			else
			{
				if (dd2 < dd3)
					return p2;
				else
					return p3;
			}
		}

		//获取以center_point为中心，半径为radius的圆弧形格子列表
		public static List<Vector2Int> GetArcPointList2(Vector2Int center_point, int radius)
		{
			List<Vector2Int> list = new List<Vector2Int>();

			Vector2Int left = new Vector2Int(center_point.x - radius, center_point.y);
			Vector2Int top = new Vector2Int(center_point.x, center_point.y + radius);
			Vector2Int right = new Vector2Int(center_point.x + radius, center_point.y);
			Vector2Int bottom = new Vector2Int(center_point.x, center_point.y - radius);

			Vector2Int a = left;
			list.Add(a);
			while (true)
			{
				a = GetArcFitPoint2(list, center_point, a, radius, 1, 1);
				if (a.x > top.x || a.y > top.y)
					break;
				if (!list.Contains(a))
					list.Add(a);
			}

			a = top;
			list.Add(a);
			while (true)
			{
				a = GetArcFitPoint2(list, center_point, a, radius, 1, -1);
				if (a.x > right.x || a.y < right.y)
					break;
				if (!list.Contains(a))
					list.Add(a);
			}

			a = right;
			list.Add(a);
			while (true)
			{
				a = GetArcFitPoint2(list, center_point, a, radius, -1, -1);
				if (a.x < bottom.x || a.y < bottom.y)
					break;
				if (!list.Contains(a))
					list.Add(a);
			}

			a = bottom;
			list.Add(a);
			while (true)
			{
				a = GetArcFitPoint2(list, center_point, a, radius, -1, 1);
				if (a.x < left.x || a.y > left.y)
					break;
				if (!list.Contains(a))
					list.Add(a);
			}

			return list;
		}

		public static Vector2Int GetArcFitPoint2(List<Vector2Int> list, Vector2Int center_point, Vector2Int base_point,
		  int radius, int dx, int dy)
		{
			Vector2Int p1 = new Vector2Int(base_point.x + dx, base_point.y);
			Vector2Int p2 = new Vector2Int(base_point.x, base_point.y + dy);
			Vector2Int p3 = new Vector2Int(base_point.x + dx, base_point.y + dy);

			double d1 = Vector2Int.Distance(center_point, p1);
			double d2 = Vector2Int.Distance(center_point, p2);
			double d3 = Vector2Int.Distance(center_point, p3);
			if (Math.Round(d1) == radius && !list.Contains(p1))
				list.Add(p1);
			if (Math.Round(d2) == radius && !list.Contains(p2))
				list.Add(p2);
			if (Math.Round(d3) == radius && !list.Contains(p3))
				list.Add(p3);

			double dd1 = Math.Abs(d1 - radius);
			double dd2 = Math.Abs(d2 - radius);
			double dd3 = Math.Abs(d3 - radius);
			if (dd1 < dd2)
			{
				if (dd1 < dd3)
					return p1;
				else
					return p3;
			}
			else
			{
				if (dd2 < dd3)
					return p2;
				else
					return p3;
			}
		}

		//列出两点间连线经过的所有格子
		public static List<Vector2Int> GetLinePointList(Vector2Int point_a, Vector2Int point_b)
		{
			List<Vector2Int> list = new List<Vector2Int>();
			if (point_a.Equals(point_b))
				list.Add(point_a);
			else if (point_a.x == point_b.x)
			{
				list.Add(point_a);
				int dv = point_a.y < point_b.y ? 1 : -1;
				for (int y = (point_a.y + dv); y * dv < point_b.y * dv; y += dv)
					list.Add(new Vector2Int(point_a.x, y));
				list.Add(point_b);
			}
			else if (point_a.y == point_b.y)
			{
				list.Add(point_a);
				int dv = point_a.x < point_b.x ? 1 : -1;
				for (int x = (point_a.x + dv); x * dv < point_b.x * dv; x += dv)
					list.Add(new Vector2Int(x, point_a.y));
				list.Add(point_b);
			}
			else
			{
				int x1 = point_a.x, y1 = point_a.y, x2 = point_b.x, y2 = point_b.y;
				int dx = x2 - x1, dy = y2 - y1;
				bool reverse = false;
				if (Math.Abs(dx) >= Math.Abs(dy))
				{
					if (x1 > x2)
					{
						int t = x1;
						x1 = x2;
						x2 = t;
						t = y1;
						y1 = y2;
						y2 = t;

						dx = -dx;
						dy = -dy;
						reverse = true;
					}

					int ddx = dx * 2, ddy = dy * 2;
					if (dy >= 0)
					{
						// 直线的倾斜角位于 [0, pi / 4]
						for (int x = x1, y = y1, e = -dx; x <= x2; x++)
						{
							if (reverse)
								list.AddFirst(new Vector2Int(x, y));
							else
								list.Add(new Vector2Int(x, y));
							e += ddy;
							if (e >= 0)
							{
								y++;
								e -= ddx;
							}
						}
					}
					else
					{
						// 直线的倾斜角位于 [-pi / 4, 0)
						for (int x = x1, y = y1, e = dx; x <= x2; x++)
						{
							if (reverse)
								list.AddFirst(new Vector2Int(x, y));
							else
								list.Add(new Vector2Int(x, y));
							e += ddy;
							if (e <= 0)
							{
								y--;
								e += ddx;
							}
						}
					}
				}
				else
				{
					if (y1 > y2)
					{
						int t = x1;
						x1 = x2;
						x2 = t;
						t = y1;
						y1 = y2;
						y2 = t;

						dx = -dx;
						dy = -dy;
						reverse = true;
					}

					int ddx = dx * 2, ddy = dy * 2;
					if (dx >= 0)
					{
						// 直线的倾斜角位于 (pi / 4, pi / 2]
						for (int x = x1, y = y1, e = -dy; y <= y2; y++)
						{
							if (reverse)
								list.AddFirst(new Vector2Int(x, y));
							else
								list.Add(new Vector2Int(x, y));
							e += ddx;
							if (e >= 0)
							{
								x++;
								e -= ddy;
							}
						}
					}
					else
					{
						// 直线的倾斜角位于 [-pi / 2, -pi / 4)
						for (int x = x1, y = y1, e = dy; y <= y2; y++)
						{
							if (reverse)
								list.AddFirst(new Vector2Int(x, y));
							else
								list.Add(new Vector2Int(x, y));
							e += ddx;
							if (e <= 0)
							{
								x--;
								e += ddy;
							}
						}
					}
				}
			}

			return list;
		}

		//获得线段p1p2延长线上的距离d以外的某点
		//distance > 0
		public static Vector2Int GetExtendPoint(Vector2Int point_a, Vector2Int point_b, int distance)
		{
			int updx = point_b.x - point_a.x;
			int updy = point_b.y - point_a.y;
			if (updx == 0 && updy == 0) // p1==p2
				return new Vector2Int(point_a.x + distance, point_a.y + distance);
			else if (updx == 0)
				return new Vector2Int(point_b.x, point_b.y + (updy > 0 ? distance : -distance));
			else if (updy == 0)
				return new Vector2Int(point_b.x + (updx > 0 ? distance : -distance), point_b.y);
			else
			{
				int dx = Math.Abs(updx);
				int dy = Math.Abs(updy);
				if (dx > dy)
				{
					int x = point_b.x + (updx > 0 ? distance : -distance);
					int y = updy * (x - point_a.x) / updx + point_a.y;
					return new Vector2Int(x, y);
				}
				else if (dx < dy)
				{
					int y = point_b.y + (updy > 0 ? distance : -distance);
					int x = updx * (y - point_a.y) / updy + point_a.x;
					return new Vector2Int(x, y);
				}
				else
					return new Vector2Int(point_b.x + (updx > 0 ? distance : -distance),
					  point_b.y + (updy > 0 ? distance : -distance));
			}
		}

		//获得point点离线段point_a,point_b得最短距离
		public static int GetNearestDistance(Vector2Int point_a, Vector2Int point_b, Vector2Int point)
		{
			if (point_a.Equals(point) || point_b.Equals(point)) // 与a点或b点重合findRangeFreePoint
				return 0;
			int a = GetMapDistance(point_b, point);
			if (point_a.Equals(point_b)) // a点和b点重合
				return a;
			int b = GetMapDistance(point_a, point);
			int c = GetMapDistance(point_a, point_b);

			if (a * a >= b * b + c * c) // 如果p点与a点内夹角是钝角，则返回b
				return b;
			if (b * b >= a * a + c * c) // 如果p点与b点内夹角是钝角，则返回a
				return a;

			double l = (a + b + c) / 2d; // 周长的一半
			double s = Math.Sqrt(l * (l - a) * (l - b) * (l - c)); // 海伦公式求面积，也可以用矢量求
			return (int)(2 * s / c);
		}

		public static int GetMapDistance(Vector2Int point_a, Vector2Int point_b)
		{
			int dx = point_a.x - point_b.x;
			int dy = point_a.y - point_b.y;
			int ddx = dx - dy;
			int ddy = dx + dy;
			return (int)Math.Sqrt(0.8 * ddx * ddx + 0.2 * ddy * ddy);
		}



		public static bool IsInAround(Vector2Int pos, Vector2Int compare_pos, int radius)
		{
			if (AStarUtil.GetMapDistance(pos, compare_pos) <= radius)
				return true;
			return false;
		}

		public static bool IsInSector(Vector2Int pos, Vector2Int sector_center_pos, Vector2 sector_dir, int sector_radius,
		  float sector_half_degrees)
		{
			int distance = GetMapDistance(pos, sector_center_pos);
			if (distance > sector_radius)
				return false;
			Vector2 v1 = sector_dir;
			Vector2 v2 = pos - sector_center_pos;
			double degree_between_v1_v2 = Math.Acos(Vector2.Dot(v1, v2) / (v1.magnitude * v2.magnitude)) * Mathf.Rad2Deg;
			if (degree_between_v1_v2 <= sector_half_degrees)
				return true;
			return false;
		}

	}
}