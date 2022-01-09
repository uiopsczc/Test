using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CsCat
{
	public class ShellRequest
	{
		public event Action<LogCatType, string> onLogAction;
		public event Action onErrorAction;
		public event Action onDoneAction;

		public void Log(LogCatType logType, string log)
		{
			onLogAction?.Invoke(logType, log);

			if (logType == LogCatType.Error)
				LogCat.LogError(log);
		}

		public void NotifyDone()
		{
			onDoneAction?.Invoke();
		}

		public void Error()
		{
			onErrorAction?.Invoke();
		}

		public TaskAwaiter GetAwaiter()
		{
			var tcs = new TaskCompletionSource<object>();
			onDoneAction += () => { tcs.SetResult(true); };
			return ((Task)tcs.Task).GetAwaiter();
		}
	}
}