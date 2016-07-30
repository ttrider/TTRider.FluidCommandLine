using System;
using System.Collections.Generic;

namespace TTRider.FluidCommandLine.Implementation
{
    public class ParameterSwitch : ParameterItem, IParameterProvider
    {
        private readonly ParameterSet owner;

        internal ParameterSwitch(ParameterSet owner, string description, Action handler)
        {
            this.owner = owner;
            this.Handler = handler;
            this.Description = description;
        }

        public HashSet<char> Flags { get; } = new HashSet<char>();
        public HashSet<string> Options { get; } = new HashSet<string>();
        public Action Handler { get; }


        ParameterSet IParameterProvider.ParameterSet => owner;

        ParameterFactory IParameterProvider.ParameterFactory => ((IParameterProvider)owner).ParameterFactory;
    }
}