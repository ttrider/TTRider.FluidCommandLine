using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TTRider.FluidCommandLine.Implementation
{
    public partial class ParameterFactory : ICommandLine
    {
        static readonly Regex ParameterRegex = new Regex(@"-{1,2}(.*)");

        int ICommandLine.Run(params string[] args)
        {
            return ((ICommandLine)this).Run((IEnumerable<string>)args);
        }
        int ICommandLine.Run(IEnumerable<string> args)
        {
            try
            {
                if (args != null)
                {
                    using (var arg = args.GetEnumerator())
                    {
                        if (arg.MoveNext())
                        {
                            // do we have a command or the first option/argument for the default command
                            var match = ParameterRegex.Match(arg.Current);
                            ParameterCommand currentCommand;
                            bool processing = false;
                            var inTail = false;

                            if (!match.Success)
                            {
                                currentCommand =
                                    this.Commands.FirstOrDefault(
                                        item => item.Name.Equals(arg.Current, StringComparison.OrdinalIgnoreCase));
                                if (currentCommand == null)
                                {
                                    // it is possible that we have a default command's default parameter's argument
                                    currentCommand = this.Commands.FirstOrDefault(item => item.IsDefault);
                                    if (currentCommand == null)
                                    {
                                        throw new UnknownCommandException(arg.Current);
                                    }

                                    var defaultParameter = currentCommand.GetDefaultParameter();
                                    if (defaultParameter == null)
                                    {
                                        throw new UnknownCommandException(arg.Current);
                                    }
                                    defaultParameter.Handler(arg.Current);
                                    inTail = true;
                                }
                                processing = arg.MoveNext();
                            }
                            else
                            {
                                // a special case - if we have a help flag as the first argument
                                // display a complete help information
                                if (string.Equals(this.HelpParameter, match.Groups[1].Value,
                                    StringComparison.OrdinalIgnoreCase))
                                {
                                    PrintHelp();
                                    return 1;
                                }

                                currentCommand = this.Commands.FirstOrDefault(c => c.IsDefault);
                                if (currentCommand == null)
                                {
                                    throw new UnknownCommandException(arg.Current);
                                }
                                processing = true;
                            }

                            while (processing)
                            {
                                match = ParameterRegex.Match(arg.Current);
                                if (match.Success)
                                {
                                    if (inTail)
                                    {
                                        throw new MixedOptionException(arg.Current);
                                    }

                                    var parameterName = match.Groups[1].Value;

                                    // in case we found a help parameter - display command help
                                    if (string.Equals(this.HelpParameter, parameterName, StringComparison.OrdinalIgnoreCase))
                                    {
                                        PrintHelp(currentCommand);
                                        return 1;
                                    }

                                    var parameter = currentCommand.GetParameter(parameterName);
                                    var options = currentCommand.GetOption(parameterName);

                                    if (parameter != null)
                                    {
                                        // we may have a value in the next argument
                                        processing = arg.MoveNext();
                                        if (!processing)
                                        {
                                            // we don't have a value - it must be an option then
                                            if (options == null)
                                            {
                                                throw new MissingParameterValueException(parameter.Name);
                                            }
                                            options.Handler();
                                        }
                                        else
                                        {
                                            match = ParameterRegex.Match(arg.Current);
                                            if (!match.Success)
                                            {
                                                parameter.Handler(arg.Current);
                                                processing = arg.MoveNext();
                                            }
                                            else
                                            {
                                                // we don't have a value - it must be an option then
                                                if (options == null)
                                                {
                                                    throw new MissingParameterValueException(parameter.Name);
                                                }
                                                options.Handler();
                                            }
                                        }
                                    }
                                    else if (options != null)
                                    {
                                        options.Handler();
                                        processing = arg.MoveNext();
                                    }
                                    else
                                    {
                                        throw new UnknownParameterOrOptionException(parameterName);
                                    }
                                }
                                else
                                {
                                    // looks like we have some tail arguments

                                    var parameter = currentCommand.GetDefaultParameter();
                                    if (parameter == null)
                                    {
                                        throw new MissingDefaultParameterException(arg.Current);
                                    }

                                    parameter.Handler(arg.Current);
                                    inTail = true;


                                    processing = arg.MoveNext();
                                }

                            }

                            currentCommand.Handler();
                            return 1;
                        }
                        PrintHelp();
                        return 1;
                    }
                }
                PrintHelp();
                return 1;
            }
            catch (CommandLineException ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }

        private void PrintHelp(ParameterCommand currentCommand)
        {
            Console.WriteLine();
            Console.WriteLine("usage:");
            Console.WriteLine($"\tac {currentCommand.Name} [parameters] | -{this.HelpParameter}");
            Console.WriteLine($"\t{currentCommand.Description}");
            Console.WriteLine();
            Console.WriteLine($"parameters:");

            foreach (var group in
                currentCommand.ParameterItems
                    .GroupBy(pi => pi.Id))
            {
                string description = null;
                bool hasParameter = false;
                bool hasOptions = false;
                bool hasDefault = false;
                string valueName = null;

                var names = new HashSet<string>();

                foreach (var item in group)
                {
                    description = item.Description;
                    names.Add(item.Name);
                    if (item is ParameterParameter)
                    {
                        hasParameter = true;

                        var p = (ParameterParameter)item;

                        hasDefault |= p.IsDefault;

                        if (!string.IsNullOrWhiteSpace(p.ValueName))
                        {
                            valueName = p.ValueName;
                        }
                    }
                    else
                    {
                        hasOptions = true;
                    }
                }

                var sb = new StringBuilder("\t");
                sb.Append(string.Join("|", names.Select(name => (name.Length == 1 ? "-" : "--") + name)));
                sb.Append(" ");
                if (hasParameter)
                {
                    if (string.IsNullOrWhiteSpace(valueName))
                    {
                        if (hasOptions)
                        {
                            sb.Append("[<");
                            sb.Append(description);
                            sb.Append(">]");
                        }
                        else
                        {
                            sb.Append("<");
                            sb.Append(description);
                            sb.Append(">");
                        }

                    }
                    else
                    {
                        if (hasOptions)
                        {
                            sb.Append("[<");
                            sb.Append(valueName);
                            sb.Append(">]");
                        }
                        else
                        {
                            sb.Append("<");
                            sb.Append(valueName);
                            sb.Append(">");
                        }

                        sb.Append(" - ");
                        sb.Append(description);
                    }

                    if (hasDefault)
                    {
                        sb.Append(" (*)");

                    }
                }
                else if (hasOptions)
                {
                    sb.Append("- ");
                    sb.Append(description);
                }

                Console.WriteLine(sb.ToString());
            }
            Console.WriteLine();
        }

        private void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine("usage:");
            Console.WriteLine($"\tac [command] [parameters] | -{this.HelpParameter}");
            Console.WriteLine();
            Console.WriteLine($"command{(this.Commands.Count == 1 ? "" : "s")}:");

            foreach (var command in this.Commands)
            {
                Console.WriteLine($"\t{command.Name,-10}\t{command.Description}\t{(command.IsDefault ? " (*)" : "")}");
            }
            Console.WriteLine();
        }
    }
}