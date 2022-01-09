using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	//坐标是x增加是向右，y增加是向上（与unity的坐标系一致），数组用ToLeftBottomBaseArrays转换
	public static partial class AStarUtil
	{
		//获取以center_point为中心，半径为radius的圆弧形格子列表
		public static List<Vector2Int> GetArcPointList(Vector2Int centerPoint, int radius)
		{
			List<Vector2Int> list = new List<Vector2Int>();

			Vector2Int left = new Vector2Int(centerPoint.x - radius, centerPoint.y);
			Vector2Int top = new Vector2Int(centerPoint.x, centerPoint.y + radius);
			Vector2Int right = new Vector2Int(centerPoint.x + radius, centerPoint.y);
			Vector2Int bottom = new Vector2Int(centerPoint.x, centerPoint.y - radius);

			Vector2Int a = left;
			list.Add(a);
			while (true)
			{
				a = GetArcFitPoint(centerPoint, a, radius, 1, 1);
				if (a.x > top.x || a.y > top.y)
					break;
				if (!list.Contains(a))
					list.Add(a);
			}

			a = top;
			list.Add(a);
			while (true)
			{
				a = GetArcFitPoint(centerPoint, a, radius, 1, -1);
				if (a.x > right.x || a.y < right.y)
					break;
				if (!list.Contains(a))
					list.Add(a);
			}

			a = right;
			list.Add(a);
			while (true)
			{
				a = GetArcFitPoint(centerPoint, a, radius, -1, -1);
				if (a.x < bottom.x || a.y < bottom.y)
					break;
				if (!list.Contains(a))
					list.Add(a);
			}

			a = bottom;
			list.Add(a);
			while (true)
			{
				a = GetArcFitPoint(centerPoint, a, radius, -1, 1);
				if (a.x < left.x || a.y > left.y)
					break;
				if (!list.Contains(a))
					list.Add(a);
			}

			return list;
		}

		private static Vector2Int GetArcFitPoint(Vector2Int centerPoint, Vector2Int basePoint, int radius, int dx, int dy)
		{
			Vector2Int p1 = new Vector2Int(basePoint.x + dx, basePoint.y);
			Vector2Int p2 = new Vector2Int(basePoint.x, basePoint.y + dy);
			Vector2Int p3 = new Vector2Int(basePoint.x + dx, basePoint.y + dy);

			double d1 = Vector2Int.Distance(centerPoint, p1);
			double d2 = Vector2Int.Distance(centerPoint, p2);
			double d3 = Vector2Int.Distance(centerPoint, p3);
			double dd1 = Math.Abs(d1 - radius);
			double dd2 = Math.Abs(d2 - radius);
			double dd3 = Math.Abs(d3 - radius);
			if (dd1 < dd2)
				return dd1 < dd3 ? p1 : p3;
			return dd2 < dd3 ? p2 : p3;
		}

		//获取以center_point为中心，半径为radius的圆弧形格子列表
		public static List<Vector2Int> GetArcPointList2(Vector2Int centerPoint, int radius)
		{
			List<Vector2Int> list = new List<Vector2Int>();

			Vector2Int left = new Vector2Int(centerPoint.x - radius, centerPoint.y);
			Vector2Int top = new Vector2Int(centerPoint.x, centerPoint.y + radius);
			Vector2Int right = new Vector2Int(centerPoint.x + radius, centerPoint.y);
			Vector2Int bottom = new Vector2Int(centerPoint.x, centerPoint.y - radius);

			Vector2Int a = left;
			list.Add(a);
			while (true)
			{
				a = GetArcFitPoint2(list, centerPoint, a, radius, 1, 1);
				if (a.x > top.x || a.y > top.y)
					break;
				if (!list.Contains(a))
					list.Add(a);
			}

			a = top;
			list.Add(a);
			while (true)
			{
				a = GetArcFitPoint2(list, centerPoint, a, radius, 1, -1);
				if (a.x > right.x || a.y < right.y)
					break;
				if (!list.Contains(a))
					list.Add(a);
			}

			a = right;
			list.Add(a);
			while (true)
			{
				a = GetArcFitPoint2(list, centerPoint, a, radius, -1, -1);
				if (a.x < bottom.x || a.y < bottom.y)
					break;
				if (!list.Contains(a))
					list.Add(a);
			}

			a = bottom;
			list.Add(a);
			while (true)
			{
				a = GetArcFitPoint2(list, centerPoint, a, radius, -1, 1);
				if (a.x < left.x || a.y > left.y)
					break;
				if (!list.Contains(a))
					list.Add(a);
			}

			return list;
		}

		public static Vector2Int GetArcFitPoint2(List<Vector2Int> list, Vector2Int centerPoint, Vector2Int basePoint,
		  int radius, int dx, int dy)
		{
			Vector2Int p1 = new Vector2Int(basePoint.x + dx, basePoint.y);
			Vector2Int p2 = new Vector2Int(basePoint.x, basePoint.y + dy);
			Vector2Int p3 = new Vector2Int(basePoint.x + dx, basePoint.y + dy);

			double d1 = Vector2Int.Distance(centerPoint, p1);
			double d2 = Vector2Int.Distance(centerPoint, p2);
			double d3 = Vector2Int.Distance(centerPoint, p3);
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
				return dd1 < dd3 ? p1 : p3;
			return dd2 < dd3 ? p2 : p3;
		}

		//列出两点间连线经过的所有格子
		public static List<Vector2Int> GetLinePointList(Vector2Int pointA, Vector2Int pointB)
		{
			List<Vector2Int> list = new List<Vector2Int>();
			if (pointA.Equals(pointB))
				list.Add(pointA);
			else if (pointA.x == pointB.x)
			{
				list.Add(pointA);
				int dv = pointA.y < pointB.y ? 1 : -1;
				for (int y = (pointA.y + dv); y * dv < pointB.y * dv; y += dv)
					list.Add(new Vector2Int(pointA.x, y));
				list.Add(pointB);
			}
			else if (pointA.y == pointB.y)
			{
				list.Add(pointA);
				int dv = pointA.x < pointB.x ? 1 : -1;
				for (int x = (pointA.x + dv); x * dv < pointB.x * dv; x += dv)
					list.Add(new Vector2Int(x, pointA.y));
				list.Add(pointB);
			}
			else
			{
				int x1 = pointA.x, y1 = pointA.y, x2 = pointB.x, y2 = pointB.y;
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
		public static Vector2Int GetExtendPoint(Vector2Int pointA, Vector2Int pointB, int distance)
		{
			int updx = pointB.x - pointA.x;
			int updy = pointB.y - pointA.y;
			if (updx == 0 && updy == 0) // p1==p2
				return new Vector2Int(pointA.x + distance, pointA.y + distance);
			if (updx == 0)
				return new Vector2Int(pointB.x, pointB.y + (updy > 0 ? distance : -distance));
			if (updy == 0)
				return new Vector2Int(pointB.x + (updx > 0 ? distance : -distance), pointB.y);
			int dx = Math.Abs(updx);
			int dy = Math.Abs(updy);
			if (dx > dy)
			{
				int x = pointB.x + (updx > 0 ? distance : -distance);
				int y = updy * (x - pointA.x) / updx + pointA.y;
				return new Vector2Int(x, y);
			}

			if (dx < dy)
			{
				int y = pointB.y + (updy > 0 ? distance : -distance);
				int x = updx * (y - pointA.y) / updy + pointA.x;
				return new Vector2Int(x, y);
			}

			return new Vector2Int(pointB.x + (updx > 0 ? distance : -distance),
				pointB.y + (updy > 0 ? distance : -distance));
		}

		//获得point点离线段point_a,point_b得最短距离
		public static int GetNearestDistance(Vector2Int pointA, Vector2Int pointB, Vector2Int point)
		{
			if (pointA.Equals(point) || pointB.Equals(point)) // 与a点或b点重合findRangeFreePoint
				return 0;
			int a = GetMapDistance(pointB, point);
			if (pointA.Equals(pointB)) // a点和b点重合
				return a;
			int b = GetMapDistance(pointA, point);
			int c = GetMapDistance(pointA, pointB);

			if (a * a >= b * b + c * c) // 如果p点与a点内夹角是钝角，则返回b
				return b;
			if (b * b >= a * a + c * c) // 如果p点与b点内夹角是钝角，则返回a
				return a;

			double l = (a + b + c) / 2d; // 周长的一半
			double s = Math.Sqrt(l * (l - a) * (l - b) * (l - c)); // 海伦公式求面积，也可以用矢量求
			return (int)(2 * s / c);
		}

		public static int GetMapDistance(Vector2Int pointA, Vector2Int pointB)
		{
			int dx = pointA.x - pointB.x;
			int dy = pointA.y - pointB.y;
			int ddx = dx - dy;
			int ddy = dx + dy;
			return (int)Math.Sqrt(0.8 * ddx * ddx + 0.2 * ddy * ddy);
		}



		public static bool IsInAround(Vector2Int pos, Vector2Int comparePos, int radius)
		{
			return AStarUtil.GetMapDistance(pos, comparePos) <= radius;
		}

		public static bool IsInSector(Vector2Int pos, Vector2Int sectorCenterPos, Vector2 sectorDir, int sectorRadius,
		  float sectorHalfDegrees)
		{
			int distance = GetMapDistance(pos, sectorCenterPos);
			if (distance > sectorRadius)
				return false;
			Vector2 v1 = sectorDir;
			Vector2 v2 = pos - sectorCenterPos;
			double degreeBetweenV1AndV2 = Math.Acos(Vector2.Dot(v1, v2) / (v1.magnitude * v2.magnitude)) * Mathf.Rad2Deg;
			return degreeBetweenV1AndV2 <= sectorHalfDegrees;
		}

	}
}