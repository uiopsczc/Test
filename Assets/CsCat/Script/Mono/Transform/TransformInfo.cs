using UnityEngine;

namespace CsCat
{
	public class TransformInfo
	{
		public Vector3 position;
		public Vector3 eulerAngles;
		public Quaternion rotation;
		public Vector3 scale;

		public TransformInfo(Vector3 position, Vector3 eulerAngles, Vector3 scale)
		{
			this.position = position;
			this.eulerAngles = eulerAngles;
			this.rotation = Quaternion.Euler(this.eulerAngles.x, this.eulerAngles.y, this.eulerAngles.z);
			this.scale = scale;
		}

		public TransformInfo(Vector3 position, Quaternion rotation, Vector3 scale)
		{
			this.position = position;
			this.eulerAngles = rotation.eulerAngles;
			this.rotation = rotation;
			this.scale = scale;
		}




	}
}




