using System;

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
			ResetAllComponents();
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

		public void ResetAllComponents()
		{
			for (int i = 0; i < _componentPoolItemIndexList.Count; i++)
			{
				var componentPoolItemIndex = _componentPoolItemIndexList[i];
				var component = _GetComponent(componentPoolItemIndex);
				component?.DoReset();
			}
		}

		public void _Destroy_Reset()
		{
			preResetCallback = null;
			postResetCallback = null;
		}
	}
}