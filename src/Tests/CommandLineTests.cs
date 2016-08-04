using TTRider.FluidCommandLine;
using TTRider.FluidCommandLine.Implementation;
using Xunit;

namespace Tests
{
    public class CommandLineTests
    {
        [Fact]
        public void CommandNew()
        {
            TestManager tm = TestManager.Instance;
            tm.cmd.Build("new");
            Assert.Equal("New command", tm.TestResult);
        }

        [Fact]
        public void CommandNewWithOptions()
        {
            TestManager tm = TestManager.Instance;
            tm.cmd.Build("new -t");
            Assert.Equal("t", TestManager.NewType);

            tm.cmd.Build("new --type");
            Assert.Equal("type", TestManager.NewType);

            Assert.Equal("New command", tm.TestResult);

            Assert.Throws<UnknownOptionException>(() => tm.cmd.Build("new --unknown"));
        }

        [Fact]
        public void CommandNewWithParams()
        {
            TestManager tm = TestManager.Instance;
            tm.cmd.Build("new -o d:\\temp");
            Assert.Equal("d:\\temp", TestManager.PathToDirectory);

            tm.cmd.Build("new --output d:\\temp");
            Assert.Equal("d:\\temp", TestManager.PathToDirectory);

            Assert.Equal("New command", tm.TestResult);
            Assert.Throws<UnknownOptionException>(() => tm.cmd.Build("new --unknown"));
        }

        [Fact]
        public void CommandNewWithOptionsValue()
        {
            TestManager tm = TestManager.Instance;
            tm.cmd.Build("new -t project");
            Assert.Equal("t", TestManager.NewType);
            Assert.Equal("project", TestManager.NewTypeValue);

            tm.cmd.Build("new -t proj");
            Assert.Equal("New command", tm.TestResult);
            Assert.Equal("t", TestManager.NewType);
            Assert.Equal("proj", TestManager.NewTypeValue);

            tm.cmd.Build("new --type storage");
            Assert.Equal("New command", tm.TestResult);
            Assert.Equal("type", TestManager.NewType);
            Assert.Equal("storage", TestManager.NewTypeValue);

            tm.cmd.Build("new --type facet");
            Assert.Equal("New command", tm.TestResult);
            Assert.Equal("type", TestManager.NewType);
            Assert.Equal("facet", TestManager.NewTypeValue);

            tm.cmd.Build("new -t project");
            Assert.Equal("New command", tm.TestResult);
            Assert.Equal("t", TestManager.NewType);
            Assert.Equal("project", TestManager.NewTypeValue);
            //tm.cmd.Build("new -o");
            //Assert.Equal("o", TestManager.NewType);

            //tm.cmd.Build("new --output");
            //Assert.Equal("output", TestManager.NewType);

            Assert.Equal("New command", tm.TestResult);

            Assert.Throws<UnknownOptionException>(() => tm.cmd.Build("new --unknown"));
        }

        [Fact]
        public void CommandBuild()
        {
            TestManager tm = TestManager.Instance;
            tm.cmd.Build("build");
            Assert.Equal("Build command", tm.TestResult);
        }

        [Fact]
        public void CommandPublish()
        {
            TestManager tm = TestManager.Instance;
            tm.cmd.Build("publish");
            Assert.Equal("Publish command", tm.TestResult);
        }
        
        [Fact]
        public void CommandTest()
        {
            TestManager tm = TestManager.Instance;
            tm.cmd.Build("test");
            Assert.Equal("Test command", tm.TestResult);
        }

        [Fact]
        public void CommandUnknown()
        {
            TestManager tm = TestManager.Instance;
            Assert.Throws<UnknownCommandException>(()=> tm.cmd.Build("unknown"));
        }

        public void CommandNewWithType1()
        {
            TestManager tm = TestManager.Instance;
            tm.cmd.Build("new -t project --type proj");
            Assert.Equal("project", TestManager.NewType);
        }
    }
}
