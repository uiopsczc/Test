using System;
using System.Diagnostics;

namespace CsCat
{
	public class StopwatchScope : IDisposable
	{
		private readonly Stopwatch _stopwatch;
		private string _name;

		public StopwatchScope(string name = StringConst.String_Empty)
		{
			this._name = name;
			_stopwatch = new Stopwatch();
			_stopwatch.Start();
			LogCat.LogFormat("{0} 开始统计耗时", this._name);
		}


		public void Dispose()
		{
			_stopwatch.Stop();
			var timeSpan = _stopwatch.Elapsed;
			LogCat.LogFormat("{0} 统计耗时结束,总共耗时{1}秒", this._name, timeSpan.TotalMilliseconds / 1000);
		}
	}
}