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
			_Reset_Pause();
			_Reset_Enable();
			_Reset_Destroy();
			_Reset_Update();
			_Reset_();
		}

		protected virtual void _PostReset()
		{
		}


		void _Destroy_Reset()
		{
			preResetCallback = null;
			postResetCallback = null;
		}
	}
}