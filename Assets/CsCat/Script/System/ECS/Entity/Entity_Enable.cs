using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class Entity
	{
		private bool _isEnabled;

		public bool isEnabled => _isEnabled;

		public void SetIsEnabled(bool isEnabled)
		{
			if (_isEnabled == isEnabled)
				return;

			for (int i = 0; i < _componentPoolItemIndexList.Count; i++)
			{
				var componentPoolItemIndex = _componentPoolItemIndexList[i];
				var component = _GetComponent(componentPoolItemIndex);
				component?.SetIsEnabled(isEnabled);
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