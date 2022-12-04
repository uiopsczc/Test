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
			ResetAllChildren();
			_OnReset_Enable();
			_OnReset_Pause();
			_OnReset_Update();
			_OnReset_Refresh();
		}

		protected virtual void _PostReset()
		{
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