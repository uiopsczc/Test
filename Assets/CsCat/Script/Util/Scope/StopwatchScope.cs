using System;
using System.Diagnostics;

namespace CsCat
{
  public class StopwatchScope : IDisposable
  {
    private readonly Stopwatch stopwatch;
    private string name;
    #region ctor

    public StopwatchScope(string name = "")
    {
      this.name = name;
      stopwatch = new Stopwatch();
      stopwatch.Start();
      LogCat.LogFormat("{0} 开始统计耗时", this.name);

    }

    #endregion

    #region public method

    public void Dispose()
    {
      stopwatch.Stop();
      var timeSpan = stopwatch.Elapsed;
      LogCat.LogFormat("{0} 统计耗时结束,总共耗时{1}秒", this.name, timeSpan.TotalMilliseconds / 1000);
    }

    #endregion
  }
}