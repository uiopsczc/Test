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
	public class FrameCallbackManager
	{
		#region field

		private readonly FrameCallbackList _updateCallbackList = new FrameCallbackList();
		private readonly FrameCallbackList _lateUpdateCallbackList = new FrameCallbackList();
		private readonly FrameCallbackList _fixedUpdateCallbackList = new FrameCallbackList();

		#endregion

		#region public method

		public void Init()
		{
		}

		public void AddFrameUpdateCallback(Func<object, bool> callback, object callbackArg)
		{
			_updateCallbackList.Add(callback, callbackArg);
		}

		public void AddFrameLateUpdateCallback(Func<object, bool> callback, object callbackArg)
		{
			_lateUpdateCallbackList.Add(callback, callbackArg);
		}

		public void AddFrameFixedUpdateCallback(Func<object, bool> callback, object callbackArg)
		{
			_fixedUpdateCallbackList.Add(callback, callbackArg);
		}

		#endregion


		public void Update()
		{
			_updateCallbackList.Execute();
		}

		public void LateUpdate()
		{
			_lateUpdateCallbackList.Execute();
		}

		public void FixedUpdate()
		{
			_fixedUpdateCallbackList.Execute();
		}

	}
}