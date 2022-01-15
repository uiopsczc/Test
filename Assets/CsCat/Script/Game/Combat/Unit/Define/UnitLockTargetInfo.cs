using UnityEngine;

namespace CsCat
{
	public class UnitLockTargetInfo
	{
		public Unit lockTargetUnit;
		public Vector3? lockTargetPos;

		public bool IsHasLockTarget()
		{
			return this.lockTargetUnit != null || this.lockTargetPos != null;
		}

		public Vector3 GetLockTargetPosition()
		{
			if (this.lockTargetUnit != null)
				return this.lockTargetUnit.GetPosition();
			if (this.lockTargetPos != null)
				return this.lockTargetPos.Value;
			return default(Vector3);
		}

	}
}