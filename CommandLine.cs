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

        public static ParameterParameter Parameter<T>(this T source, string name, Action<string> handler, bool isDefault = false)
            where T : IParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            var item = new ParameterParameter(source.ParameterSet, name, handler, isDefault);
            source.ParameterSet.Parameters.Add(item);
            return item;
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

        public static ParameterOption Option<T>(this T source, string name, Action handler, bool isDefault = false)
            where T : IParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            var item = new ParameterOption(source.ParameterSet, name, handler, isDefault);
            source.ParameterSet.Options.Add(item);
            return item;
        }

        public static ParameterOptionValue OptionValue<T>(this T source, string name, Action handler)
            where T : IParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            var item = new ParameterOptionValue(source.ParameterSet, name, handler);
            source.ParameterSet.OptionsValues.Add(item);
            return item;
        }       
    }
}