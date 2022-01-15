using UnityEngine;

namespace CsCat
{
	public class UnitLookAtInfo
	{
		public bool isRotateXArrived;
		public bool isRotateYArrived;
		public Unit lookAtUnit;
		public Vector3? lookAtDir;
		public string mode = "idle";
		public bool isLocked;


		public bool HasLookAt()
		{
			return IsLookAtDir() || IsLookAtUnit();
		}

		public bool IsLookAtDir()
		{
			return this.lookAtDir != null;
		}

		public bool IsLookAtUnit()
		{
			return this.lookAtUnit != null;
		}
	}
}