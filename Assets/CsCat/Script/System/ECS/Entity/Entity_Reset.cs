using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
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
			ResetAllComponents();
		}

		protected virtual void _PostReset()
		{
			
		}

		public void ResetAllComponents()
		{
			for (int i = 0; i < _componentPoolItemIndexList.Count; i++)
			{
				var componentPoolItemIndex = _componentPoolItemIndexList[i];
				var component = _GetComponent(componentPoolItemIndex);
				component?.DoReset();
			}
		}

		void _OnDespawn_Reset()
		{
		}
	}
}