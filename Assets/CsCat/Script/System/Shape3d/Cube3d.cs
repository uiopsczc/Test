using System;
using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
	[Serializable]
	public class Cube3D : Shape3D
	{
		#region field

		public Vector3 center;
		public Vector3 size;

		#endregion

		#region property

		//顶部的点
		public Vector3 rightTopForward => ToWorldSpace(localRightTopForward);

		public Vector3 rightTopBack => ToWorldSpace(localRightTopBack);

		public Vector3 leftTopBack => ToWorldSpace(localLeftTopBack);

		public Vector3 leftTopForward => ToWorldSpace(localLeftTopForward);


		//底部的点
		public Vector3 rightBottomForward => ToWorldSpace(localRightBottomForward);

		public Vector3 rightBottomBack => ToWorldSpace(localRightBottomBack);

		public Vector3 leftBottomBack => ToWorldSpace(localLeftBottomBack);

		public Vector3 leftBottomForward => ToWorldSpace(localLeftBottomForward);

		//顶部的点
		public Vector3 localRightTopForward => localVertexList[0];

		public Vector3 localRightTopBack => localVertexList[1];

		public Vector3 localLeftTopBack => localVertexList[2];

		public Vector3 localLeftTopForward => localVertexList[3];


		//底部的点
		public Vector3 localRightBottomForward => localVertexList[4];

		public Vector3 localRightBottomBack => localVertexList[5];

		public Vector3 localLeftBottomBack => localVertexList[6];

		public Vector3 localLeftBottomForward => localVertexList[7];

		#endregion

		#region ctor

		public Cube3D(Vector3 center, Vector3 size)
		{
			MultiplyMatrix(Matrix4x4.Translate(center));
			this.size = size;

			//顶部的点
			Vector3 localRightTopForward = size * 0.5f;
			Vector3 localRightTopBack = new Vector3(size.x, size.y, -size.z) * 0.5f;
			Vector3 localLeftTopBack = new Vector3(-size.x, size.y, -size.z) * 0.5f;
			Vector3 localLeftTopForward = new Vector3(-size.x, size.y, size.z) * 0.5f;


			//底部的点
			Vector3 localRightBottomForward = new Vector3(size.x, -size.y, size.z) * 0.5f;
			Vector3 localRightBottomBack = new Vector3(size.x, -size.y, -size.z) * 0.5f;
			Vector3 localLeftBottomBack = -size * 0.5f;
			Vector3 localLeftBottomForward = new Vector3(-size.x, -size.y, size.z) * 0.5f;


			localVertexList.Add(localRightTopForward);
			localVertexList.Add(localRightTopBack);
			localVertexList.Add(localLeftTopBack);
			localVertexList.Add(localLeftTopForward);

			localVertexList.Add(localRightBottomForward);
			localVertexList.Add(localRightBottomBack);
			localVertexList.Add(localLeftBottomBack);
			localVertexList.Add(localLeftBottomForward);
		}

		#endregion

		#region  operator

		public static Cube3D operator +(Cube3D cube, Vector3 vector)
		{
			Cube3D clone = CloneUtil.CloneDeep(cube);
			clone.AddWorldOffset(vector);
			return clone;
		}

		public static Cube3D operator -(Cube3D cube, Vector3 vector)
		{
			Cube3D clone = CloneUtil.CloneDeep(cube);
			clone.AddWorldOffset(-vector);
			return clone;
		}

		#endregion

		public override List<KeyValuePair<Vector3, Vector3>> GetDrawLineList()
		{
			List<KeyValuePair<Vector3, Vector3>> result = new List<KeyValuePair<Vector3, Vector3>>
			{
                //顶部面
                new KeyValuePair<Vector3, Vector3>(rightTopForward, rightTopBack),
				new KeyValuePair<Vector3, Vector3>(rightTopBack, leftTopBack),
				new KeyValuePair<Vector3, Vector3>(leftTopBack, leftTopForward),
				new KeyValuePair<Vector3, Vector3>(leftTopForward, rightTopForward),
                //底部面
                new KeyValuePair<Vector3, Vector3>(rightBottomForward, rightBottomBack),
				new KeyValuePair<Vector3, Vector3>(rightBottomBack, leftBottomBack),
				new KeyValuePair<Vector3, Vector3>(leftBottomBack, leftBottomForward),
				new KeyValuePair<Vector3, Vector3>(leftBottomForward, rightBottomForward),
                //其余四条线
                new KeyValuePair<Vector3, Vector3>(rightTopForward, rightBottomForward),
				new KeyValuePair<Vector3, Vector3>(rightTopBack, rightBottomBack),
				new KeyValuePair<Vector3, Vector3>(leftTopForward, leftBottomForward),
				new KeyValuePair<Vector3, Vector3>(leftTopBack, leftBottomBack)
			};


			//顶部面


			//底部面


			//其余四条线

			return result;
		}
	}
}