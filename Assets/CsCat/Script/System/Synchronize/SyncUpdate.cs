using System;
using System.Collections.Generic;

namespace CsCat
{
  public class SyncUpdate
  {

    #region field

    private readonly List<Action> runnable_list = new List<Action>();
    private readonly object lock_obj = new object();

    #endregion

    #region public method

    public void Run(Action runnable)
    {
      if (runnable == null) return;
      lock (lock_obj)
      {
        runnable_list.Add(runnable);
      }
    }

    public void Update()
    {
      lock (lock_obj)
      {
        var count = runnable_list.Count;
        if (count > 0)
        {
          for (var i = 0; i < count; i++)
            runnable_list[i]();
          runnable_list.Clear();
        }
      }
    }

    #endregion
  }
}