using System;

namespace CsCat
{
	public partial class Component
	{
		public Action preResetCallback;
		public Action postResetCallback;

		public void DoReset()
		{
			PreReset();
			Reset();
			PostReset();
		}

		protected virtual void PreReset()
		{
			preResetCallback?.Invoke();
			preResetCallback = null;
		}

		protected void Reset()
		{
			_OnReset_Enable();
			_OnReset_Pause();
			_OnReset_Update();
			_Reset();
		}

		protected virtual void _Reset()
		{
		}

		protected virtual void PostReset()
		{
			postResetCallback?.Invoke();
			postResetCallback = null;
		}


		void _OnDespawn_Reset()
		{
			preResetCallback = null;
			postResetCallback = null;
		}
	}
}