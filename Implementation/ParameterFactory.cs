using System.Collections.Generic;

namespace TTRider.FluidCommandLine.Implementation
{
    public partial class ParameterFactory : ParameterSet
    {
        protected override ParameterFactory GetFactory() => this;

        internal HashSet<ParameterCommand> Commands { get; } = new HashSet<ParameterCommand>();
    }
}