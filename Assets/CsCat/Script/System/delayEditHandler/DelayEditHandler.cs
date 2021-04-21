using System;
using System.Collections.Generic;

namespace CsCat
{
  public class DelayEditHandler
  {
    private readonly object edit_target;
    private Action to_callback;

    public DelayEditHandler(object edit_target)
    {
      this.edit_target = edit_target;
    }

    public object this[object key]
    {
      set
      {
        ToSet(key, value);
      }
    }

    public void ToSet(object key, object value)
    {
      ToCallback(() => edit_target.SetPropertyValue("Item", value, new object[] { key }));
    }


    public void ToAdd(params object[] args)
    {
      ToCallback(() => edit_target.InvokeMethod("Add", true, args));
    }

    public void ToRemove(params object[] args)
    {
      ToCallback(() => edit_target.InvokeMethod("Remove", true, args));
    }

    public void ToRemoveAt(int to_remove_index)
    {
      ToCallback(() => edit_target.InvokeMethod("RemoveAt", true, to_remove_index));
    }

    public void ToRemoveAt_Stack(int to_remove_index)
    {
      ToCallback_Stack(() => edit_target.InvokeMethod("RemoveAt", true, to_remove_index));
    }

    public void ToCallback(Action to_callback)
    {
      this.to_callback += to_callback;
    }

    //后入先出
    public void ToCallback_Stack(Action to_callback)
    {
      this.to_callback += to_callback + this.to_callback;
    }


    public void Handle()
    {
      this.to_callback?.Invoke();
      this.to_callback = null;
    }
  }
}