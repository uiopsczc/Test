using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class GameEntity
	{
		protected override void _SetIsPaused(bool isPaused)
		{
			base._SetIsPaused(isPaused);
			//Corotinues无法Pause
			//SetPause_Coroutines(is_pause);
		}
	}
}