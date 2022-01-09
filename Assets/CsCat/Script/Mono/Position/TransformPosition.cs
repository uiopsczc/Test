using UnityEngine;

namespace CsCat
{
	public class TransformPosition : IPosition
	{
		public Transform transform;
		public string socket_name;

		public TransformPosition(Transform transform, string socket_name = null)
		{
			this.transform = transform;
			this.socket_name = socket_name;
		}

		public Vector3 GetPosition()
		{
			return GetTransform().position;
		}

		public Transform GetTransform()
		{
			return !socket_name.IsNullOrWhiteSpace() ? this.transform.GetSocketTransform(socket_name) : this.transform;
		}

		public void SetSocketName(string socket_name)
		{
			this.socket_name = socket_name;
		}

		public bool IsValid()
		{
			return this.GetTransform() != null;
		}
	}
}