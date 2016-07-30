using System;
using TTRider.FluidCommandLine.Implementation;

namespace TTRider.FluidCommandLine
{
    public static class CommandLine
    {
        public static ICommandLine End()
        {
            return new ParameterFactory();
        }

        public static ICommandLine End(this IParameterProvider source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return source.ParameterFactory;
        }


        public static ParameterParameter Parameter(string description, Action<string> handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));
            return new ParameterFactory().Parameter(description, handler);
        }

        public static ParameterParameter Parameter<T>(this T source, string description, Action<string> handler)
            where T : IParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));

            var item = new ParameterParameter(source.ParameterSet, description, handler);
            source.ParameterSet.Parameters.Add(item);
            return item;
        }

        public static ParameterSwitch Switch(string description, Action handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));
            return new ParameterFactory().Switch(description, handler);
        }

        public static ParameterSwitch Switch<T>(this T source, string description, Action handler)
            where T : IParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));

            var item = new ParameterSwitch(source.ParameterSet, description, handler);
            source.ParameterSet.Switches.Add(item);
            return item;
        }

        public static ParameterFactory Arguments(string description, Action<string> handler)
        {
            return new ParameterFactory().Arguments(description, handler);
        }


        public static T Arguments<T>(this T source, string description, Action<string> handler)
            where T : IParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            source.ParameterSet.ParameterArguments.Description = description;
            source.ParameterSet.ParameterArguments.Handler = handler;
            return source;
        }

        public static ParameterCommand Command(string name, string description, Action handler, bool isDefault = false)
        {
            return new ParameterFactory().Command(name, description, handler, isDefault);
        }


        public static ParameterCommand Command<T>(this T source, string name, string description, Action handler, bool isDefault = false)
            where T : IParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var item = new ParameterCommand(source.ParameterFactory)
            {
                Name = name,
                Description = description,
                IsDefault = isDefault,
                Handler = handler
            };
            source.ParameterFactory.Commands.Add(item);
            return item;
        }

        public static ParameterParameter Option(this ParameterParameter source, string option)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            source.Options.Add(option);
            return source;
        }

        public static ParameterParameter Flag(this ParameterParameter source, char flag)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            source.Flags.Add(flag);
            return source;
        }
        public static ParameterSwitch Option(this ParameterSwitch source, string option)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            source.Options.Add(option);
            return source;
        }

        public static ParameterSwitch Flag(this ParameterSwitch source, char flag)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            source.Flags.Add(flag);
            return source;
        }
    }
}