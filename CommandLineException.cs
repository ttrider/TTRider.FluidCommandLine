using System;

namespace TTRider.FluidCommandLine.Implementation
{
    public class CommandLineException : Exception
    {
        protected CommandLineException(string message)
            : base(message)
        {
        }
    }
}