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
			PreReset();
			Reset();
			PostReset();
		}

		protected virtual void PreReset()
		{
			preResetCallback?.Invoke();
			preResetCallback = null;
		}

		protected void Reset()
		{
			ResetAllComponents();
			_OnReset_Enable();
			_OnReset_Pause();
			_OnReset_Update();
			_Reset();
		}

		protected virtual void _Reset()
		{
		}

		protected virtual void PostReset()
		{
			postResetCallback?.Invoke();
			postResetCallback = null;
		}

		public void ResetAllComponents()
		{
			for (int i = 0; i < componentPoolObjectIndexList.Count; i++)
			{
				var componentPoolObjectIndex = componentPoolObjectIndexList[i];
				var component = _GetComponent(componentPoolObjectIndex);
				component?.DoReset();
			}
		}

		void _OnDespawn_Reset()
		{
			preResetCallback = null;
			postResetCallback = null;
		}
	}
}