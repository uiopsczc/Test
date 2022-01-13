using System;

namespace CsCat
{
	public class FrameCallback
	{
		#region ctor

		public FrameCallback(Func<object, bool> callback, object arg)
		{
			callbackArg = arg;
			this.callback = callback;
		}

		#endregion

		#region public method

		public bool Execute()
		{
			return callback(callbackArg);
		}

		#endregion

		#region field

		public bool isCancel;


		private readonly object callbackArg;

		/// <summary>
		///   objcet参数,bool【true:下一帧继续执行该回调，false：删除该回调】
		/// </summary>
		private readonly Func<object, bool> callback;

		#endregion
	}
}