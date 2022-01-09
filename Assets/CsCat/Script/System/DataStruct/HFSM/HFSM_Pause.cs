using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class HFSM
	{
		public override void SetIsPaused(bool isPaused, bool isLoopChildren = false)
		{
			if (_isPaused == isPaused)
				return;
			this._isPaused = isPaused;
			if (isLoopChildren)
			{
				current_sub_direct_hfsm?.SetIsPaused(isPaused, true);
				current_sub_direct_state?.SetIsPaused(isPaused, true);
			}
			_SetIsPaused(isPaused);
		}
	}
}