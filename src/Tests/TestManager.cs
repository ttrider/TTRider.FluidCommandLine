using System;
using System.Threading;
using TTRider.FluidCommandLine;

namespace Tests
{
    public class TestManager
    {
        public static string NewType = string.Empty;
        public static string NewTypeValue = string.Empty;
        public static string PathToDirectory = string.Empty;

        public static TestManager Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public ICommandLine cmd
        {
            get
            {
                return command;
            }
        }

        public string TestResult {
            get
            {
                return testResult;
            }
        }

        private static void initClass()
        {
            command = CommandLine
                .Command("new", "Creates a new project", () => New())
                    .Option("t", () => NewType = "t")
                        .OptionValue("proj", () => NewTypeValue = "proj")
                        .OptionValue("project", () => NewTypeValue = "project")
                        .OptionValue("storage", () => NewTypeValue = "storage")
                        .OptionValue("facet", () => NewTypeValue = "facet")
                    .Option("type", () => NewType = "type")
                        .OptionValue("proj", () => NewTypeValue = "proj")
                        .OptionValue("project", () => NewTypeValue = "project")
                        .OptionValue("storage", () => NewTypeValue = "storage")
                        .OptionValue("facet", () => NewTypeValue = "facet")
                    .Parameter("o", (string str)=> PathToDirectory = str)
                    .Parameter("output", (string str) => PathToDirectory = str)
                .Command("build", "Build a project", () => Build())
                .Command("publish", "Publish a project", () => Publish())
                .Command("test", "Test a project", () => Test())
                .End();
        }

        private static void New()
        {
            testResult = "New command";
        }

        private static void Build()
        {
            testResult = "Build command";
        }

        private static void Publish()
        {
            testResult = "Publish command";
        }

        private static void Test()
        {
            testResult = "Test command";
        }
        private static string testResult;
        private static ICommandLine command;
        private static Lazy<TestManager> instance = new Lazy<TestManager>(() => { var obj = new TestManager(); initClass(); return obj; }, LazyThreadSafetyMode.ExecutionAndPublication);
    }
}
