namespace TTRider.FluidCommandLine.Implementation
{
    public class MixedOptionException : CommandLineException
    {

        public MixedOptionException(string value)
            : base($"Can't have parameter or option {value} inside the argument list")
        {
        }
    }
}