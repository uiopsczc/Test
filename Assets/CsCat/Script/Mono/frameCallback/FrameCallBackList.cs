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

    private List<FrameCallback> callback_list = new List<FrameCallback>();
    private readonly List<FrameCallback> executing_callback_list = new List<FrameCallback>();

    #endregion

    #region public method

    public void Execute()
    {
      if (callback_list.Count == 0)
        return;
      executing_callback_list.Swap(ref callback_list);
      foreach (var current_callback in executing_callback_list)
      {
        if (current_callback.is_cancel) continue;
        try
        {
          //下一帧要继续执行这个函数，所以要加到callbackList中
          var is_need_remain = current_callback.Execute();
          if (is_need_remain)
            callback_list.Add(current_callback);
        }
        catch (Exception ex)
        {
          LogCat.LogErrorFormat("{0}, UpdateFrame Error {1}", GetType().Name, ex);
        }
      }

      executing_callback_list.Clear();
    }

    public void Add(FrameCallback frameCallback)
    {
      callback_list.Add(frameCallback);
    }

    public void Add(Func<object, bool> callback, object callback_arg)
    {
      callback_list.Add(new FrameCallback(callback, callback_arg));
    }

    #endregion
  }
}