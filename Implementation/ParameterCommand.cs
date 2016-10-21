using System;

namespace TTRider.FluidCommandLine.Implementation
{
    public class ParameterCommand : ParameterSet
    {
        private readonly ParameterFactory parameterFactory;

        internal ParameterCommand(ParameterFactory parameterFactory)
        {
            this.parameterFactory = parameterFactory;
        }

        protected override ParameterFactory GetFactory()
        {
            return this.parameterFactory;
        }

        internal string Name { get; set; }
        internal string Description { get; set; }
        internal bool IsDefault { get; set; }
        internal Action Handler { get; set; }
    }
}