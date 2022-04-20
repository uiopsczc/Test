using System;

namespace CsCat
{
	public partial class AbstractComponent
	{
		public Action preResetCallback;
		public Action postResetCallback;

		protected  virtual  void _PreReset()
		{
			preResetCallback?.Invoke();
		}


		public void Reset()
		{
			_PreReset();
			_Reset();
			_PostReset();
		}

		protected virtual void _Reset()
		{
		}

		protected virtual void _PostReset()
		{
			postResetCallback?.Invoke();
			this.postResetCallback = null;
		}


		void _OnDespawn_Reset()
		{
			preResetCallback = null;
			postResetCallback = null;
		}
	}
}