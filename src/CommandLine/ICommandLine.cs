using System.Collections.Generic;

namespace TTRider.FluidCommandLine
{
    public interface ICommandLine
    {
        void Build(params string[] args);
        void Build(IEnumerable<string> args);
    }
}
