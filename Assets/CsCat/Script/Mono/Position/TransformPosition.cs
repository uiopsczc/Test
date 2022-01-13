using UnityEngine;

namespace CsCat
{
	public class TransformPosition : IPosition
	{
		public Transform transform;
		public string socketName;

		public TransformPosition(Transform transform, string socketName = null)
		{
			this.transform = transform;
			this.socketName = socketName;
		}

		public Vector3 GetPosition()
		{
			return GetTransform().position;
		}

		public Transform GetTransform()
		{
			return !socketName.IsNullOrWhiteSpace() ? this.transform.GetSocketTransform(socketName) : this.transform;
		}

		public void SetSocketName(string socketName)
		{
			this.socketName = socketName;
		}

		public bool IsValid()
		{
			return this.GetTransform() != null;
		}
	}
}