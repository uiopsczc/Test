using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TreeNode
	{
		protected bool _isPaused;
		public bool isPaused => _isPaused;

		public virtual void SetIsPaused(bool isPaused)
		{
			if (_isPaused == isPaused)
				return;
			this._isPaused = isPaused;
			SetAllChildrenIsPaused(isPaused);
			_SetIsPaused(isPaused);
		}

		protected virtual void _SetIsPaused(bool isPaused)
		{
		}

		public void SetAllChildrenIsPaused(bool isPaused)
		{
			for (int i = 0; i < childPoolItemIndexList.Count; i++)
			{
				var childPoolItemIndex = childPoolItemIndexList[i];
				var child = _GetChild(childPoolItemIndex);
				child?.SetIsPaused(isPaused);
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