using System.Diagnostics;

namespace Lab4.Utils;

public class UnixEnvironmentUtil
{
    private const string EnvironmentFilePath = "/etc/environment";
    private const string ProfileScriptPath = "/etc/profile.d/";

    private static string? GetVariableFromBash(string name)
    {
        try
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    Arguments = $"-c 'echo ${name}'",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();

            return string.IsNullOrEmpty(result) ? null : result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading environment variable from Bash: {ex.Message}");
            return null;
        }
    }

    public static void SetEnvironmentVariableUnix(string name, string value)
    {
        if (File.Exists(EnvironmentFilePath))
        {
            var lines = File.ReadAllLines(EnvironmentFilePath);
            bool variableExists = false;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith($"{name}="))
                {
                    lines[i] = $"{name}=\"{value}\"";
                    variableExists = true;
                    break;
                }
            }

            if (!variableExists)
            {
                using var writer = File.AppendText(EnvironmentFilePath);
                writer.WriteLine($"{name}=\"{value}\"");
            }
            else
            {
                File.WriteAllLines(EnvironmentFilePath, lines);
            }
        }
        else
        {
            // If /etc/environment is not avalible --> /etc/profile.d
            string scriptFilePath = Path.Combine(ProfileScriptPath, $"{name.ToLower()}_env.sh");
            string scriptContent = $"export {name}=\"{value}\"";

            File.WriteAllText(scriptFilePath, scriptContent);
        }

        Console.WriteLine($"Environment variable {name} set to: {value}");
    }

    public static string? GetEnvironmentVariableUnix(string name)
    {
        // 1. Try to read from Environment
        string? value = Environment.GetEnvironmentVariable(name);
        if (!string.IsNullOrEmpty(value))
        {
            return value;
        }

        // 2. Try to read from /etc/environment
        if (File.Exists(EnvironmentFilePath))
        {
            foreach (var line in File.ReadLines(EnvironmentFilePath))
            {
                if (line.StartsWith($"{name}="))
                {
                    return line.Substring($"{name}=".Length).Trim('"');
                }
            }
        }

        // 3. Try to read from /etc/profile.d
        return GetVariableFromBash(name);
    }

}
