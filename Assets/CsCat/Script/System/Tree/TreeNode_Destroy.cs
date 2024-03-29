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
			preDestroyCallback?.Invoke();
			preDestroyCallback = null;
			_PreDestroy();
			RemoveAllChildren();
			_Destroy();
			_PostDestroy();
			postDestroyCallback?.Invoke();
			postDestroyCallback = null;
		}

		protected virtual void _PreDestroy()
		{
		}

		protected virtual void _Destroy()
		{
			SetIsEnabled(false);
			SetIsPaused(false);
			_isDestroyed = true;
			_cache.Clear();
		}

		protected virtual void _PostDestroy()
		{
			
		}

		

		void _Despawn_Destroy()
		{
			_isDestroyed = false;
			preDestroyCallback = null;
			postDestroyCallback = null;
		}
	}
}