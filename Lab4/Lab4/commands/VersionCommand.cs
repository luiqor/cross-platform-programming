using System.Reflection;

using McMaster.Extensions.CommandLineUtils;

namespace Lab4.Commands;

[Command(Name = "version", Description = "Show version info")]
public class VersionCommand
{
    private void OnExecute(IConsole console)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        var version = assembly.GetName()?.Version?.ToString() ?? "?.?.?";

        Console.WriteLine("Author: https://github.com/luiqor");
        Console.WriteLine($"Version: {version}");
    }
}