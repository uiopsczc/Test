using UnityEngine;

namespace CsCat
{
	public class LogMessage
	{
		public string message;
		public string stackTrace;
		public LogCatType logType;

		public LogMessage(string message, string stackTrace, LogCatType logType)
		{
			this.message = message;
			this.stackTrace = stackTrace != null ? "  " + stackTrace.Replace("\n", "\n  ") : null;
			this.logType = logType;
		}

	}
}
