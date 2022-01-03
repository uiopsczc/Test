using UnityEngine;
using System.Collections.Generic;
using System;

namespace CsCat
{
	[Serializable]
	public partial class Polygon : Shape2D
	{
		#region field

		/// <summary>
		/// 是否是顺时针
		/// </summary>
		private bool isClockwise = true;

		#endregion

		#region property

		/// <summary>
		/// 矩形包围盒
		/// </summary>
		public Rectangle boxRectangle
		{
			get
			{
				if (vertexList == null || vertexList.Count < 0)
					return null;

				float lx = vertexList[0].x;
				float rx = vertexList[0].x;
				float ty = vertexList[0].y;
				float by = vertexList[0].y;

				for (int i = 1; i < vertexList.Count; i++)
				{
					var v = vertexList[i];
					if (v.x < lx)
						lx = v.x;

					if (v.x > rx)
						rx = v.x;

					if (v.y < by)
						by = v.y;

					if (v.y > ty)
						ty = v.y;
				}

				Vector2 center = new Vector2((lx + rx) / 2, (ty + by) / 2);
				float length = Math.Abs(rx - lx);
				float width = Math.Abs(ty - by);
				return new Rectangle(center, new Vector2(length, width));
			}
		}

		#endregion


		#region ctor

		/// <summary>
		/// 默认顺时针方向,不需要加起始点，最后程序会自动加起始点
		/// </summary>
		/// <param name="local_vertexes"></param>
		public Polygon(params Vector2[] local_vertexes) : base(local_vertexes)
		{
		}

		#endregion

		#region operator

		public static Polygon operator +(Polygon polygon, Vector2 vector)
		{
			Polygon clone = CloneUtil.CloneDeep(polygon);
			clone.AddWorldOffset(vector);
			return clone;
		}

		public static Polygon operator -(Polygon polygon, Vector2 vector)
		{
			Polygon clone = CloneUtil.CloneDeep(polygon);
			clone.AddWorldOffset(-vector);
			return clone;
		}

		#endregion


		#region public method

		#region 多边形和点的关系

		/// <summary>
		/// 点是否在多边形内部
		/// http://www.cnblogs.com/zhangshu/archive/2011/08/08/2130694.html
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public virtual bool Contains(Vector2 point)
		{
			int count = 0;
			//minValue+100000,因为再减去一个数，会越界
			Line L = new Line(new Vector2(float.MinValue + 100000, point.y), point);

			for (var i = 0; i < this.lineList.Count; i++)
			{
				Line S = this.lineList[i];
				if (S.ClassifyPoint(point) == PointClassification.OnSegment)
					return true;

				if (!S.GetDirection().EqualsEPSILON(Vector2.right) &&
					!S.GetDirection().EqualsEPSILON(-Vector2.right))
				{
					bool aOnL = L.ClassifyPoint(S.pointA) == PointClassification.OnSegment;
					bool bOnL = L.ClassifyPoint(S.pointB) == PointClassification.OnSegment;
					if (aOnL || bOnL)
					{
						if (aOnL && S.pointA.y > S.pointB.y) count++;
						if (bOnL && S.pointB.y > S.pointA.y) count++;
					}
					else
					{
						Line.LineClassification lineRelation = S.Intersection(L, out _);
						if (lineRelation == Line.LineClassification.SegmentsIntersect)
							count++;
					}
				}
			}

			return count % 2 != 0;
		}

		#endregion

		#region 该多边形离指定点最近的点

		public Vector2? GetClosestValidPoint(Vector2 p2, List<Line> constraintLineList,
			List<Vector2> constraintPointList,
			bool isAddConstraintLinePoints = false)
		{
			List<Vector2> closestPointList = new List<Vector2>();
			for (var i = 0; i < this.lineList.Count; i++)
			{
				Line line = this.lineList[i];
				var closestPoint = line.GetClosestPoint(p2);
				closestPointList.Add(closestPoint);
			}

			//是否将限制线段的端点加进去进行最近点计算判断
			if (isAddConstraintLinePoints)
			{
				for (var i = 0; i < constraintLineList.Count; i++)
				{
					Line constraintLine = constraintLineList[i];
					closestPointList.Add(constraintLine.pointA);
					closestPointList.Add(constraintLine.pointB);
				}
			}

			//排序
			closestPointList.Sort(delegate (Vector2 v1, Vector2 v2)
				{
					float distanceSqr1 = (p2 - v1).sqrMagnitude;
					float distanceSqr2 = (p2 - v2).sqrMagnitude;
					return distanceSqr1 < distanceSqr2 ? -1 : distanceSqr1 == distanceSqr2 ? 0 : 1;
				}
			);

			if (constraintLineList == null || constraintLineList.Count == 0)
				return closestPointList[0];

			while (closestPointList.Count != 0)
			{
				Vector2 cur = closestPointList.RemoveFirst();
				bool isGotoNext = false;
				for (var i = 0; i < constraintLineList.Count; i++)
				{
					Line constraintLine = constraintLineList[i];
					if (constraintPointList.Contains(cur))
					{
						isGotoNext = true;
						break;
					}

					if (constraintLine.pointA == cur || constraintLine.pointB == cur)
						return cur;

					if (constraintLine.Contains(cur))
					{
						isGotoNext = true;
						break;
					}
				}

				if (isGotoNext)
					continue;
				return cur;
			}

			LogCat.LogError("did not find closest point");
			return null;
		}

