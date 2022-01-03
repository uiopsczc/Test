using UnityEngine;
using System;

namespace CsCat
{
	/// <summary>
	///  VertexeList;//定点顺时针为:(leftBottom->leftTop->rightTop->rightBottom),逆时针为:(rightBottom->rightTop->leftTop->leftBottom)
	///  顶点点列表,外边界用按顺时针，内边界用逆时针，区域在方向右侧
	///  默认Matrix4x4.identity是向上
	/// </summary>
	[Serializable]
	public class Rectangle3D : Polygon3D
	{
		#region field

		protected Vector2 _size;

		#endregion

		#region property

		public Vector3 localLeftBottom => localVertexList[0];

		public Vector3 localLeftTop => localVertexList[1];

		public Vector3 localRightTop => localVertexList[2];

		public Vector3 localRightBottom => localVertexList[3];


		public Vector2 size => _size;


		public Vector3 leftBottom => ToWorldSpace(localVertexList[0]);

		public Vector3 leftTop => ToWorldSpace(localVertexList[1]);

		public Vector3 rightTop => ToWorldSpace(localVertexList[2]);

		public Vector3 rightBottom => ToWorldSpace(localVertexList[3]);

		#endregion

		#region ctor

		/// <summary>
		/// 默认Matrix4x4.identity是向上
		/// </summary>
		/// <param name="center"></param>
		/// <param name="size"></param>
		public Rectangle3D(Vector3 center, Vector2 size) : this(center, size, Matrix4x4.identity)
		{
		}

		/// <summary>
		/// leftBottom->leftTop->rightTop->rightBottom
		/// 默认Matrix4x4.identity是向上
		/// </summary>
		/// <param name="center"></param>
		/// <param name="length"></param>
		/// <param name="width"></param>
		/// <param name="angle"></param>
		public Rectangle3D(Vector3 center, Vector2 size, Matrix4x4 matrix)
		{
			MultiplyMatrix(Matrix4x4.Translate(center));
			MultiplyMatrix(matrix);
			this._size = size;


			Vector3 localLeftBottom = new Vector3(-this.size.x / 2, 0, -this.size.y / 2);
			Vector3 localLeftTop = new Vector3(-this.size.x / 2, 0, this.size.y / 2);
			Vector3 localRightTop = new Vector3(this.size.x / 2, 0, this.size.y / 2);
			Vector3 localRightBottom = new Vector3(this.size.x / 2, 0, -this.size.y / 2);


			localVertexList.Add(localLeftBottom);
			localVertexList.Add(localLeftTop);
			localVertexList.Add(localRightTop);
			localVertexList.Add(localRightBottom);
		}

		#endregion

		#region operator

		public static Rectangle3D operator +(Rectangle3D rectangle, Vector3 vector)
		{
			Rectangle3D clone = CloneUtil.CloneDeep(rectangle);
			clone.AddWorldOffset(vector);
			return clone;
		}

		public static Rectangle3D operator -(Rectangle3D rectangle, Vector3 vector)
		{
			Rectangle3D clone = CloneUtil.CloneDeep(rectangle);
			clone.AddWorldOffset(-vector);
			return clone;
		}

		#endregion
	}
}