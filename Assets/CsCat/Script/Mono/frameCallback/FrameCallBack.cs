using System;

namespace CsCat
{
  public class FrameCallback
  {
    #region ctor

    public FrameCallback(Func<object, bool> callback, object arg)
    {
      callback_arg = arg;
      this.callback = callback;
    }

    #endregion

    #region public method

    public bool Execute()
    {
      return callback(callback_arg);
    }

    #endregion

    #region field

    public bool is_cancel;


    private readonly object callback_arg;

    /// <summary>
    ///   objcet参数,bool【true:下一帧继续执行该回调，false：删除该回调】
    /// </summary>
    private readonly Func<object, bool> callback;

    #endregion
  }
}