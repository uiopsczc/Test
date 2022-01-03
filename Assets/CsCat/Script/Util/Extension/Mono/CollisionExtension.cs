using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public static class CollisionExtension
	{
		public static ContactPoint[] GetContactPoints(this Collision self)
		{
			List<ContactPoint> list = new List<ContactPoint>();
			for (int i = 0; i < self.contactCount; i++)
				list.Add(self.GetContact(i));

			return list.IsNullOrEmpty() ? null : list.ToArray();
		}

		public static Collider[] GetContactThisColliders(this Collision self)
		{
			List<Collider> list = new List<Collider>();
			for (int i = 0; i < self.contactCount; i++)
				list.Add(self.GetContact(i).thisCollider);

			return list.IsNullOrEmpty() ? null : list.ToArray();
		}

		public static ContactPoint GetContactPoint(this Collision self, Collider collider)
		{
			for (int i = 0; i < self.contactCount; i++)
				if (self.GetContact(i).thisCollider == collider)
					return self.GetContact(i);

			return default;
		}
	}
}