using System;
using TTRider.FluidCommandLine.Implementation;

namespace TTRider.FluidCommandLine
{
    public static class CommandLine
    {
        public static ParameterFactory End()
        {
            return new ParameterFactory();
        }

        public static ParameterFactory End(this IParameterProvider source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return source.ParameterFactory;
        }


        public static ParameterParameter Parameter(string name, string description, Occurance occcurance = Occurance.Optional)
        {
            return new ParameterFactory().Parameter(name, description, occcurance);
        }

        public static ParameterParameter Parameter(string name, Occurance occcurance = Occurance.Optional)
        {
            return new ParameterFactory().Parameter(name, occcurance);
        }

        public static ParameterParameter Parameter<T>(this T source, string name, string description, Occurance occcurance = Occurance.Optional)
            where T : IParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var item = new ParameterParameter(source.ParameterSet) { Name = name, Description = description, Occcurance = occcurance };
            source.ParameterSet.Parameters.Add(item);
            return item;
        }

        public static ParameterParameter Parameter<T>(this T source, string name, Occurance occcurance = Occurance.Optional)
            where T : IParameterProvider
        {
            return source.Parameter(name, null, occcurance);
        }

        public static ParameterSwitch Switch(string name, string description, Occurance occcurance = Occurance.Optional)
        {
            return new ParameterFactory().Switch(name, description, occcurance);
        }

        public static ParameterSwitch Switch(string name, Occurance occcurance = Occurance.Optional)
        {
            return new ParameterFactory().Switch(name, occcurance);
        }

        public static ParameterSwitch Switch<T>(this T source, string name, string description, Occurance occcurance = Occurance.Optional)
            where T : IParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var item = new ParameterSwitch(source.ParameterSet) { Name = name, Description = description, Occcurance = occcurance };
            source.ParameterSet.Switches.Add(item);
            return item;
        }

        public static ParameterSwitch Switch<T>(this T source, string name, Occurance occcurance = Occurance.Optional)
            where T : IParameterProvider
        {
            return source.Switch(name, null, occcurance);
        }


        public static ParameterFactory Arguments(string description, Occurance occcurance = Occurance.Optional)
        {
            return new ParameterFactory().Arguments(description, occcurance);
        }

        public static ParameterFactory Arguments(Occurance occcurance = Occurance.Optional)
        {
            return new ParameterFactory().Arguments(occcurance);
        }

        public static T Arguments<T>(this T source, string description, Occurance occcurance = Occurance.Optional)
            where T : IParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            source.ParameterSet.ParameterArguments.Description = description;
            source.ParameterSet.ParameterArguments.Occcurance = occcurance;
            return source;
        }

        public static T Arguments<T>(this T source, Occurance occcurance = Occurance.Optional)
            where T : IParameterProvider
        {
            return source.Arguments(null, occcurance);
        }


        public static ParameterCommand Command(string name, string description, bool isDefault = false)
        {
            return new ParameterFactory().Command(name, description, isDefault);
        }

        public static ParameterCommand Command(string name, bool isDefault = false)
        {
            return new ParameterFactory().Command(name, isDefault);
        }

        public static ParameterCommand Command<T>(this T source, string name, string description, bool isDefault = false)
            where T : IParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var item = new ParameterCommand(source.ParameterFactory)
            {
                Name = name,
                Description = description,
                IsDefault = isDefault
            };
            source.ParameterFactory.Commands.Add(item);
            return item;
        }

        public static ParameterCommand Command<T>(this T source, string name, bool isDefault = false)
             where T : IParameterProvider
        {
            return source.Command(name, null, isDefault);
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
    }
}