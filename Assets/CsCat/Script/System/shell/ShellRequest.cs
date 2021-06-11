using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CsCat
{
  public class ShellRequest
  {
    public event Action<LogCatType, string> on_log_action;
    public event Action on_error_action;
    public event Action on_done_action;

    public void Log(LogCatType logType, string log)
    {
      on_log_action?.Invoke(logType, log);

      if (logType == LogCatType.Error)
        LogCat.LogError(log);
    }

    public void NotifyDone()
    {
      on_done_action?.Invoke();
    }

    public void Error()
    {
      on_error_action?.Invoke();
    }

    public TaskAwaiter GetAwaiter()
    {
      var tcs = new TaskCompletionSource<object>();
      on_done_action += () => { tcs.SetResult(true); };
      return ((Task) tcs.Task).GetAwaiter();
    }
  }
}