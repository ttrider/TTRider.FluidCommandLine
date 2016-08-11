using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTRider.FluidCommandLine.Implementation
{
    public class ParameterOptionValue : ParameterItem, IParameterProvider
    {
        public readonly ParameterSet Owner;

        internal ParameterOptionValue(ParameterSet owner, string name, Action handler)
        {
            this.Owner = owner;
            this.Handler = handler;
            this.Name = name;
        }

        public HashSet<ParameterOptionValue> OptionsValues { get; } = new HashSet<ParameterOptionValue>();

        //public HashSet<char> Flags { get; } = new HashSet<char>();
        //public HashSet<string> Options { get; } = new HashSet<string>();
        public Action Handler { get; }


        ParameterSet IParameterProvider.ParameterSet => Owner;

        ParameterFactory IParameterProvider.ParameterFactory => ((IParameterProvider)Owner).ParameterFactory;
    }    
}
