using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTRider.FluidCommandLine
{
    public class test
    {

        enum commands
        {
            Build,
            Test,
            Setup
        }
        static void Run()
        {
            string buildInputFile;
            string signKey;
            bool isSQL = true;
            string sqlcs;
            bool isPostgres = false;
            commands c=commands.Build;


            var cmd = CommandLine
                        .Parameter("path to *.acproj file", s => buildInputFile = s)
                            .Flag('i').Option("input")
                        .Command("build", "build project",()=>c=commands.Build, true)
                            .Switch("generate scripts to create SQLServer database objects", () => isSQL = true)
                                .Option("sql")
                            .Switch("generate scripts to create Postgres database objects", () => isPostgres = true)
                                .Option("postgres")
                            .Parameter("private key used to sign solutions", s => signKey = s)
                                .Flag('k').Option("key")
                        .Command("setup", "setup local project", () => c = commands.Setup)
                            .Parameter("setup SQLServer database", s => sqlcs = s)
                                .Option("sql")
                            .Parameter("setup Postgres database", s => sqlcs = s)
                                .Option("postgres")
                        .Command("test", "test project",()=>c = commands.Test)
                            .Parameter("private key used to sign solutions", s => signKey = s)
                                .Flag('k').Option("key")
                        .End()

                            ;


            cmd.Build("build -i file");

        }
    }
}
