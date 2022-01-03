using System;

namespace CsCat
{
	public class ExceptionArgsTooLong : Exception
	{
		private const string EXCEPTION_ARGS_TOO_LONG = "args must less then 10";

		public ExceptionArgsTooLong() : base(EXCEPTION_ARGS_TOO_LONG)
		{
		}
	}
}