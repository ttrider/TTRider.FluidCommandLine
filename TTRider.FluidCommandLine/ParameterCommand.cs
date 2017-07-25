using System;
using System.Collections.Generic;
using System.Linq;

namespace TTRider.FluidCommandLine
{
    public class ParameterCommand : ParameterSet
    {
        private readonly ParameterFactory parameterFactory;

        internal ParameterCommand(ParameterFactory parameterFactory, string name, string description, Action handler, bool isDefault)
        {
            this.parameterFactory = parameterFactory;
            Name = name;
            Description = description;
            IsDefault = isDefault;
            Handler = () =>
            {
                handler();
                return 0;
            };
        }
        internal ParameterCommand(ParameterFactory parameterFactory, string name, string description, Func<int> handler, bool isDefault)
        {
            this.parameterFactory = parameterFactory;
            Name = name;
            Description = description;
            IsDefault = isDefault;
            Handler = handler;
        }

        internal override ParameterFactory GetParameterFactory() => this.parameterFactory;

        internal string Name { get; }
        internal string Description { get; }
        internal bool IsDefault { get; }
        internal Func<int> Handler { get; }


        internal ParameterParameter GetDefaultParameter()
        {
            return this
                .Parameters
                .Concat(
                    GetParameterFactory().Parameters
                    )
                .FirstOrDefault(item => item.IsDefault);
        }

        internal ParameterParameter GetParameter(string name)
        {
            return this
                .Parameters
                .Concat(
                    GetParameterFactory().Parameters
                    )
                .FirstOrDefault(item => string.Equals(item.Name, name));
        }

        internal ParameterOption GetOption(string name)
        {
            return this
                .Options
                .Concat(
                    GetParameterFactory().Options
                    )
                .FirstOrDefault(item => string.Equals(item.Name, name));
        }

        internal IEnumerable<ParameterItem> ParameterItems
        {
            get
            {
                var pf = GetParameterFactory();

                return this.Parameters.AsEnumerable<ParameterItem>()
                    .Concat(this.Options)
                    .Concat(pf.Parameters)
                    .Concat(pf.Options);
            }
        }
    }
}