		#endregion

		#region 线段和多边形的关系

		/// <summary>
		/// 线段是否在多边形内,整个线段都在多边形内，只有一部分在多边形内的算作在多边形外
		/// http://www.cnblogs.com/zhangshu/archive/2011/08/08/2130694.html
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public virtual bool Contains(Line line)
		{
			Line PQ = line;
			//bool a = this.Contains(PQ.PointA);
			//bool b = this.Contains(PQ.PointB);
			if (!this.Contains(PQ.pointA) || !this.Contains(PQ.pointB))
				return false;
			List<Vector2> pointSet = new List<Vector2>();
			for (var i = 0; i < lineList.Count; i++)
			{
				Line S = lineList[i];
				bool aOnS = S.ClassifyPoint(PQ.pointA) == PointClassification.OnSegment;
				bool bOnS = S.ClassifyPoint(PQ.pointB) == PointClassification.OnSegment;
				bool aOnPQ = PQ.ClassifyPoint(S.pointA) == PointClassification.OnSegment;
				bool bOnPQ = PQ.ClassifyPoint(S.pointB) == PointClassification.OnSegment;
				if (aOnS || bOnS)
				{
					if (aOnS)
						pointSet.Add(PQ.pointA);
					if (bOnS)
						pointSet.Add(PQ.pointB);
				}
				else if (aOnPQ || bOnPQ)
				{
					if (aOnPQ)
						pointSet.Add(S.pointA);
					if (bOnPQ)
						pointSet.Add(S.pointB);
				}
				else
				{
					if (PQ.Intersection(S, out _) == Line.LineClassification.SegmentsIntersect)
						return false;
				}
			}

			//排序,先比较x，如果相同再比较y
			pointSet.Sort((v1, v2) => v1.x < v2.x ? -1 : v1.x == v2.x && v1.y < v2.y ? -1 : v1 == v2 ? 0 : 1);

			for (int i = 1; i < pointSet.Count; i++)
			{
				Vector2 v1 = pointSet[i - 1];
				Vector2 v2 = pointSet[i];
				Vector2 v = (v1 + v2) / 2;
				if (!Contains(v))
					return false;
			}

			return true;
		}

		#endregion

		#region 和另一个多边形的关系

		public Classification ClassifyPolygon(Polygon other)
		{
			for (var i = 0; i < other.lineList.Count; i++)
			{
				Line otherLine = other.lineList[i];
				for (var j = 0; j < lineList.Count; j++)
				{
					Line thisLine = lineList[j];
					if (thisLine.Intersection(otherLine, out _) !=
						Line.LineClassification.SegmentsNotIntersect) //相交

						return Classification.Intersect;
				}
			}

			bool isContain = true;
			for (var i = 0; i < other.vertexList.Count; i++)
			{
				Vector2 otherPoint = other.vertexList[i];
				if (!this.Contains(otherPoint))
				{
					isContain = false;
					break;
				}
			}

			if (isContain)
				return Classification.Contain;

			bool isContained = true;
			for (var i = 0; i < this.vertexList.Count; i++)
			{
				Vector2 thisPoint = this.vertexList[i];
				if (!other.Contains(thisPoint))
				{
					isContained = false;
					break;
				}
			}

			return isContained ? Classification.Contained : Classification.Isolate;
		}

		#endregion

		/// <summary>
		/// 设置为顶点方向;true:顺时针方向,false:逆时针方向
		/// </summary>
		/// <param name="value"></param>
		public void SetClockWise(bool value)
		{
			if (this.isClockwise != value)
				Reverse();
			this.isClockwise = value;
		}

		public bool GetClockWise()
		{
			return this.isClockwise;
		}

		/// <summary>
		/// 将同一条线段上的多个顶点转化为该线段的两个端点，从而减少不必要的顶点
		/// </summary>
		public void Systemize()
		{
			List<Vector2> ret = new List<Vector2>();
			Vector2 last_dir = Vector2.zero;
			for (int i = 0; i < lineList.Count; i++)
			{
				Line line = lineList[i];
				Vector2 cur_dir = line.GetDirection();
				if (cur_dir == Vector2.zero)
				{
					if (i == 0)
						ret.Add(line.localVertexList[0]);
				}
				else
				{
					if (cur_dir.EqualsEPSILON(last_dir))
						continue;
					else
					{
						last_dir = cur_dir;
						ret.Add(line.localVertexList[0]);
					}
				}
			}

			this.localVertexList = ret;
		}

		#endregion
	}
}