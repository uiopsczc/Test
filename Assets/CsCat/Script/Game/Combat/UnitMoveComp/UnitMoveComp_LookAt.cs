using UnityEngine;

namespace CsCat
{
	public partial class UnitMoveComp
	{
		//模式为lock时，可以占据LookAt， 其他地方调用LookAt将不起作用，除非mode为force强行LookAt
		//在不需占据LookAt时，需传入unlock解锁
		// 暂时没用
		private bool _LookAt(string mode)
		{
			this._unitLookAtInfo.isRotateXArrived = false;
			this._unitLookAtInfo.isRotateYArrived = false;
			if (mode.Equals("stopLookAt"))
			{
				this._unitLookAtInfo.lookAtUnit = null;
				this._unitLookAtInfo.lookAtDir = null;
				return false;
			}

			if (mode.Equals("unlock"))
			{
				this._unitLookAtInfo.isLocked = false;
				return false;
			}

			if (!mode.Equals("force") && this._unitLookAtInfo.isLocked)
				return false;
			this._unitLookAtInfo.mode = mode.IsNullOrWhiteSpace() ? "idle" : mode;
			if (mode.Equals("lock"))
				this._unitLookAtInfo.isLocked = true;
			return true;
		}


		public void LookAt(Unit unit, string mode)
		{
			if (_LookAt(mode) == false)
				return;
			this._unitLookAtInfo.lookAtUnit = unit;
			this._unitLookAtInfo.lookAtDir = null;
		}

		public void LookAt(Vector3 dir, string mode)
		{
			if (_LookAt(mode) == false)
				return;
			this._unitLookAtInfo.lookAtUnit = null;
			this._unitLookAtInfo.lookAtDir = dir;
		}
	}
}