using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
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
			RemoveAllComponents();
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
			_Destroy_Pause();
			_Destroy_Enable();
			_Destroy_Reset();
			_Destroy_Update();
			_Destroy_();
			_isDestroyed = true;
		}

		protected virtual void _PostDestroy()
		{
			
		}

		public void _Reset_Destroy()
		{
			preDestroyCallback = null;
			postDestroyCallback = null;
		}


		void _Despawn_Destroy()
		{
			_isDestroyed = false;
		}
	}
}