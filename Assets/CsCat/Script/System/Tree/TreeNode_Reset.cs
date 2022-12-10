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
			ResetAllChildren();
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
			_Reset_Enable();
			_Reset_Pause();
			_Reset_Update();
			_Reset_Refresh();
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

		void _Despawn_Reset()
		{
		}
	}
}