using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class CatmullRomSpline
	{
		public float length;
		public List<CatmullRomSegment> segmentList = new List<CatmullRomSegment>();
		private readonly CatmullRomPoint[] _orgPoints;
		private int segmentNum;

		public CatmullRomSpline(CatmullRomPoint[] orgPoints, int subSegmentNum)
		{
			segmentNum = orgPoints.Length - 1;
			//添加头尾顺延曲线方向的两个点
			var list = new List<CatmullRomPoint>(orgPoints);
			var first = new CatmullRomPoint(2 * orgPoints[0].position - orgPoints[1].position,
				orgPoints[0].createTime);
			var last = new CatmullRomPoint(
				2 * orgPoints[orgPoints.Length - 1].position - orgPoints[orgPoints.Length - 2].position,
				orgPoints[orgPoints.Length - 1].createTime);
			list.Insert(0, first);
			list.Add(last);
			this._orgPoints = list.ToArray();

			length = 0;
			for (var i = 1; i <= this._orgPoints.Length - 3; i++)
			{
				segmentList.Add(new CatmullRomSegment(this._orgPoints[i - 1], this._orgPoints[i], this._orgPoints[i + 1],
					this._orgPoints[i + 2], subSegmentNum));
				length += segmentList[segmentList.Count - 1].length;
			}
		}


		public CatmullRomPoint GetPointAtDistance(float distance)
		{
			distance = Mathf.Clamp(distance, 0, length);
			float curLength = 0;
			float lastLength = 0;
			for (var i = 0; i < segmentList.Count; i++)
			{
				var segment = segmentList[i];
				curLength += segment.length;
				if (curLength < distance)
				{
					lastLength = curLength;
					continue;
				}

				var subDistance = distance - lastLength;
				return segment.GetPointAtDistance(subDistance);
			}

			var last = segmentList[segmentList.Count - 1];
			return last.GetPointAtDistance(last.length);
		}

		public CatmullRomPoint GetTangentAtDistance(float distance)
		{
			distance = Mathf.Clamp(distance, 0, length);
			float curLength = 0;
			float lastLength = 0;
			for (var i = 0; i < segmentList.Count; i++)
			{
				var segment = segmentList[i];
				curLength += segment.length;
				if (curLength < distance)
				{
					lastLength = curLength;
					continue;
				}

				var subDistance = distance - lastLength;
				return segment.GetTangentAtDistance(subDistance);
			}

			var last = segmentList[segmentList.Count - 1];
			return last.GetTangentAtDistance(last.length);
		}

		public static Vector3 ComputeBinormal(Vector3 tangent, Vector3 normal)
		{
			return Vector3.Cross(tangent, normal).normalized;
		}
	}
}