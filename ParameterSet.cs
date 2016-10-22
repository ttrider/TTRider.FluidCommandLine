using System.Collections.Generic;

namespace TTRider.FluidCommandLine
{
    public abstract class ParameterSet : ParameterProvider
    {
        internal HashSet<ParameterOption> Options { get; } = new HashSet<ParameterOption>();
        internal HashSet<ParameterParameter> Parameters { get; } = new HashSet<ParameterParameter>();
        internal override ParameterSet GetParameterSet() => this;
    }
}