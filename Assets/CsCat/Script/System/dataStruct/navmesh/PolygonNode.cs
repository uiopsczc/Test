using UnityEngine;

namespace CsCat
{
	public class PolygonNode
	{
		/// <summary>
		/// 坐标点
		/// </summary>
		public Vector2 vertex;

		/// <summary>
		/// 是否是交点
		/// </summary>
		public bool isIntersectPoint;

		/// <summary>
		/// 是否已处理过
		/// </summary>
		public bool isProcessed = false;

		/// <summary>
		/// 进点--false； 出点--true
		/// </summary>
		public bool isOutPoint = false;

		/// <summary>
		/// 交点的双向引用
		/// </summary>
		public PolygonNode other;

		/// <summary>
		/// 点是否在主多边形中
		/// </summary>
		public bool isSubject;

		/// <summary>
		/// 多边形的下一个点
		/// </summary>
		public PolygonNode next;

		/// <summary>
		/// 用于合并时，表示这个顶点是否处理过
		/// </summary>
		public bool isChecked = false;


		public PolygonNode(Vector2 point, bool isIntersectPoint, bool isSubject)
		{
			this.vertex = point;
			this.isIntersectPoint = isIntersectPoint;
			this.isSubject = isSubject;
		}


		public override string ToString()
		{
			return string.Format("{0}/n交点：{1}/n出点：{2}/n主：{3}/n处理：{4}", vertex, isIntersectPoint, isOutPoint,
				isSubject,
				isProcessed);
		}
	}
}