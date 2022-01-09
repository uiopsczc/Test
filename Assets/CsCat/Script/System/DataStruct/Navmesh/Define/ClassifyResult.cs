using UnityEngine;

namespace CsCat
{
	public class ClassifyResult
	{
		/// <summary>
		///   直线与cell（三角形）的关系
		/// </summary>
		public PathResult result = PathResult.NO_RELATIONSHIP;

		/// <summary>
		///   相交边的索引
		/// </summary>
		public int side = 0;

		/// <summary>
		///   下一个邻接cell的索引
		/// </summary>
		public int cellIndex = -1;

		/// <summary>
		///   交点
		/// </summary>
		public Vector2 intersection = new Vector2();


		public override string ToString()
		{
			return result + StringConst.String_Space + cellIndex;
		}
	}
}