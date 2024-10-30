using McMaster.Extensions.CommandLineUtils;

using VersionCommand = Lab4.Commands.VersionCommand;
using RunCommand = Lab4.Commands.RunCommand;
using SetPathCommand = Lab4.Commands.SetPathCommand;

namespace Lab4;

[Command(Name = "labtool", Description = "Tool for running lab assignments")]
[Subcommand(typeof(VersionCommand), typeof(SetPathCommand), typeof(RunCommand))]
public class Program
{
    public static int Main(string[] args)
    {
        return CommandLineApplication.Execute<Program>(args);
    }

    public void OnExecute(CommandLineApplication app)
    {
        app.ShowHelp();
    }
}