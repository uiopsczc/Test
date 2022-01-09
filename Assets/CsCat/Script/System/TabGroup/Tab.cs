using System;

namespace CsCat
{
	public class Tab
	{
		public Action onSelectCallback;
		public Action onUnSelectCallback;
		private bool _isSelected;

		public bool isSelected => _isSelected;

		public Tab(Action onSelectCallback, Action onUnSelectCallback)
		{
			this.onSelectCallback = onSelectCallback;
			this.onUnSelectCallback = onUnSelectCallback;
		}

		public void Select()
		{
			_isSelected = true;
			onSelectCallback?.Invoke();
		}

		public void UnSelect()
		{
			_isSelected = false;
			onUnSelectCallback?.Invoke();
		}
	}
}