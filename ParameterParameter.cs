using System;

namespace TTRider.FluidCommandLine
{
    public class ParameterParameter : ParameterItem
    {
        internal ParameterParameter(ParameterSet owner, Guid id, string name, string description, Action<string> handler, bool isDefault, string valueName)
            : base(owner, id, name, description)
        {
            this.Handler = handler;
            this.IsDefault = isDefault;
            this.ValueName = valueName;
        }
        internal string ValueName { get; }
        internal bool IsDefault { get; }
        internal Action<string> Handler { get; }
    }
}