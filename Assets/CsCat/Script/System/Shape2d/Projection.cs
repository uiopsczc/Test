using UnityEngine;
using System.Collections.Generic;

namespace CsCat
{
	public class Projection
	{
		#region field

		public float min;
		public float max;

		#endregion

		#region ctor

		public Projection(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		#endregion

		#region static method

		public static bool Overlap(Projection p1, Projection p2)
		{
			return !(p1.min > p2.max) && !(p1.max < p2.min);
		}

		public static Projection GetProjection(Vector2 axis, List<Vector2> vertexList)
		{
			float min = Vector2.Dot(vertexList[0], axis);
			float max = min;

			for (int i = 1; i < vertexList.Count; i++)
			{
				Vector2 vertex = vertexList[i];
				float tmp = Vector2.Dot(vertex, axis);
				if (tmp > max)
					max = tmp;
				else if (tmp < min)
					min = tmp;
			}

			return new Projection(min, max);
		}

		#endregion
	}
}