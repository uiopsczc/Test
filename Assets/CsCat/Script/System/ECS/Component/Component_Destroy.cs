using System;

namespace CsCat
{
	public partial class Component
	{
		private bool _isDestroyed;
		public Action preDestroyCallback;
		public Action postDestroyCallback;

		public bool IsDestroyed()
		{
			return this._isDestroyed;
		}

		public void DoDestroy()
		{
			if (IsDestroyed())
				return;
			PreDestroy();
			Destroy();
			PostDestroy();
		}

		protected void PreDestroy()
		{
			preDestroyCallback?.Invoke();
			preDestroyCallback = null;
		}

		protected void Destroy()
		{
			if (IsDestroyed())
				return;
			SetIsEnabled(false);
			SetIsPaused(false);
			_Destroy();
			_isDestroyed = true;
			cache.Clear();
		}

		protected virtual void _Destroy()
		{
		}

		protected  virtual  void PostDestroy()
		{
			postDestroyCallback?.Invoke();
			postDestroyCallback = null;
		}
		

		void _OnDespawn_Destroy()
		{
			_isDestroyed = false;
			preDestroyCallback = null;
			postDestroyCallback = null;
		}
	}
}