using System;
using UnityEngine;

namespace CsCat
{
	public class BoxBase
	{
		public virtual bool IsIntersect(BoxBase other, float tolerence = float.Epsilon)
		{
			return false;
		}

		public virtual void DebugDraw(Vector3 offset, Color color)
		{
		}
	}
}