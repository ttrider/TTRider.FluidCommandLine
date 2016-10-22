using System;

namespace TTRider.FluidCommandLine
{
    public static class CommandLine
    {
        public static ICommandLine End()
        {
            return new ParameterFactory();
        }

        public static ICommandLine End(this ParameterProvider source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return source.GetParameterFactory();
        }

        public static ParameterProvider Help(string name)
        {
            return new ParameterFactory().Help(name);
        }
        public static ParameterProvider Help<T>(this T source, string name)
            where T : ParameterProvider
        {
            return source.AddHelp(name);
        }


        #region Commands

        public static ParameterCommand DefaultCommand(string name, Action handler, string description)
        {
            return new ParameterFactory().Command(name, handler, true, description);
        }
        public static ParameterCommand Command(string name, Action handler, string description)
        {
            return new ParameterFactory().Command(name, handler, false, description);
        }
        public static ParameterCommand Command(string name, Action handler, bool isDefault, string description)
        {
            return new ParameterFactory().Command(name, handler, isDefault, description);
        }
        public static ParameterCommand DefaultCommand<T>(this T source, string name, Action handler, string description)
            where T : ParameterProvider
        {
            return source.Command(name, handler, true, description);
        }
        public static ParameterCommand Command<T>(this T source, string name, Action handler, string description)
            where T : ParameterProvider
        {
            return source.Command(name, handler, false, description);
        }
        public static ParameterCommand Command<T>(this T source, string name, Action handler, bool isDefault, string description)
            where T : ParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));

            return source.AddCommand(
                    name,
                    description,
                    handler,
                    isDefault);
        }
        #endregion Commands

        #region Parameters

        public static ParameterProvider Parameter<T>(this T source, string name, Action<string> handler, bool isDefault, string description, string valueName = null)
            where T : ParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            return source.AddParameter(Guid.NewGuid(), name, description, handler, isDefault, valueName);
        }

        public static ParameterProvider Parameter<T>(this T source, string name, string alias, Action<string> handler, bool isDefault, string description, string valueName = null)
            where T : ParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(alias))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(alias));

            var id = Guid.NewGuid();

            return source
                .AddParameter(id, name, description, handler, isDefault, valueName)
                .AddParameter(id, alias, description, handler, isDefault, valueName);
        }


        public static ParameterProvider Parameter<T>(this T source, string name, Action<string> handler, string description, string valueName = null)
            where T : ParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));
            return source.AddParameter(Guid.NewGuid(), name, description, handler, false, valueName);
        }

        public static ParameterProvider Parameter<T>(this T source, string name, string alias, Action<string> handler, string description, string valueName = null)
            where T : ParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(alias))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(alias));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));

            var id = Guid.NewGuid();

            return source
                .AddParameter(id, name, description, handler, false, valueName)
                .AddParameter(id, alias, description, handler, false, valueName);
        }

        public static ParameterProvider DefaultParameter<T>(this T source, string name, Action<string> handler, string description, string valueName = null)
            where T : ParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));
            return source.AddParameter(Guid.NewGuid(), name, description, handler, true, valueName);
        }

        public static ParameterProvider DefaultParameter<T>(this T source, string name, string alias, Action<string> handler, string description, string valueName = null)
            where T : ParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(alias))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(alias));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));
            var id = Guid.NewGuid();

            return source
                .AddParameter(id, name, description, handler, true, valueName)
                .AddParameter(id, alias, description, handler, true, valueName);
        }

        #endregion Parameters

        #region Options
        public static ParameterProvider Option<T>(this T source, string name, Action handler, string description)
            where T : ParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            return source.AddOption(Guid.NewGuid(), name, description, handler);
        }

        public static ParameterProvider Option<T>(this T source, string name, string alias, Action handler, string description)
            where T : ParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(alias))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(alias));
            var id = Guid.NewGuid();
            return source
                .AddOption(id, name, description, handler)
                .AddOption(id, alias, description, handler);
        }

        #endregion Options

        #region ParametersWithOptions

        public static ParameterProvider Parameter<T>(this T source, string name, Action<string> parameterHandler, Action optionHandler, bool isDefault, string description, string valueName = null)
            where T : ParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (parameterHandler == null) throw new ArgumentNullException(nameof(parameterHandler));
            if (optionHandler == null) throw new ArgumentNullException(nameof(optionHandler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));

            var id = Guid.NewGuid();
            return source
                .AddParameter(id, name, description, parameterHandler, isDefault, valueName)
                .AddOption(id, name, description, optionHandler);
        }

        public static ParameterProvider Parameter<T>(this T source, string name, string alias, Action<string> parameterHandler, Action optionHandler, bool isDefault, string description, string valueName = null)
            where T : ParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (parameterHandler == null) throw new ArgumentNullException(nameof(parameterHandler));
            if (optionHandler == null) throw new ArgumentNullException(nameof(optionHandler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(alias))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(alias));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));

            var id = Guid.NewGuid();
            return source
                .AddParameter(id, name, description, parameterHandler, isDefault, valueName)
                .AddParameter(id, alias, description, parameterHandler, isDefault, valueName)
                .AddOption(id, name, description, optionHandler)
                .AddOption(id, alias, description, optionHandler);
        }


        public static ParameterProvider Parameter<T>(this T source, string name, Action<string> parameterHandler, Action optionHandler, string description, string valueName = null)
            where T : ParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (parameterHandler == null) throw new ArgumentNullException(nameof(parameterHandler));
            if (optionHandler == null) throw new ArgumentNullException(nameof(optionHandler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));

            var id = Guid.NewGuid();
            return source
                .AddParameter(id, name, description, parameterHandler, false, valueName)
                .AddOption(id, name, description, optionHandler);
        }

        public static ParameterProvider Parameter<T>(this T source, string name, string alias, Action<string> parameterHandler, Action optionHandler, string description, string valueName = null)
            where T : ParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (parameterHandler == null) throw new ArgumentNullException(nameof(parameterHandler));
            if (optionHandler == null) throw new ArgumentNullException(nameof(optionHandler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(alias))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(alias));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));

            var id = Guid.NewGuid();
            return source
                .AddParameter(id, name, description, parameterHandler, false, valueName)
                .AddParameter(id, alias, description, parameterHandler, false, valueName)
                .AddOption(id, name, description, optionHandler)
                .AddOption(id, alias, description, optionHandler);
        }

        public static ParameterProvider DefaultParameter<T>(this T source, string name, Action<string> parameterHandler, Action optionHandler, string description, string valueName = null)
            where T : ParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (parameterHandler == null) throw new ArgumentNullException(nameof(parameterHandler));
            if (optionHandler == null) throw new ArgumentNullException(nameof(optionHandler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));

            var id = Guid.NewGuid();
            return source
                .AddParameter(id, name, description, parameterHandler, true, valueName)
                .AddOption(id, name, description, optionHandler);
        }

        public static ParameterProvider DefaultParameter<T>(this T source, string name, string alias, Action<string> parameterHandler, Action optionHandler, string description, string valueName = null)
            where T : ParameterProvider
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (parameterHandler == null) throw new ArgumentNullException(nameof(parameterHandler));
            if (optionHandler == null) throw new ArgumentNullException(nameof(optionHandler));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(alias))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(alias));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(description));

            var id = Guid.NewGuid();
            return source
                .AddParameter(id, name, description, parameterHandler, true, valueName)
                .AddParameter(id, alias, description, parameterHandler, true, valueName)
                .AddOption(id, name, description, optionHandler)
                .AddOption(id, alias, description, optionHandler);
        }

        #endregion Parameters

    }
}