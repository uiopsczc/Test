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
				currentSubDirectHFSM?.SetIsPaused(isPaused, true);
				currentSubDirectState?.SetIsPaused(isPaused, true);
			}

			_SetIsPaused(isPaused);
		}
	}
}