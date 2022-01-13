using System;

namespace CsCat
{
	/// <summary>
	///   用于每一帧的update，lateUpdate的回调
	///   Func
	///   <object, bool>
	///     callback
	///     callbackArg是传入到callback中参数
	///     如果返回false表示下一帧不会再执行该callback
	/// </summary>
	public class FrameCallbackMananger
	{
		#region field

		private readonly FrameCallbackList updateCallbackList = new FrameCallbackList();
		private readonly FrameCallbackList lateUpdateCallbackList = new FrameCallbackList();
		private readonly FrameCallbackList fixedUpdateCallbackList = new FrameCallbackList();

		#endregion

		#region public method

		public void Init()
		{
		}

		public void AddFrameUpdateCallback(Func<object, bool> callback, object callbackArg)
		{
			updateCallbackList.Add(callback, callbackArg);
		}

		public void AddFrameLateUpdateCallback(Func<object, bool> callback, object callbackArg)
		{
			lateUpdateCallbackList.Add(callback, callbackArg);
		}

		public void AddFrameFixedUpdateCallback(Func<object, bool> callback, object callbackArg)
		{
			fixedUpdateCallbackList.Add(callback, callbackArg);
		}

		#endregion

		#region private method

		public void Update()
		{
			updateCallbackList.Execute();
		}

		public void LateUpdate()
		{
			lateUpdateCallbackList.Execute();
		}

		public void FixedUpdate()
		{
			fixedUpdateCallbackList.Execute();
		}

		#endregion
	}
}