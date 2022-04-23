using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class AbstractEntity
	{
		public Action preResetCallback;
		public Action postResetCallback;

		public void DoReset(bool isLoopChildren = false)
		{
			PreReset();
			Reset(isLoopChildren);
			PostReset();
		}

		protected virtual void PreReset()
		{
			preResetCallback?.Invoke();
			preResetCallback = null;
		}

		protected void Reset(bool isLoopChildren = false)
		{
			if (isLoopChildren)
				ResetAllChildren();
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

		public void ResetAllChildren()
		{
			for (int i = 0; i < childKeyList.Count; i++)
			{
				var child = GetChild(childKeyList[i]);
				child?.Reset(true);
			}
		}

		public void ResetAllComponents()
		{
			for (int i = 0; i < componentKeyList.Count; i++)
			{
				var component = GetComponent(componentKeyList[i]);
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