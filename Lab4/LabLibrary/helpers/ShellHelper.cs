
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LabLibrary.Helpers;
public class ShellHelper
{

    public static void Execute(string command)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            RunShellCommand("cmd.exe", $"/c {command}");
        }
        else
        {
            RunShellCommand("/bin/bash", $"-c {command}");
        }
    }

    private static void RunShellCommand(string shell, string arguments)
    {
        ProcessStartInfo processInfo = new()
        {
            FileName = shell,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using Process? process = Process.Start(processInfo);

        if (process == null)
        {
            Console.WriteLine("Failed to start process.");
            return;
        }

        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        Console.WriteLine(output);
    }
}