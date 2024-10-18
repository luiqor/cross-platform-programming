using McMaster.Extensions.CommandLineUtils;

namespace Lab4.Commands;

[Command(Name = "version", Description = "Show version info")]
class VersionCommand
{
    private void OnExecute(IConsole console)
    {
        Console.WriteLine("Author: https://github.com/luiqor");
        Console.WriteLine("Version: 1.0.0");
    }
}