using System;

namespace CsCat
{
	public class AOPScope : IDisposable
	{
		private Action _preCallback;
		private Action _postCallback;

		public AOPScope(Action preCallback, Action postCallback)
		{
			this._preCallback = preCallback;
			this._postCallback = postCallback;
			preCallback?.Invoke();
		}


		public void Dispose()
		{
			_postCallback?.Invoke();
		}
	}
}