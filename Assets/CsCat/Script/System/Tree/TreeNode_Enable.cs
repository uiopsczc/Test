using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TreeNode
	{
		private bool _isEnabled;

		public bool isEnabled => _isEnabled;

		public void SetIsEnabled(bool isEnabled)
		{
			if (_isEnabled == isEnabled)
				return;

			for (int i = 0; i < childPoolItemIndexList.Count; i++)
			{
				var childPoolItemIndex = childPoolItemIndexList[i];
				var child = _GetChild(childPoolItemIndex);
				child?.SetIsEnabled(isEnabled);
			}
			_isEnabled = isEnabled;
			_SetIsEnabled(isEnabled);
			if (isEnabled)
				OnEnable();
			else
				OnDisable();
		}

		protected virtual void _SetIsEnabled(bool isEnabled)
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		void _OnReset_Enable()
		{
			_isEnabled = false;
		}

		void _OnDespawn_Enable()
		{
			_isEnabled = false;
		}
	}
}