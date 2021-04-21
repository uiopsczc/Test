using System;
using System.Diagnostics;

namespace CsCat
{
  public class DelayEditHandlerScope : IDisposable
  {
    private DelayEditHandler delayEditHandler;
    #region ctor

    public DelayEditHandlerScope(object edit_target)
    {
      delayEditHandler = new DelayEditHandler(edit_target);
    }
    #endregion

    public object this[object key]
    {
      set { delayEditHandler[key] = value; }
    }

    public void ToAdd(params object[] args)
    {
      delayEditHandler.ToAdd(args);
    }

    public void ToRemove(params object[] args)
    {
      delayEditHandler.ToRemove(args);
    }

    public void ToRemoveAt(int to_remove_index)
    {
      delayEditHandler.ToRemoveAt(to_remove_index);
    }

    public void ToRemoveAt_Stack(int to_remove_index)
    {
      delayEditHandler.ToRemoveAt_Stack(to_remove_index);
    }

    //后入先出
    public void ToCallback_Stack(Action to_callback)
    {
      delayEditHandler.ToCallback_Stack(to_callback);
    }

    public void ToSet(object key, object value)
    {
      delayEditHandler.ToSet(key, value);
    }

    public void ToCallback(Action to_callback)
    {
      delayEditHandler.ToCallback(to_callback);
    }

    #region public method

    public void Dispose()
    {
      delayEditHandler.Handle();
    }

    #endregion
  }
}