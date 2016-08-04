using System;
using System.Collections.Generic;

namespace TTRider.FluidCommandLine.Implementation
{
    public class ParameterParameter : ParameterItem, IParameterProvider
    {
        private readonly ParameterSet owner;
        
        internal ParameterParameter(ParameterSet owner, string name, Action<string> handler)
        {
            this.owner = owner;
            this.Handler = handler;
            this.Name = name;
        }

        public string Value { get; set; }
        public Action<string> Handler { get; }


        ParameterSet IParameterProvider.ParameterSet => owner;

        ParameterFactory IParameterProvider.ParameterFactory => ((IParameterProvider) owner).ParameterFactory;
    }
}