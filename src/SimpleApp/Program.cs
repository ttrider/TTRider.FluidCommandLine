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

       



    }


}
