using System;
using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
	[Serializable]
	public partial class Shape2D
	{
		#region field

		public List<Vector2> localVertexList = new List<Vector2>();
		public Matrix4x4 matrix = Matrix4x4.identity;

		/// <summary>
		/// worldOffset不会跟随martix进行位移和旋转或缩放
		/// </summary>
		protected Vector2 worldOffset = Vector2.zero;

		#endregion

		#region property

		public List<Vector2> vertexList
		{
			get
			{
				List<Vector2> result = new List<Vector2>(localVertexList.Count);
				for (var i = 0; i < localVertexList.Count; i++)
				{
					Vector2 v = localVertexList[i];
					result.Add(ToWorldSpace(v));
				}

				return result;
			}
		}

		public virtual Vector2 center => ToWorldSpace(Vector2.zero);

		public Vector2 CenterOfAllPoints()
		{
			Vector2 sum = Vector2.zero;
			for (var i = 0; i < vertexList.Count; i++)
			{
				var v = vertexList[i];
				sum += v;
			}

			return sum / vertexList.Count;
		}

		public List<Line> lineList
		{
			get
			{
				List<Line> result = new List<Line>();
				for (int i = 0; i < vertexList.Count - 1; i++)
					result.Add(new Line(vertexList[i], vertexList[i + 1]));
				if (vertexList.Count > 2)
					result.Add(new Line(vertexList[vertexList.Count - 1], vertexList[0]));
				return result;
			}
		}

		#endregion


		protected Shape2D(params Vector2[] localVertexes)
		{
			localVertexList.AddRange(localVertexes);
		}

		#region public method

		public virtual bool IsIntersect(Rectangle rectangle2)
		{
			return false;
		}

		public virtual bool IsIntersect(Circle circle)
		{
			return false;
		}

		public override bool Equals(object obj)
		{
			Shape2D other = obj as Shape2D;
			if (other == null)
				return false;
			if (other.vertexList.Count != vertexList.Count)
				return false;
			if (!other.worldOffset.Equals(worldOffset))
				return false;
			for (int i = 0; i < vertexList.Count; i++)
			{
				if (vertexList[i].Equals(other.vertexList[i]))
					return false;
			}

			return true;
		}

		public override int GetHashCode()
		{
			List<Vector2> list = new List<Vector2>(vertexList) { worldOffset };
			return ObjectUtil.GetHashCode(list.ToArray());
		}

		public virtual List<KeyValuePair<Vector2, Vector2>> GetDrawLineList()
		{
			List<KeyValuePair<Vector2, Vector2>> result = new List<KeyValuePair<Vector2, Vector2>>();
			for (int i = 0; i < vertexList.Count - 1; i++)
				result.Add(new KeyValuePair<Vector2, Vector2>(vertexList[i], vertexList[i + 1]));
			result.Add(new KeyValuePair<Vector2, Vector2>(vertexList[vertexList.Count - 1], vertexList[0]));

			return result;
		}


		public Shape2D AddWorldOffset(Vector2 addWorldOffset)
		{
			this.worldOffset += addWorldOffset;
			return this;
		}

		public T AddWorldOffset<T>(Vector2 addWorldOffset) where T : Shape2D
		{
			return (T)AddWorldOffset(addWorldOffset);
		}

		public Shape2D MultiplyMatrix(Matrix4x4 matrix)
		{
			this.matrix *= matrix;
			return this;
		}

		public T MultiplyMatrix<T>(Matrix4x4 matrix) where T : Shape2D
		{
			return (T)MultiplyMatrix(matrix);
		}


		public Shape2D PreMultiplyMatrix(Matrix4x4 matrix)
		{
			this.matrix = matrix * this.matrix;
			return this;
		}

		public T PreMultiplyMatrix<T>(Matrix4x4 matrix) where T : Shape2D
		{
			return (T)PreMultiplyMatrix(matrix);
		}


		public void Reverse()
		{
			this.localVertexList.Reverse();
		}


		public Vector2 ToLocalSpace(Vector2 p)
		{
			return matrix.inverse.MultiplyPoint3x4(p - worldOffset);
		}


		public Vector2 ToWorldSpace(Vector2 p)
		{
			return matrix.MultiplyPoint3x4(p).ToVector2() + worldOffset;
		}


		public Vector2? GetNext(Vector2 cur)
		{
			int curIndex = this.vertexList.IndexOf(cur);
			if (curIndex == -1)
				return null;
			return curIndex == vertexList.Count - 1 ? vertexList[0] : vertexList[curIndex + 1];
		}

		public Vector2? GetPre(Vector2 cur)
		{
			int curIndex = this.vertexList.IndexOf(cur);
			if (curIndex == -1)
				return null;
			return curIndex == 0 ? vertexList[vertexList.Count - 1] : vertexList[curIndex - 1];
		}

		#endregion
	}
}