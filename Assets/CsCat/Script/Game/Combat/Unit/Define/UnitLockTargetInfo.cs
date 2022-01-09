using UnityEngine;

namespace CsCat
{
	public class UnitLockTargetInfo
	{
		public Unit lock_target_unit;
		public Vector3? lock_target_pos;

		public bool IsHasLockTarget()
		{
			return this.lock_target_unit != null || this.lock_target_pos != null;
		}

		public Vector3 GetLockTargetPosition()
		{
			if (this.lock_target_unit != null)
				return this.lock_target_unit.GetPosition();
			if (this.lock_target_pos != null)
				return this.lock_target_pos.Value;
			return default(Vector3);
		}

	}
}