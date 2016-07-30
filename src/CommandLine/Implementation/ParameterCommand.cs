using System;

namespace TTRider.FluidCommandLine.Implementation
{
    public class ParameterCommand : ParameterSet
    {
        private readonly ParameterFactory parameterFactory;

        public ParameterCommand(ParameterFactory parameterFactory)
        {
            this.parameterFactory = parameterFactory;
        }

        protected override ParameterFactory GetFactory()
        {
            return this.parameterFactory;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public Action Handler { get; set; }
    }
}