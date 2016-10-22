namespace TTRider.FluidCommandLine.Implementation
{
    public class MissingDefaultParameterException : CommandLineException
    {
        public MissingDefaultParameterException(string argument)
            : base($"Can't assign argument '{argument}' - this command doesn't have default parameter")
        { }
    }
}