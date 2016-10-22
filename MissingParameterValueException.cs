namespace TTRider.FluidCommandLine.Implementation
{
    public class MissingParameterValueException : CommandLineException
    {
        public string Parameter { get; private set; }

        public MissingParameterValueException(string parameter)
            : base($"Parameter '{parameter}' expects a value")
        {
            this.Parameter = parameter;
        }
    }
}