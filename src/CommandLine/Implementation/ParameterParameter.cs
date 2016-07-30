using System;
using System.Collections.Generic;

namespace TTRider.FluidCommandLine.Implementation
{
    public class ParameterParameter : ParameterItem, IParameterProvider
    {
        private readonly ParameterSet owner;

        internal ParameterParameter(ParameterSet owner, string description, Action<string> handler)
        {
            this.owner = owner;
            this.Handler = handler;
            this.Description = description;
        }

        public HashSet<char> Flags { get; } = new HashSet<char>();
        public HashSet<string> Options { get; } = new HashSet<string>();

        public Action<string> Handler { get; }


        ParameterSet IParameterProvider.ParameterSet => owner;

        ParameterFactory IParameterProvider.ParameterFactory => ((IParameterProvider) owner).ParameterFactory;
    }
}