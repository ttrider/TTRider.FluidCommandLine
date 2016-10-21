using System;
using System.Collections.Generic;
using System.Linq;
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
                    using (var arg = args
                        .Where(a => !string.IsNullOrWhiteSpace(a))
                        .GetEnumerator())
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

                                    var defaultParameter = currentCommand.Parameters.FirstOrDefault(
                                        item => item.IsDefault);
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

                                    var parameter =
                                        currentCommand.Parameters.FirstOrDefault(
                                            item => item.Name.Equals(parameterName, StringComparison.OrdinalIgnoreCase));
                                    var options =
                                        currentCommand.Options.FirstOrDefault(
                                            item => item.Name.Equals(parameterName, StringComparison.OrdinalIgnoreCase));

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

                                    var parameter =
                                        currentCommand.Parameters.FirstOrDefault(
                                            item => item.IsDefault);
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
                        }
                        else
                        {
                            //print help
                            return 1;
                        }
                    }



                }
                else
                {
                    //print help here
                    return 1;
                }
                return 0;
            }
            catch (CommandLineException ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }
    }
}