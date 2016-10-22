namespace TTRider.FluidCommandLine.Implementation
{
    public class UnknownCommandException : CommandLineException
    {
        public string Command { get; private set; }

        public UnknownCommandException(string command)
            : base($"Unknown command '{command}'")
        {
            this.Command = command;
        }
    }
}