namespace TTRider.FluidCommandLine.Implementation
{
    public class UnknownParameterOrOptionException : CommandLineException
    {
        public string Parameter { get; private set; }

        public UnknownParameterOrOptionException(string parameter)
            : base($"Unknown parameter or option '{parameter}'")
        {
            this.Parameter = parameter;
        }
    }
}