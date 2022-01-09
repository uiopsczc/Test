using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class AbstractEntity
	{
		private bool _isEnabled;

		public bool isEnabled => _isEnabled;

		public void SetIsEnabled(bool isEnabled, bool isLoopChildren = false)
		{
			if (_isEnabled == isEnabled)
				return;
			if (isLoopChildren)
			{
				foreach (var child in ForeachChild())
					child.SetIsEnabled(isEnabled);
			}

			foreach (var component in ForeachComponent())
				component.SetIsEnabled(isEnabled);
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

		void _OnDespawn_Enable()
		{
			_isEnabled = false;
		}
	}
}