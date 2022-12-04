using System;

namespace CsCat
{
	public partial class Component
	{
		public Action preResetCallback;
		public Action postResetCallback;

		public void DoReset()
		{
			preResetCallback?.Invoke();
			preResetCallback = null;
			_PreReset();
			_Reset();
			_PostReset();
			postResetCallback?.Invoke();
			postResetCallback = null;
		}

		protected virtual void _PreReset()
		{
			
		}

		protected virtual void _Reset()
		{
			_OnReset_Enable();
			_OnReset_Pause();
			_OnReset_Update();
		}

		protected virtual void _PostReset()
		{
		}


		void _OnDespawn_Reset()
		{
			preResetCallback = null;
			postResetCallback = null;
		}
	}
}