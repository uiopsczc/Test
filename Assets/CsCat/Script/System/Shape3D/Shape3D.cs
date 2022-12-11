using System;
using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
	[Serializable]
	public partial class Shape3D
	{
		#region field

		public List<Vector3> localVertexList = new List<Vector3>();
		protected Matrix4x4 _matrix = Matrix4x4.identity;

		/// <summary>
		/// worldOffset不会跟随matrix进行位移和旋转或缩放
		/// </summary>
		public Vector3 worldOffset = Vector3.zero;

		#endregion

		#region property

		public List<Vector3> vertexeList
		{
			get
			{
				List<Vector3> result = new List<Vector3>(localVertexList.Count);
				for (var i = 0; i < localVertexList.Count; i++)
				{
					Vector3 v = localVertexList[i];
					result.Add(ToWorldSpace(v));
				}

				return result;
			}
		}

		public virtual Vector3 center => ToWorldSpace(Vector3.zero);

		public Vector3 CenterOfAllPoints()
		{
			Vector3 sum = Vector3.zero;
			for (var i = 0; i < vertexeList.Count; i++)
			{
				var v = vertexeList[i];
				sum += v;
			}

			return sum / vertexeList.Count;
		}

		public List<Line3D> lineList
		{
			get
			{
				List<Line3D> result = new List<Line3D>(vertexeList.Count + 1);
				for (int i = 0; i < vertexeList.Count - 1; i++)
					result.Add(new Line3D(vertexeList[i], vertexeList[i + 1]));
				if (vertexeList.Count > 2)
					result.Add(new Line3D(vertexeList[vertexeList.Count - 1], vertexeList[0]));
				return result;
			}
		}

		#endregion


		protected Shape3D(params Vector3[] localVertexes)
		{
			localVertexList.AddRange(localVertexes);
		}


		#region public method

		public override bool Equals(object obj)
		{
			if (!(obj is Shape3D other))
				return false;
			if (other.vertexeList.Count != vertexeList.Count)
				return false;
			if (!other.worldOffset.Equals(worldOffset))
				return false;
			for (int i = 0; i < vertexeList.Count; i++)
			{
				if (vertexeList[i].Equals(other.vertexeList[i]))
					return false;
			}

			return true;
		}

		public override int GetHashCode()
		{
			List<Vector3> list = new List<Vector3>(vertexeList) { worldOffset };
			return ObjectUtil.GetHashCode(list.ToArray());
		}

		public virtual List<KeyValuePair<Vector3, Vector3>> GetDrawLineList()
		{
			List<KeyValuePair<Vector3, Vector3>> result =
				new List<KeyValuePair<Vector3, Vector3>>(vertexeList.Count + 1);
			for (int i = 0; i < vertexeList.Count - 1; i++)
				result.Add(new KeyValuePair<Vector3, Vector3>(vertexeList[i], vertexeList[i + 1]));
			result.Add(new KeyValuePair<Vector3, Vector3>(vertexeList[vertexeList.Count - 1], vertexeList[0]));

			return result;
		}


		public Shape3D AddWorldOffset(Vector3 addWorldOffset)
		{
			this.worldOffset += addWorldOffset;
			return this;
		}

		public T AddWorldOffset<T>(Vector3 addWorldOffset) where T : Shape3D
		{
			return (T)AddWorldOffset(addWorldOffset);
		}


		public Shape3D MultiplyMatrix(Matrix4x4 matrix)
		{
			this._matrix *= matrix;
			return this;
		}

		public T MultiplyMatrix<T>(Matrix4x4 matrix) where T : Shape3D
		{
			return (T)MultiplyMatrix(matrix);
		}

		public Shape3D PreMultiplyMatrix(Matrix4x4 matrix)
		{
			this._matrix = matrix * this._matrix;
			return this;
		}

		public T PreMultiplyMatrix<T>(Matrix4x4 matrix) where T : Shape3D
		{
			return (T)PreMultiplyMatrix(matrix);
		}


		public void Reverse()
		{
			this.localVertexList.Reverse();
		}


		public Vector3 ToLocalSpace(Vector3 p)
		{
			return _matrix.inverse.MultiplyPoint(p - worldOffset);
		}


		public Vector3 ToWorldSpace(Vector3 p)
		{
			return _matrix.MultiplyPoint(p) + worldOffset;
		}


		public Vector3? GetNext(Vector3 cur)
		{
			int curIndex = this.vertexeList.IndexOf(cur);
			if (curIndex == -1)
				return null;
			return curIndex == vertexeList.Count - 1 ? vertexeList[0] : vertexeList[curIndex + 1];
		}

		public Vector3? GetPre(Vector3 cur)
		{
			int curIndex = this.vertexeList.IndexOf(cur);
			if (curIndex == -1)
				return null;
			return curIndex == 0 ? vertexeList[vertexeList.Count - 1] : vertexeList[curIndex - 1];
		}

		#endregion
	}
}