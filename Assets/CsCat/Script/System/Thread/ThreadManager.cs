using System.Collections.Generic;
using System.Threading;

namespace CsCat
{
  public class ThreadManager : ISingleton
  {
    #region property

    public static ThreadManager instance => SingletonFactory.instance.Get<ThreadManager>();

    #endregion

    #region field

    public List<Thread> list = new List<Thread>();
    public Dictionary<string, Thread> dict = new Dictionary<string, Thread>();

    #endregion

    #region public method

    public void Abort()
    {
      foreach (var t in list)
        t.Abort();
      list.Clear();
      foreach (var t in dict.Values)
        t.Abort();
      dict.Clear();
    }

    public void Start(object args = null)
    {
      foreach (var t in list)
        if (args == null)
          t.Start();
        else
          t.Start(args);
      foreach (var t in dict.Values)
        if (args == null)
          t.Start();
        else
          t.Start(args);
    }

    public void Add(ParameterizedThreadStart thread_callback)
    {
      list.Add(new Thread(thread_callback));
    }

    public void Add(ThreadStart thread_callback)
    {
      list.Add(new Thread(thread_callback));
    }

    #endregion
  }
}