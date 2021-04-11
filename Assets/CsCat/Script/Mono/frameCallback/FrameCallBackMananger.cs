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

    private readonly FrameCallbackList update_callbackList = new FrameCallbackList();
    private readonly FrameCallbackList lateUpdate_callbackList = new FrameCallbackList();
    private readonly FrameCallbackList fixedUpdate_callbackList = new FrameCallbackList();

    #endregion

    #region public method

    public void Init()
    {
    }

    public void AddFrameUpdateCallback(Func<object, bool> callback, object callback_arg)
    {
      update_callbackList.Add(callback, callback_arg);
    }

    public void AddFrameLateUpdateCallback(Func<object, bool> callback, object callback_arg)
    {
      lateUpdate_callbackList.Add(callback, callback_arg);
    }

    public void AddFrameFixedUpdateCallback(Func<object, bool> callback, object callback_arg)
    {
      fixedUpdate_callbackList.Add(callback, callback_arg);
    }

    #endregion

    #region private method

    public void Update()
    {
      update_callbackList.Execute();
    }

    public void LateUpdate()
    {
      lateUpdate_callbackList.Execute();
    }

    public void FixedUpdate()
    {
      fixedUpdate_callbackList.Execute();
    }

    #endregion
  }
}