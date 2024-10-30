
using McMaster.Extensions.CommandLineUtils;
using DotNetEnv;

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

        const string envFilePath = ".env";
        Env.Load(envFilePath);

        UpdateEnvFile(envFilePath, "LAB_PATH", Path);
        console.WriteLine($"LAB_PATH set to: {Path}");
    }

    private void UpdateEnvFile(string filePath, string key, string value)
    {
        if (!File.Exists(filePath))
        {
            using (File.Create(filePath)) { }
        }

        var lines = File.ReadAllLines(filePath);
        bool keyFound = false;

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith(key + "="))
            {
                lines[i] = $"{key}={value}";
                keyFound = true;
                break;
            }
        }

        if (!keyFound)
        {
            var newEntry = $"{key}={value}";
            File.AppendAllLines(filePath, [newEntry]);
        }
        else
        {
            File.WriteAllLines(filePath, lines);
        }
    }
}