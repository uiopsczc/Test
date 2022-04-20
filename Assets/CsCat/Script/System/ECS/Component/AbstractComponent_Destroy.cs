using System;

namespace CsCat
{
	public partial class AbstractComponent
	{
		private bool _isDestroyed;
		public Action preDestroyCallback;
		public Action postDestroyCallback;

		public bool IsDestroyed()
		{
			return this._isDestroyed;
		}

		public void _PreDestroy()
		{
			preDestroyCallback?.Invoke();
		}

		public void Destroy()
		{
			if (IsDestroyed())
				return;
			SetIsEnabled(false);
			SetIsPaused(false);
			_PreDestroy();
			_Destroy();
			_PostDestroy();
			_isDestroyed = true;
			cache.Clear();
		}

		protected  virtual  void _PostDestroy()
		{
			postDestroyCallback?.Invoke();
		}

		protected virtual void _Destroy()
		{
		}


		public void OnDespawn()
		{
			_OnDespawn_();
			_OnDespawn_Destroy();
			_OnDespawn_Enable();
			_OnDespawn_Pause();
			_OnDespawn_Reset();
		}

		void _OnDespawn_Destroy()
		{
			_isDestroyed = false;
			preDestroyCallback = null;
			postDestroyCallback = null;
		}
	}
}