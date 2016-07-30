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

            ParameterCommand command = null;
            var parameters = new Dictionary<string, List<string>>();
            var switches = new HashSet<string>();
            var arguments = new List<string>();

            ParameterSet ps = this;

            if (args != null)
            {
                var enumerator = args.GetEnumerator();


                List<string> currentParameter = null;



                if (enumerator.MoveNext())
                {
                    // first and only first element may be a command

                    string argument;
                    ParameterSwitch pSwitch;
                    ParameterParameter pParameter;
                    ClassifyParameter(enumerator.Current, out pSwitch, out pParameter, out argument);

                    // if there are commands, one of them needs to 
                    // either match the first argument or there should be a default command

                    if (argument != null)
                    {
                        if (this.Commands.Count == 0)
                        {
                            ps.ParameterArguments.Handler(argument);
                        }
                        else
                        {
                            foreach (var cmd in this.Commands)
                            {
                                if (string.Equals(cmd.Name, argument))
                                {
                                    cmd.Handler();
                                    command = cmd;
                                    break;
                                }
                                if (cmd.IsDefault)
                                {
                                    command = cmd;
                                }
                            }
                            if (command != null)
                            {
                                ps = command;
                            }
                            else
                            {
                                throw new MissingCommandException();
                            }
                        }
                    }
                    else if (pSwitch != null)
                    {
                        pSwitch.Handler();
                    }
                    else if (pParameter != null)
                    {
                        if (!parameters.TryGetValue(pParameter.Name, out currentParameter))
                        {
                            currentParameter = new List<string>();
                            parameters[pParameter.Name] = currentParameter;
                        }
                    }

                    while (enumerator.MoveNext())
                    {
                        ps.ClassifyParameter(enumerator.Current, out pSwitch, out pParameter, out argument);

                        if (argument != null)
                        {
                            if (currentParameter != null)
                            {
                                currentParameter.Add(argument);
                            }
                            else
                            {
                                arguments.Add(argument);
                                currentParameter = null;
                            }
                        }
                        else if (pSwitch != null)
                        {
                            switches.Add(pSwitch.Name);
                            currentParameter = null;
                        }
                        else if (pParameter != null)
                        {
                            if (!parameters.TryGetValue(pParameter.Name, out currentParameter))
                            {
                                currentParameter = new List<string>();
                                parameters[pParameter.Name] = currentParameter;
                            }
                        }

                    }
                }
            }
        }
    }
}