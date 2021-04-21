using System;
using System.Diagnostics;

namespace CsCat
{
  public class AOPScope : IDisposable
  {
    private Action pre_callback;
    private Action post_callback;
    #region ctor


    public AOPScope(Action pre_callback, Action post_callback)
    {
      this.pre_callback = pre_callback;
      this.post_callback = post_callback;
      pre_callback?.Invoke();
    }

    #endregion

    #region public method

    public void Dispose()
    {
      post_callback?.Invoke();
    }

    #endregion
  }
}