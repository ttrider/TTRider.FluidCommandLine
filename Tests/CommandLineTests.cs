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

        [Fact]
        public void AC()
        {
            var commandLine = CommandLine
              .Help("h")
              .Option("v", () => { }, "verbose output")
              .DefaultCommand("build", () => { }, "Build a project")
                .DefaultParameter("i", (p) => { }, "path to the acproj.json")
                .Option("fk", () => { }, "generate FOREIGN KEYs")
                .Option("doc", () => { }, "generate documentation")
                .Parameter("metadata", "m", str =>
                {
                }, () => { },
                    "compile metadata", "output directory for metadata xml file")
                .Parameter("sql", str =>
                {

                }, () => { },
                    "compile SQLServer scripts", "output directory for the SQLServer files")

                .Parameter("postgres", str =>
                {
                }, () => { },
                    "compile Portgres scripts", "output directory for the Portgres files")


            .Command("install", () => { }, "Install a project")
                .DefaultParameter("i", str => { }, "path to the acproj.json")
                .Option("fk", () => { }, "create FOREIGN KEYs")
                .Option("data", "d", () => {  }, "install data")
                .Parameter("sql", str =>
                {
                }, "install SQLServer schema", "SQLServer connection string")
                .Parameter("postgres", str =>
                {
                }, "install Postgres schema", "Postgres connection string")
            .End();

            var args = new []
            {
                "install",
                "--data",
                "--sql",
                "Server=tcp:w90cnqf7o5.database.windows.net,1433;Initial Catalog=ac-pcc-2016;Persist Security Info=False;User ID=river;Password=$martM0ney;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
                "-i",
                @"..\..\..\Solutions\PopulationHealth"
            };

            commandLine.Run(args);
        }


    }
}
