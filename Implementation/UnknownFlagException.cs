using System;

namespace TTRider.FluidCommandLine.Implementation
{
    public class UnknownFlagException : Exception
    {
        public string Parameter { get; private set; }

        public UnknownFlagException(string parameter)
        {
            this.Parameter = parameter;
        }
    }

    public class UnknownCommandException : Exception
    {
        public string Parameter { get; private set; }

        public UnknownCommandException(string parameter)
        {
            this.Parameter = parameter;
        }
    }

    public class UnknownOptionException : Exception
    {
        public string Parameter { get; private set; }

        public UnknownOptionException(string parameter)
        {
            this.Parameter = parameter;
        }
    }

    public class MissingDefaultParameterException : Exception
    {
    }

    public class MissingCommandException : Exception
    {

        public MissingCommandException()
        {
        }
    }
}