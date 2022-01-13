using System;
using UnityEngine;

namespace CsCat
{
	public class ColliderCat
	{
		public ColliderType colliderType;
		public BoxBase box;

		public bool IsIntersect(ColliderCat otherCollider, float tolerence = float.Epsilon)
		{
			return box.IsIntersect(otherCollider.box, tolerence);
		}

		public void DebugDraw(Vector3 offset)
		{
			box.DebugDraw(offset, ColliderConst.ColliderInfoDict[colliderType].color);
		}
	}
}