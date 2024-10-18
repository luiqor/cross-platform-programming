using McMaster.Extensions.CommandLineUtils;

using VersionCommand = Lab4.Commands.VersionCommand;

namespace Lab4;

[Command(Name = "labtool", Description = "Tool for running lab assignments")]
[Subcommand(typeof(VersionCommand))]
class Program
{
    public static int Main(string[] args)
    {
        return CommandLineApplication.Execute<Program>(args);
    }

    private void OnExecute(CommandLineApplication app)
    {
        app.ShowHelp();
    }
}