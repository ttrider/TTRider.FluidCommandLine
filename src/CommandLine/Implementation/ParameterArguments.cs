namespace TTRider.FluidCommandLine.Implementation
{
    public class ParameterArguments : ParameterItem, IParameterProvider
    {
        private readonly ParameterSet owner;

        internal ParameterArguments(ParameterSet owner)
        {
            this.owner = owner;
        }

        ParameterSet IParameterProvider.ParameterSet => owner;
        ParameterFactory IParameterProvider.ParameterFactory => ((IParameterProvider)owner).ParameterFactory;

    }
}