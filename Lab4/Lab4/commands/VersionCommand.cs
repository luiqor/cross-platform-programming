using System.Xml.Linq;

using McMaster.Extensions.CommandLineUtils;

namespace Lab4.Commands;

[Command(Name = "version", Description = "Show version info")]
public class VersionCommand
{
    private void OnExecute(IConsole console)
    {
        string csprojPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Lab4.csproj");
        if (!File.Exists(csprojPath))
        {
            console.WriteLine("Project file not found.");
            return;
        }

        XDocument csproj = XDocument.Load(csprojPath);
        XElement? propertyGroup = csproj.Descendants("PropertyGroup").FirstOrDefault();


        Console.WriteLine($"Author: {propertyGroup?.Element("Authors")?.Value ?? "https://github.com/luiqor"}");
        Console.WriteLine($"Version: {propertyGroup?.Element("Version")?.Value ?? "?.?.?"}");
    }
}