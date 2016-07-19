using System.Collections.Generic;

namespace TTRider.FluidCommandLine.Implementation
{
    public class ParameterSwitch : ParameterItem, IParameterProvider
    {
        private readonly ParameterSet owner;

        internal ParameterSwitch(ParameterSet owner)
        {
            this.owner = owner;
        }

        public HashSet<char> Flags { get; } = new HashSet<char>();
        public HashSet<string> Options { get; } = new HashSet<string>();


        ParameterSet IParameterProvider.ParameterSet => owner;

        ParameterFactory IParameterProvider.ParameterFactory => ((IParameterProvider)owner).ParameterFactory;
    }
}