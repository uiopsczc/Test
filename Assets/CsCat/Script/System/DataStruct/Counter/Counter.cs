using System;

namespace CsCat
{
	public class Counter
	{
		private int _count = 0;
		private Action _changeValueCallback;

		public int count => _count;

		public void Increase()
		{
			this._count = this._count + 1;
			this._CheckCallback();
		}

		public void Decrease()
		{
			this._count = this._count - 1;
			this._CheckCallback();
		}

		public void Reset()
		{
			this._count = 0;
			this._changeValueCallback = null;
		}


		public void AddChangeValueCallback(Action callback)
		{
			this._changeValueCallback += callback;
		}

		private void _CheckCallback()
		{
			this._changeValueCallback?.Invoke();
		}
	}

}