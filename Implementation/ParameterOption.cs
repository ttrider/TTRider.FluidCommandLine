using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTRider.FluidCommandLine.Implementation
{
    public class ParameterOption : ParameterSet, IParameterProvider
    {
        private readonly ParameterSet owner;
        
        internal ParameterOption(ParameterSet owner, string name, Action handler, bool isDefault)
        {
            this.owner = owner;
            this.Handler = handler;
            this.Name = name;
            this.IsDefault = isDefault;
        }

        public Action Handler { get; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }


        ParameterSet IParameterProvider.ParameterSet => owner;

        ParameterFactory IParameterProvider.ParameterFactory => ((IParameterProvider)owner).ParameterFactory;

        protected override ParameterFactory GetFactory()
        {
            throw new NotImplementedException();
        }
    }
}
