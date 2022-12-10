using System;

namespace CsCat
{
	public partial class Component
	{
		private bool _isPaused;

		public bool isPaused => _isPaused;

		public void SetIsPaused(bool isPaused)
		{
			if (_isPaused == isPaused)
				return;
			_isPaused = isPaused;
			_SetIsPaused(isPaused);
		}

		protected virtual void _SetIsPaused(bool isPaused)
		{
		}

		void _Reset_Pause()
		{
			_isPaused = false;
		}

		void _Destroy_Pause()
		{
			_isPaused = false;
		}
	}
}