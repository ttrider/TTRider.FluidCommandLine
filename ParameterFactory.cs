using System.Collections.Generic;

namespace TTRider.FluidCommandLine
{
    public partial class ParameterFactory : ParameterSet
    {
        internal override ParameterFactory GetParameterFactory() => this;

        internal HashSet<ParameterCommand> Commands { get; } = new HashSet<ParameterCommand>();

        internal string HelpParameter { get; set; }
    }
}