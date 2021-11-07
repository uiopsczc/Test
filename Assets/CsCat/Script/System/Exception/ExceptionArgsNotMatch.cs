using System;

namespace CsCat
{
    public class ExceptionArgsNotMatch : Exception
    {
        private const string EXCEPTION_ARGS_NOT_MATCH = "args not match";

        public ExceptionArgsNotMatch() : base(EXCEPTION_ARGS_NOT_MATCH)
        {
        }
    }
}