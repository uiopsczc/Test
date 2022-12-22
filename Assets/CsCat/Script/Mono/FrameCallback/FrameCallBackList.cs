using System;
using System.Collections.Generic;

namespace CsCat
{
	/// <summary>
	///   采用双缓冲策略
	/// </summary>
	public class FrameCallbackList
	{
		#region field

		private List<FrameCallback> _callbackList = new List<FrameCallback>();
		private readonly List<FrameCallback> _executingCallbackList = new List<FrameCallback>();

		#endregion

		#region public method

		public void Execute()
		{
			if (_callbackList.Count == 0)
				return;
			_executingCallbackList.Swap(ref _callbackList);
			for (var i = 0; i < _executingCallbackList.Count; i++)
			{
				var currentCallback = _executingCallbackList[i];
				if (currentCallback.isCancel) continue;
				try
				{
					//下一帧要继续执行这个函数，所以要加到callbackList中
					var isNeedRemain = currentCallback.Execute();
					if (isNeedRemain)
						_callbackList.Add(currentCallback);
				}
				catch (Exception ex)
				{
					LogCat.LogErrorFormat("{0}, UpdateFrame Error {1}", GetType().Name, ex);
				}
			}

			_executingCallbackList.Clear();
		}

		public void Add(FrameCallback frameCallback)
		{
			_callbackList.Add(frameCallback);
		}

		public void Add(Func<object, bool> callback, object callback_arg)
		{
			_callbackList.Add(new FrameCallback(callback, callback_arg));
		}

		#endregion
	}
}