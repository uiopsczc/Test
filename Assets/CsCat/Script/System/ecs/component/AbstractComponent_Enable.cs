using System;

namespace CsCat
{
	public partial class AbstractComponent
	{
		private bool _isEnabled;

		public bool isEnabled => _isEnabled;


		public void SetIsEnabled(bool isEnabled)
		{
			if (_isEnabled == isEnabled)
				return;
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