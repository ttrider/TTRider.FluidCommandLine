

using System;

namespace TTRider.FluidCommandLine.Implementation
{
    public abstract class ParameterItem : ParameterProvider
    {
        private readonly ParameterSet owner;

        protected ParameterItem(ParameterSet owner, Guid id, string name, string description)
        {
            this.owner = owner;
            this.Id = id;
            this.Name = name;
            this.Description = description;

        }
        internal Guid Id { get; }
        internal string Name { get; }
        internal string Description { get; }

        internal override ParameterFactory GetParameterFactory() => owner.GetParameterFactory();
        internal override ParameterSet GetParameterSet() => owner;
    }
}