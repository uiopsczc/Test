using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class AbstractEntity
	{
		protected bool _isPaused;
		public bool isPaused => _isPaused;

		public virtual void SetIsPaused(bool isPaused, bool isLoopChildren = false)
		{
			if (_isPaused == isPaused)
				return;
			this._isPaused = isPaused;
			if (isLoopChildren)
				SetAllChildrenIsPaused(isPaused);
			SetAllComponentsIsPaused(isPaused);
			_SetIsPaused(isPaused);
		}

		protected virtual void _SetIsPaused(bool isPaused)
		{
		}

		public void SetAllChildrenIsPaused(bool isPaused)
		{
			for (int i = 0; i < childKeyList.Count; i++)
			{
				var child = GetChild(childKeyList[i]);
				child?.SetIsPaused(isPaused, true);
			}
		}

		public void SetAllComponentsIsPaused(bool isPaused)
		{
			for (int i = 0; i < componentKeyList.Count; i++)
			{
				var component = GetComponent(componentKeyList[i]);
				component?.SetIsPaused(isPaused);
			}
		}

		void _OnReset_Pause()
		{
			_isPaused = false;
		}

		void _OnDespawn_Pause()
		{
			_isPaused = false;
		}
	}
}