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
				for (int i = 0; i < childKeyList.Count; i++)
				{
					var child = GetChild(childKeyList[i]);
					child?.SetIsEnabled(isEnabled);
				}
			}

			for (int i = 0; i < componentKeyList.Count; i++)
			{
				var component = GetComponent(componentKeyList[i]);
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