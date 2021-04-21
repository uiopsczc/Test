using System;
using System.Diagnostics;
using System.Threading;

namespace CsCat
{
  public class LockScope : IDisposable
  {
    private object lock_object;

    public bool is_has_lock { get; private set; }

    public LockScope(object obj)
    {
      if (!Monitor.TryEnter(obj))
        return;

      this.is_has_lock = true;
      this.lock_object = obj;
    }

    public void Dispose()
    {
      if (!this.is_has_lock)
        return;

      Monitor.Exit(this.lock_object);
      this.lock_object = null;
      this.is_has_lock = false;
    }
  }
}