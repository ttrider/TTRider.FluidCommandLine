using System;

namespace TTRider.FluidCommandLine.Implementation
{
    public class ParameterOption : ParameterItem, IParameterProvider
    {
        private readonly ParameterSet owner;
        
        internal ParameterOption(ParameterSet owner, string name, Action handler)
        {
            this.owner = owner;
            this.Handler = handler;
            this.Name = name;
        }

        internal Action Handler { get; }

        ParameterSet IParameterProvider.ParameterSet => owner;

        ParameterFactory IParameterProvider.ParameterFactory => ((IParameterProvider)owner).ParameterFactory;
    }
}
