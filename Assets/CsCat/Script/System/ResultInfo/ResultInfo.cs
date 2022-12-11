using System;

namespace CsCat
{
	public class ResultInfo : IDespawn
	{
		private bool _isSuccess;
		private bool _isFail;
		private bool _isDone;

		public Action onSuccessCallback;
		public Action onFailCallback;
		public Action onDoneCallback;

		public ResultInfo()
		{
		}

		public ResultInfo(Action onSuccessCallback = null, Action onFailCallback = null,
			Action onDoneCallback = null)
		{
			Init(onSuccessCallback, onFailCallback, onDoneCallback);
		}

		public void Init(Action onSuccessCallback = null, Action onFailCallback = null,
			Action onDoneCallback = null)
		{
			this.onSuccessCallback = onSuccessCallback;
			this.onFailCallback = onFailCallback;
			this.onDoneCallback = onDoneCallback;
		}

		public bool isSuccess
		{
			get => _isSuccess;
			set
			{
				if (_isSuccess == value)
					return;
				_isSuccess = value;
				if (value)
				{
					_OnSuccess();
					isDone = true;
				}
			}
		}

		public bool isFail
		{
			get => _isFail;
			set
			{
				if (_isFail == value)
					return;
				_isFail = value;
				if (value)
				{
					_OnFail();
					isDone = true;
				}
			}
		}


		public bool isDone
		{
			get => _isDone;
			set
			{
				if (_isDone == value)
					return;
				_isDone = value;
				if (value)
					_OnDone();
			}
		}

		void _OnSuccess()
		{
			this.onSuccessCallback?.Invoke();
		}


		void _OnFail()
		{
			this.onFailCallback?.Invoke();
		}


		void _OnDone()
		{
			this.onDoneCallback?.Invoke();
		}

		public void Reset()
		{
			this._isSuccess = false;
			this._isFail = false;
			this._isDone = false;

			onSuccessCallback = null;
			onFailCallback = null;
			onDoneCallback = null;
		}


		public void Despawn()
		{
			Reset();
		}
	}
}