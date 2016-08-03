using TTRider.FluidCommandLine.Implementation;

namespace TTRider.FluidCommandLine
{
    public interface IParameterProvider
    {
        ParameterSet ParameterSet { get; }
        ParameterFactory ParameterFactory { get; }
    }
}