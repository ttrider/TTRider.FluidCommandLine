using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TTRider.FluidCommandLine.Implementation
{
    public class ParameterFactory : ParameterSet, ICommandLine
    {

        protected override ParameterFactory GetFactory()
        {
            return this;
        }

        public HashSet<ParameterCommand> Commands { get; } = new HashSet<ParameterCommand>();


        public void Build(params string[] args)
        {
            Build((IEnumerable<string>)args);
        }

        public void Build(IEnumerable<string> args)
        {

            List<ParameterOption> options = new List<ParameterOption>();
            List<ParameterOptionValue> optionsValues = new List<ParameterOptionValue>();
            List<ParameterParameter> parameters = new List<ParameterParameter>();

            ParameterSet ps = this;

            if (args != null)
            {
                var enumerator = args.GetEnumerator();

                if (enumerator.MoveNext())
                {

                    ParameterCommand pCommand;
                    ClassifyParameter(enumerator.Current, options, optionsValues, parameters, out pCommand);

                    if (pCommand != null)
                    {
                        if (parameters.Count > 0)
                        {
                            foreach (ParameterParameter paramItem in parameters)
                            {
                                paramItem.Handler(paramItem.Value);
                            }
                        }
                        if (optionsValues.Count > 0)
                        {
                            foreach (ParameterOptionValue optionValue in optionsValues)
                            {
                                optionValue.Handler();
                            }
                        }
                        if (options.Count > 0)
                        {
                            foreach (ParameterOption option in options)
                            {
                                option.Handler();
                            }
                        }
                        pCommand.Handler();
                    }
                    // if there are commands, one of them needs to 
                    // either match the first argument or there should be a default command
                }
            }
        }
    }
}