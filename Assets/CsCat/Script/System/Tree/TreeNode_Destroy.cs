using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TreeNode
	{
		private bool _isDestroyed;
		public Action preDestroyCallback;
		public Action postDestroyCallback;

		public bool IsDestroyed()
		{
			return _isDestroyed;
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
			SetIsEnabled(false);
			SetIsPaused(false);
			RemoveAllChildren();
			_Destroy();
			_isDestroyed = true;
			_cache.Clear();
		}

		protected virtual void _Destroy()
		{
		}

		protected void PostDestroy()
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