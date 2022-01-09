using UnityEngine;
using System;

namespace CsCat
{
	[Serializable]
	public partial class Polygon3D : Shape3D
	{
		#region ctor

		/// <summary>
		/// 默认顺时针方向,不需要加起始点，最后程序会自动加起始点
		/// </summary>
		/// <param name="localVertexes"></param>
		public Polygon3D(params Vector3[] localVertexes) : base(localVertexes)
		{
		}

		#endregion

		#region operator

		public static Polygon3D operator +(Polygon3D polygon, Vector3 vector)
		{
			Polygon3D clone = CloneUtil.CloneDeep(polygon);
			clone.AddWorldOffset(vector);
			return clone;
		}

		public static Polygon3D operator -(Polygon3D polygon, Vector3 vector)
		{
			Polygon3D clone = CloneUtil.CloneDeep(polygon);
			clone.AddWorldOffset(-vector);
			return clone;
		}

		#endregion
	}
}