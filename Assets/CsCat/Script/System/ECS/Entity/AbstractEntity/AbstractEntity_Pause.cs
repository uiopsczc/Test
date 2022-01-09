using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class AbstractEntity
	{
		protected bool _isPaused;
		public bool is_paused => _isPaused;

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
			foreach (var child in ForeachChild())
				child.SetIsPaused(isPaused, true);
		}

		public void SetAllComponentsIsPaused(bool isPaused)
		{
			foreach (var component in ForeachComponent())
				component.SetIsPaused(isPaused);
		}

		void _OnDespawn_Pause()
		{
			_isPaused = false;
		}
	}
}