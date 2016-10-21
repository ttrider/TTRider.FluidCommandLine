using System;
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

        public ParameterCommand GetDefaultCommand()
        {
            return Commands.FirstOrDefault(x => x.IsDefault);
        }


        static readonly Regex ParameterRegex = new Regex(@"-{1,2}(.*)");
        

        public void Build(params string[] args)
        {
            Build((IEnumerable<string>)args);
        }
        public void Build(IEnumerable<string> args)
        {


            if (args != null)
            {
                using (var arg = args
                    .Where(a=>!string.IsNullOrWhiteSpace(a))
                    .GetEnumerator())
                {
                    if (arg.MoveNext())
                    {

                        // do we have a command or the first argument for the default command
                        var match = ParameterRegex.Match(arg.Current);
                        var currentCommand =
                            !match.Success
                                ? this.Commands.FirstOrDefault(item => item.Name.Equals(arg.Current, StringComparison.OrdinalIgnoreCase))
                                : this.Commands.FirstOrDefault(item => item.IsDefault);

                        if (currentCommand == null)
                        {
                            throw new UnknownCommandException(arg.Current);
                        }


                        // let's process the rest of the command line
                        var processing = match.Success || arg.MoveNext();
                        var inTail = false;

                        while (processing)
                        {
                            match = ParameterRegex.Match(arg.Current);
                            if (match.Success)
                            {
                                if (inTail)
                                {
                                    throw new UnknownOptionException("can't have options after tail arguments");
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
                                        parameter.Handler(null);
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
                                            parameter.Handler(null);
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
                                    throw new UnknownOptionException(parameterName);
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
                                    throw new MissingDefaultParameterException();
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
                    }
                }



            }
            else
            {
                //print help here
            }
        }
        //public void Build(IEnumerable<string> args)
        //{
        //    if (args != null)
        //    {
        //        var argumentString = string.Join(" ", args);


        //        List<ParameterOption> options = new List<ParameterOption>();
        //        List<ParameterOptionValue> optionsValues = new List<ParameterOptionValue>();
        //        List<ParameterParameter> parameters = new List<ParameterParameter>();

        //        ParameterSet ps = this;

        //        ParameterCommand pCommand;
        //        ClassifyParameter(argumentString, options, optionsValues, parameters, out pCommand);

        //        if (pCommand != null)
        //        {
        //            if (parameters.Count > 0)
        //            {
        //                foreach (ParameterParameter paramItem in parameters)
        //                {
        //                    paramItem.Handler(paramItem.Value);
        //                }
        //            }
        //            if (optionsValues.Count > 0)
        //            {
        //                foreach (ParameterOptionValue optionValue in optionsValues)
        //                {
        //                    optionValue.Handler();
        //                }
        //            }
        //            if (options.Count > 0)
        //            {
        //                foreach (ParameterOption option in options)
        //                {
        //                    option.Handler();
        //                }
        //            }
        //            pCommand.Handler();
        //        }
        //    }
        //}
    }
}