using UnityEngine;

namespace CsCat
{
	public class Vector3Position : IPosition
	{
		public Vector3 vector3;

		public Vector3Position(Vector3 vector3)
		{
			this.vector3 = vector3;
		}

		public Vector3 GetPosition()
		{
			return vector3;
		}

		public Transform GetTransform()
		{
			return null;
		}

		public void SetSocketName(string socketName)
		{
		}

		public bool IsValid()
		{
			return true;
		}
	}
}