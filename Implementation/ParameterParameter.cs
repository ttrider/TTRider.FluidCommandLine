using System;
using System.Collections.Generic;

namespace TTRider.FluidCommandLine.Implementation
{
    public class ParameterParameter : ParameterItem, IParameterProvider
    {
        private readonly ParameterSet owner;

        internal ParameterParameter(ParameterParameter param)
        {
            this.owner = param.owner;
            this.Handler = param.Handler;
            this.Name = param.Name;
            this.IsDefault = param.IsDefault;
        }

        internal ParameterParameter(ParameterSet owner, string name, Action<string> handler, bool IsDefault = false)
        {
            this.owner = owner;
            this.Handler = handler;
            this.Name = name;
            this.IsDefault = IsDefault;
        }

        public string Value { get; set; }
        public bool IsDefault { get; set; }
        public Action<string> Handler { get; }


        ParameterSet IParameterProvider.ParameterSet => owner;

        ParameterFactory IParameterProvider.ParameterFactory => ((IParameterProvider) owner).ParameterFactory;
    }
}