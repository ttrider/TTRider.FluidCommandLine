using System.Collections.Generic;

namespace TTRider.FluidCommandLine.Implementation
{
    public abstract class ParameterSet : IParameterProvider
    {
        internal HashSet<ParameterOption> Options { get; } = new HashSet<ParameterOption>();
        internal HashSet<ParameterParameter> Parameters { get; } = new HashSet<ParameterParameter>();


        protected abstract ParameterFactory GetFactory();

        ParameterFactory IParameterProvider.ParameterFactory => GetFactory();
        ParameterSet IParameterProvider.ParameterSet => this;
    }
}