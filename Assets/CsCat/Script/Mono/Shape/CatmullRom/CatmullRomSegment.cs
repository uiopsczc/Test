using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class Knot
	{
		public Vector3 position;
		public Vector3 tangent;
		public float createdTime;


		public Knot(Vector3 position, Vector3 tangent, float createdTime)
		{
			this.position = position;
			this.tangent = tangent;
			this.createdTime = createdTime;
		}
	}

	public class Segment
	{
		public Knot end;
		public float length;
		public Knot start;

		public Segment(Knot start, Knot end)
		{
			this.start = start;
			this.end = end;
			length = (this.end.position - this.start.position).magnitude;
		}

		public Knot GetKnot(float lerp)
		{
			var position = Vector3.Lerp(start.position, end.position, lerp);
			var tangent = Vector3.Lerp(start.tangent, end.tangent, lerp).normalized;
			var createTime = Mathf.Lerp(start.createdTime, end.createdTime, lerp);
			return new Knot(position, tangent, createTime);
		}
	}

	public class CatmullRomSegment
	{
		public float length;
		private readonly CatmullRomPoint p0;
		private readonly CatmullRomPoint p1;
		private readonly CatmullRomPoint p2;
		private readonly CatmullRomPoint p3;
		public int subSegmentNum;

		public List<Segment> subSegmentList = new List<Segment>();

		public CatmullRomSegment(CatmullRomPoint p0, CatmullRomPoint p1, CatmullRomPoint p2, CatmullRomPoint p3,
		  int subSegmentNum)
		{
			this.p0 = p0;
			this.p1 = p1;
			this.p2 = p2;
			this.p3 = p3;
			this.subSegmentNum = subSegmentNum;

			length = 0;
			for (float i = 0; i < subSegmentNum; i++)
			{
				var point0 = GetPoint(i / subSegmentNum);
				var tangent0 = GetTangent(i / subSegmentNum);
				var createTime0 = Mathf.Lerp(p2.createTime, p3.createTime, i / subSegmentNum);
				var point1 = GetPoint((i + 1) / subSegmentNum);
				var tangent1 = GetTangent((i + 1) / subSegmentNum);
				var createTime1 = Mathf.Lerp(p2.createTime, p3.createTime, (i + 1) / subSegmentNum);
				var segment = new Segment(new Knot(point0, tangent0, createTime0), new Knot(point1, tangent1, createTime1));
				subSegmentList.Add(segment);
				length += segment.length;
			}
		}


		public Vector3 GetPoint(float t)
		{
			var point = new Vector3();

			var t2 = t * t;
			var t3 = t2 * t;

			point = 0.5f * (2.0f * p1.position +
						  (-p0.position + p2.position) * t +
						  (2.0f * p0.position - 5.0f * p1.position + 4 * p2.position - p3.position) * t2 +
						  (-p0.position + 3.0f * p1.position - 3.0f * p2.position + p3.position) * t3);
			return point;
		}

		public Vector3 GetTangent(float t)
		{
			var point = new Vector3();

			var t2 = t * t;

			point = 0.5f * (-p0.position + p2.position) +
				  (2.0f * p0.position - 5.0f * p1.position + 4 * p2.position - p3.position) * t +
				  (-p0.position + 3.0f * p1.position - 3.0f * p2.position + p3.position) * t2 * 1.5f;
			point.Normalize();
			return point;
			;
		}

		public Knot GetKnotAtDistance(float distance)
		{
			float curDistance = 0;
			Segment target = null;
			float lerp = 0;

			for (var i = 0; i < subSegmentList.Count; i++)
			{
				var subSegment = subSegmentList[i];
				curDistance += subSegment.length;
				if (curDistance >= distance)
				{
					lerp = (subSegment.length - (curDistance - distance)) / subSegment.length;
					target = subSegment;
					break;
				}
			}

			if (distance + 0.001f >= length)
			{
				target = subSegmentList[subSegmentList.Count - 1];
				lerp = 1;
			}
			else if (distance <= 0)
			{
				target = subSegmentList[0];
				lerp = 0;
			}

			//LogCat.LogWarning(subSegments.Count);
			//if (target == null)
			//{
			//    LogCat.LogWarning(subSegments.Count);
			//    LogCat.LogWarning(curDistance);
			//    LogCat.LogWarning(distance);
			//}
			return target.GetKnot(lerp);
			;
		}

		public CatmullRomPoint GetPointAtDistance(float distance)
		{
			var knot = GetKnotAtDistance(distance);
			return new CatmullRomPoint(knot.position, knot.createdTime);
		}


		public CatmullRomPoint GetTangentAtDistance(float distance)
		{
			var knot = GetKnotAtDistance(distance);
			return new CatmullRomPoint(knot.tangent, knot.createdTime);
		}
	}
}