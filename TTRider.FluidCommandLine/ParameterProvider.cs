using System;
using TTRider.FluidCommandLine;

namespace TTRider.FluidCommandLine
{
    public abstract class ParameterProvider
    {
        internal abstract ParameterSet GetParameterSet();
        internal abstract ParameterFactory GetParameterFactory();

        internal ParameterCommand AddCommand(string name, string description, Action handler, bool isDefault)
        {
            var item =
                new ParameterCommand(
                    this.GetParameterFactory(),
                    name,
                    description,
                    handler,
                    isDefault);

            this.GetParameterFactory().Commands.Add(item);

            return item;
        }

        internal ParameterCommand AddCommand(string name, string description, Func<int> handler, bool isDefault)
        {
            var item =
                new ParameterCommand(
                    this.GetParameterFactory(),
                    name,
                    description,
                    handler,
                    isDefault);

            this.GetParameterFactory().Commands.Add(item);

            return item;
        }

        internal ParameterProvider AddOption(Guid id, string name, string description, Action handler)
        {
            var ps = this.GetParameterSet();

            var item = new ParameterOption(
                ps,
                id,
                name,
                description,
                handler);

            ps.Options.Add(item);

            return item;
        }

        internal ParameterProvider AddHelp(string name)
        {
            var pf = this.GetParameterFactory();

            pf.HelpParameter = name;

            return pf;
        }

        internal ParameterProvider AddParameter(Guid id, string name, string description, Action<string> handler, bool isDefault, string valueName)
        {
            var ps = this.GetParameterSet();

            var item = new ParameterParameter(
                ps,
                id,
                name,
                description,
                handler,
                isDefault,
                valueName);

            ps.Parameters.Add(item);

            return item;

        }
    }
}