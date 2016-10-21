using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TTRider.FluidCommandLine.Implementation
{
    public abstract class ParameterSet : IParameterProvider
    {
        private static readonly Regex SwitchRegex = new Regex(@"(?<option> -{1,2}\S*)(?:[=:]?|\s+)(?<value> [^-\s].*?)?(?=\s+[-\/]|$)");

        protected ParameterSet()
        {
            this.ParameterArguments = new ParameterArguments(this) { Name = "Arguments", Description = null, Occcurance = Occurance.Prohibited };
        }

        public HashSet<ParameterOption> Options { get; } = new HashSet<ParameterOption>();
        public HashSet<ParameterOptionValue> OptionsValues { get; } = new HashSet<ParameterOptionValue>();
        public HashSet<ParameterParameter> Parameters { get; } = new HashSet<ParameterParameter>();

        public ParameterArguments ParameterArguments { get; }


        ParameterSet IParameterProvider.ParameterSet => this;

        protected abstract ParameterFactory GetFactory();
        ParameterFactory IParameterProvider.ParameterFactory => GetFactory();

        internal bool TryGetOption(ParameterCommand command, string option, out ParameterOption value)
        {
            value = null;
            option = option.Replace("-", "").Trim();
            foreach (var item in command.Options)
            {
                if (item.Name.Equals(option))
                {
                    value = item;
                    return true;
                }
            }
            return false;
        }

        internal bool TryGetParameter(ParameterCommand command, string parameter, out ParameterParameter value)
        {
            value = null;
            parameter = parameter.Replace("-", "").Trim();
            if (string.IsNullOrWhiteSpace(parameter))
            {
                value = command.Parameters.FirstOrDefault(x => x.IsDefault);
            }
            else
            {
                foreach (var item in command.Parameters)
                {
                    if (item.Name.Equals(parameter))
                    {
                        value = new ParameterParameter(item);
                        return true;
                    }
                }
            }
            return (value != null);
        }

        internal bool TryGetOptionValue(ParameterCommand command, string optionValue, IList<ParameterOptionValue> value)
        {
            if (value != null)
            {
                optionValue = optionValue.Trim();
                if(string.IsNullOrWhiteSpace(optionValue))
                {
                    return true;
                }
                foreach (var item in command.OptionsValues)
                {
                    if (item.Name.Equals(optionValue))
                    {
                        value.Add(item);
                        return true;
                    }
                }
            }
            return false;
        }

        internal bool TryGetCommand(string command, out ParameterCommand value)
        {
            value = null;
            foreach (var item in this.GetFactory().Commands)
            {
                if (item.Name.Equals(command, StringComparison.OrdinalIgnoreCase))
                {
                    value = item;
                    return true;
                }
            }
            value = this.GetFactory().GetDefaultCommand();
            if (value != null)
            {
                return true;
            }
            return false;
        }

        internal void ClassifyParameter(string value, List<ParameterOption> optionsList, List<ParameterOptionValue> optionsValuesList, List<ParameterParameter> parametersList, out ParameterCommand command)
        {
            string commandName = string.Empty;
            string tempValue = string.Empty;
            ParameterOption tempOption = null;
            ParameterParameter tempParameter = null;
            
            if (value.Trim().Contains(" "))
            {
                commandName = value.Substring(0, value.IndexOf(' '));
            }
            else
            {
                commandName = value.Trim();
            }

            if (!this.TryGetCommand(commandName, out command))
            {
                throw new UnknownCommandException(value);
            }
            if (commandName != command.Name)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(command.Name);
                sb.Append(" ");
                sb.Append(value);
                tempValue = value;
                value = sb.ToString();
            }
            else if (value == commandName)
            {
                value = string.Empty;
            }
            if (!string.IsNullOrWhiteSpace(value))
            {
                var m = SwitchRegex.Match(value);
                if (m.Success)
                {
                    tempValue = value;
                    tempValue = tempValue.Remove(0, command.Name.Length);
                    List<KeyValuePair<string, string>> matchesList = new List<KeyValuePair<string, string>>();
                    while (m.Success)
                    {
                        tempParameter = null;
                        if (TryGetParameter(command, m.Groups["option"].Value, out tempParameter))
                        {
                            tempParameter.Value = m.Groups["value"].Value.Trim();
                            parametersList.Add(tempParameter);
                        }
                        else if (!TryGetOption(command, m.Groups["option"].Value, out tempOption))
                        {
                            throw new UnknownOptionException(value);
                        }
                        else
                        {
                            optionsList.Add(tempOption);
                            if (!TryGetOptionValue(command, m.Groups["value"].Value, optionsValuesList))
                            {
                                throw new UnknownOptionException(value);
                            }
                        }

                        tempValue = tempValue.Replace(m.Value, "");
                        m = m.NextMatch();
                    }

                    if (!string.IsNullOrWhiteSpace(tempValue))
                    {
                        if (TryGetParameter(command, string.Empty, out tempParameter))
                        {
                            tempParameter.Value = tempValue.Trim();
                            parametersList.Add(tempParameter);
                        }
                    }
                }     
                else if (!string.IsNullOrWhiteSpace(value))
                {
                    value = value.Remove(0, command.Name.Length);
                    if (TryGetParameter(command, string.Empty, out tempParameter))
                    {
                        tempParameter.Value = value.Trim();
                        parametersList.Add(tempParameter);
                    }
                }                        
            }
        }
    }
}