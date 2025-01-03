
using System.Runtime.InteropServices;
using McMaster.Extensions.CommandLineUtils;

using Lab4.Utils;


namespace Lab4.Commands;

[Command(Name = "set-path", Description = "Set path to input/output files")]
public class SetPathCommand
{
    [Option(Description = "Set LAB_PATH env var", ShortName = "p", LongName = "path")]
    public required string Path { get; set; }

    private void OnExecute(IConsole console)
    {
        if (string.IsNullOrEmpty(Path))
        {
            console.WriteLine("Please specify a valid path.");
            return;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            UnixEnvironmentUtil.SetEnvironmentVariableUnix("LAB_PATH", Path);
        }
        else
        {
            Environment.SetEnvironmentVariable("LAB_PATH", Path, EnvironmentVariableTarget.Machine);
        }

        console.WriteLine($"LAB_PATH set to: {Path}");
    }
}