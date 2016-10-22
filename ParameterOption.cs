using System;

namespace TTRider.FluidCommandLine
{
    public class ParameterOption : ParameterItem
    {
        
        internal ParameterOption(ParameterSet owner, Guid id, string name, string description, Action handler)
            :base(owner, id, name, description)
        {
            this.Handler = handler;
        }

        internal Action Handler { get; }

    }
}
