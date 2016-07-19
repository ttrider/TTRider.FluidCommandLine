using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TTRider.FluidCommandLine.Implementation
{
    public abstract class ParameterSet : IParameterProvider
    {
        readonly Regex switchRegex = new Regex("(^-(?\'flag\'\\w)$)|(^--(?\'option\'[^\\s]+)$)|(^(?\'arg\'[^-][^-][^\\s]+)$)");

        protected ParameterSet()
        {
            this.ParameterArguments = new ParameterArguments(this) { Name = "Arguments", Description = null, Occcurance = Occurance.Prohibited };
        }

        public HashSet<ParameterParameter> Parameters { get; } = new HashSet<ParameterParameter>();
        public HashSet<ParameterSwitch> Switches { get; } = new HashSet<ParameterSwitch>();

        public ParameterArguments ParameterArguments { get; }


        ParameterSet IParameterProvider.ParameterSet => this;

        protected abstract ParameterFactory GetFactory();
        ParameterFactory IParameterProvider.ParameterFactory => GetFactory();


        internal bool TryGetSwitch(char flag, out ParameterSwitch value)
        {
            value = null;
            foreach (var item in this.Switches)
            {
                if (item.Flags.Contains(flag))
                {
                    value = item;
                    return true;
                }
            }
            return false;
        }
        internal bool TryGetSwitch(string option, out ParameterSwitch value)
        {
            value = null;
            foreach (var item in this.Switches)
            {
                if (item.Options.Contains(option))
                {
                    value = item;
                    return true;
                }
            }
            return false;
        }

        internal bool TryGetParameter(char flag, out ParameterParameter value)
        {
            value = null;
            foreach (var item in this.Parameters)
            {
                if (item.Flags.Contains(flag))
                {
                    value = item;
                    return true;
                }
            }
            return false;
        }
        internal bool TryGetParameter(string option, out ParameterParameter value)
        {
            value = null;
            foreach (var item in this.Parameters)
            {
                if (item.Options.Contains(option))
                {
                    value = item;
                    return true;
                }
            }
            return false;
        }


        internal void ClassifyParameter(string value, out ParameterSwitch pSwitch, out ParameterParameter pParameter, out string argument)
        {
            pSwitch = null;
            pParameter = null;
            argument = null;

            var m = switchRegex.Match(value);
            if (m.Success)
            {
                if (m.Groups["flag"].Success)
                {
                    var f = m.Groups["flag"].Value[0];
                    // lookup flag 
                    if (!this.TryGetSwitch(f, out pSwitch))
                    {
                        if (!this.TryGetParameter(f, out pParameter))
                        {
                            throw new UnknownFlagException(value);
                        }
                    }
                }
                else if (m.Groups["option"].Success)
                {
                    var f = m.Groups["option"].Value;
                    // lookup flag 
                    if (!this.TryGetSwitch(f, out pSwitch))
                    {
                        if (!this.TryGetParameter(f, out pParameter))
                        {
                            throw new UnknownFlagException(value);
                        }
                    }
                }
                else if (m.Groups["arg"].Success)
                {
                    argument = m.Groups["arg"].Value;
                }
                else
                {
                    throw new UnknownFlagException(value);
                }
            }
            else
            {
                throw new UnknownFlagException(value);
            }
        }
    }
}