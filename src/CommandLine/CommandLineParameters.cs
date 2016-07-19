using System.Collections.Generic;
using System.Linq;

namespace TTRider.FluidCommandLine
{
    public class CommandLineParameters
    {

        internal CommandLineParameters(string command, HashSet<string> switches, IDictionary<string, List<string>> parameters, IEnumerable<string> arguments)
        {
            this.Arguments = arguments.ToList();
            this.Command = command;
            this.Switches = switches;
            this.Parameters = parameters;
        }

        public string Command { get; }
        public HashSet<string> Switches { get; }
        public IDictionary<string, List<string>> Parameters { get; }
        public IReadOnlyList<string> Arguments { get; }

    }
}