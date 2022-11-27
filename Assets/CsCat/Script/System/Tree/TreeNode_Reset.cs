using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TreeNode
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
			ResetAllChildren();
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
			for (int i = 0; i < _childPoolItemIndexList.Count; i++)
			{
				var childPoolItemIndex = _childPoolItemIndexList[i];
				var child = _GetChild(childPoolItemIndex);
				child?.DoReset();
			}
		}

		void _OnDespawn_Reset()
		{
			preResetCallback = null;
			postResetCallback = null;
		}
	}
}