using System;

namespace TTRider.FluidCommandLine
{
    public class CommandLineException : Exception
    {
        protected CommandLineException(string message)
            : base(message)
        {
        }
    }
}