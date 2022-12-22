using System;

namespace CsCat
{
	public class FrameCallback
	{
		#region ctor

		public FrameCallback(Func<object, bool> callback, object arg)
		{
			_callbackArg = arg;
			this._callback = callback;
		}

		#endregion

		#region public method

		public bool Execute()
		{
			return _callback(_callbackArg);
		}

		#endregion

		#region field

		public bool isCancel;


		private readonly object _callbackArg;

		/// <summary>
		///   objcet参数,bool【true:下一帧继续执行该回调，false：删除该回调】
		/// </summary>
		private readonly Func<object, bool> _callback;

		#endregion
	}
}