using UnityEngine;

namespace CsCat
{
	public partial class UnitMoveComp
	{
		//模式为lock时，可以占据LookAt， 其他地方调用LookAt将不起作用，除非mode为force强行LookAt
		//在不需占据LookAt时，需传入unlock解锁
		// 暂时没用
		private bool __LookAt(string mode)
		{
			this.unitLookAtInfo.isRotateXArrived = false;
			this.unitLookAtInfo.isRotateYArrived = false;
			if (mode.Equals("stop_look_at"))
			{
				this.unitLookAtInfo.lookAtUnit = null;
				this.unitLookAtInfo.lookAtDir = null;
				return false;
			}

			if (mode.Equals("unlock"))
			{
				this.unitLookAtInfo.isLocked = false;
				return false;
			}

			if (!mode.Equals("force") && this.unitLookAtInfo.isLocked)
				return false;
			this.unitLookAtInfo.mode = mode.IsNullOrWhiteSpace() ? "idle" : mode;
			if (mode.Equals("lock"))
				this.unitLookAtInfo.isLocked = true;
			return true;
		}


		public void LookAt(Unit unit, string mode)
		{
			if (__LookAt(mode) == false)
				return;
			this.unitLookAtInfo.lookAtUnit = unit;
			this.unitLookAtInfo.lookAtDir = null;
		}

		public void LookAt(Vector3 dir, string mode)
		{
			if (__LookAt(mode) == false)
				return;
			this.unitLookAtInfo.lookAtUnit = null;
			this.unitLookAtInfo.lookAtDir = dir;
		}
	}
}