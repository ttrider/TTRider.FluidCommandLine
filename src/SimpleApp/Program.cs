using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTRider.FluidCommandLine;

namespace SimpleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var p = new Program();
            

            var cmdlA = GetCommandLine_Arguments();
            p.Test_Arguments(cmdlA);

            //var cmd2 = CommandLine
            //                .Switch("name", "description", Occurance.Multiple)
            //                    .Flag('f')
            //                    .Option("foo")
            //                .Switch("name", Occurance.Multiple)
            //                    .Flag('f')
            //                    .Option("foo")
            //                .Arguments("files")
            //                .Command("commandname", "description", true)
            //                    .Switch("dd")
            //                        .Flag('f')
            //                .End();

            //var a = cmd2.Bind("");





        }

        public static ICommandLine GetCommandLine_Arguments()
        {
            return CommandLine.Arguments();
        }

        public void Test_Arguments(ICommandLine cmd)
        {
            var cmdLine = cmd.Bind();
            Assert.IsNotNull(cmdLine);
            Assert.AreEqual(null, cmdLine.Command);
        }




    }

    class Assert
    {
        public static void AreEqual(object expected, object actual)
        {
            if (!object.Equals(expected, actual))
            {
                throw new InvalidProgramException($"Expected: {expected}; Actual: {actual}");
            }
        }
        public static void IsNotNull(object actual)
        {
            if (actual==null)
            {
                throw new InvalidProgramException($"Unexpected null");
            }
        }
    }
}
