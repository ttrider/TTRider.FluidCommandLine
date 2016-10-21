using System.Collections.Generic;

namespace TTRider.FluidCommandLine
{
    public interface ICommandLine
    {
        int Run(params string[] args);
        int Run(IEnumerable<string> args);
    }
}
