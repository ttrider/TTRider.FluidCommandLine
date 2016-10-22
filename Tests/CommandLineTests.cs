using System.Collections.Generic;
using TTRider.FluidCommandLine;
using Xunit;

namespace Tests
{
    public class CommandLineTests
    {
        readonly BuildCommand buildCommand = new BuildCommand();
        bool executeBuild;
        readonly BuildCommand testCommand = new BuildCommand();
        bool executeTest;
        private readonly ICommandLine commandLine;

        class BuildCommand
        {
            public List<string> Inputs { get; } = new List<string>();
            public bool OFlag { get; set; }
            public bool OutputFlag { get; set; }
        }

        public CommandLineTests()
        {
            commandLine = CommandLine
                .Command("build", () => executeBuild = true, true, "Build something")
                    .Parameter("i", input => buildCommand.Inputs.Add(input), "input")
                    .Parameter("input", input => buildCommand.Inputs.Add(input), true, "input")
                    .Option("input", () => buildCommand.Inputs.Add(""), "enable input")
                    .Option("o", () => buildCommand.OFlag = true, "enable output")
                    .Option("output", () => buildCommand.OutputFlag = true, "enable output")

                .Command("test", () => executeTest = true, "Test something")
                    .Parameter("i", input => testCommand.Inputs.Add(input), "input")
                    .Parameter("input", input => testCommand.Inputs.Add(input), "input")
                    .Option("o", () => testCommand.OFlag = true, "enable output")
                    .Option("output", () => testCommand.OutputFlag = true, "enable output")
                .End();
        }


        [Fact]
        public void Complete()
        {
            commandLine.Run("build", "-i", "input_one", "--input", "input_two", "-o", "--output");

            Assert.True(executeBuild);
            Assert.False(executeTest);

            Assert.True(buildCommand.OFlag);
            Assert.True(buildCommand.OutputFlag);
            Assert.Equal(new[] { "input_one", "input_two" }, buildCommand.Inputs);
        }

        [Fact]
        public void DefaultCommand()
        {
            commandLine.Run("-i", "input_one", "--input", "input_two", "-o", "--output");

            Assert.True(executeBuild);
            Assert.False(executeTest);

            Assert.True(buildCommand.OFlag);
            Assert.True(buildCommand.OutputFlag);
            Assert.Equal(new[] { "input_one", "input_two" }, buildCommand.Inputs);
        }

        [Fact]
        public void DefaultParameter()
        {
            commandLine.Run("build", "-i", "input_one", "-o", "--output", "input_two");

            Assert.True(executeBuild);
            Assert.False(executeTest);

            Assert.True(buildCommand.OFlag);
            Assert.True(buildCommand.OutputFlag);
            Assert.Equal(new[] { "input_one", "input_two" }, buildCommand.Inputs);
        }

        [Fact]
        public void MissingParameterValue()
        {
            commandLine.Run("build", "-i", "input_one", "--input", "-o", "--output");

            Assert.True(executeBuild);
            Assert.False(executeTest);

            Assert.True(buildCommand.OFlag);
            Assert.True(buildCommand.OutputFlag);
            Assert.Equal(new[] { "input_one", "" }, buildCommand.Inputs);
        }

        [Fact]
        public void MissingParameterName()
        {
            commandLine.Run("build", "input_one");

            Assert.True(executeBuild);
            Assert.False(executeTest);

            Assert.Equal(new[] { "input_one" }, buildCommand.Inputs);
        }

        [Fact]
        public void MissingCommandName()
        {
            commandLine.Run("input_one");

            Assert.True(executeBuild);
            Assert.False(executeTest);

            Assert.Equal(new[] { "input_one" }, buildCommand.Inputs);
        }
    }
}
