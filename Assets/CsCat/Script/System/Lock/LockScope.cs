using System;
using System.Diagnostics;
using System.Threading;

namespace CsCat
{
	public class LockScope : IDisposable
	{
		private object _lockObject;

		public bool isHasLock { get; private set; }

		public LockScope(object obj)
		{
			if (!Monitor.TryEnter(obj))
				return;

			this.isHasLock = true;
			this._lockObject = obj;
		}

		public void Dispose()
		{
			if (!this.isHasLock)
				return;

			Monitor.Exit(this._lockObject);
			this._lockObject = null;
			this.isHasLock = false;
		}
	}
}