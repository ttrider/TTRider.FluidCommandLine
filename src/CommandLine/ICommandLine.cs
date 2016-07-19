using System.Collections.Generic;

namespace TTRider.FluidCommandLine
{
    public interface ICommandLine
    {
        CommandLineParameters Bind(params string[] args);
        CommandLineParameters Bind(IEnumerable<string> args);
    }
}
