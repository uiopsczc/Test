using System;

namespace CsCat
{
	public class Pause : ISingleton
	{
		/// <summary>
		///   每次pause会加一，每次unPause会减一，当为pauseCount的时候会Resume
		/// </summary>
		private int _pauseCount;

		public void SingleInit()
		{
		}


		private void _AddPauseCount(int cnt)
		{
			var org = isPaused;
			_pauseCount += cnt;
			var cur = isPaused;
			if (org != cur)
			{
				if (isPaused)
					onPause?.Invoke();
				else
					onUnPause?.Invoke();
			}
		}


		public static Pause instance = SingletonFactory.instance.Get<Pause>();
		public bool isPaused => _pauseCount > 0;


		public Action onPause;
		public Action onUnPause;


		public void SetPause()
		{
			_AddPauseCount(1);
		}

		public void SetUnPause()
		{
			_AddPauseCount(-1);
		}


		public void Reset()
		{
			var org = isPaused;
			_pauseCount = 0;
			var cur = isPaused;

			if (org != cur)
				onUnPause?.Invoke();
		}
	}
}