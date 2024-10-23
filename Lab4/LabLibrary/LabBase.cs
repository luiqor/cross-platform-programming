

using LabLibrary.Helpers;

namespace LabLibrary;

public abstract class LabBase
{
    protected abstract string LabName { get; }

    public void Build()
    {
        Console.WriteLine($"Build {LabName}");
        ShellHelper.Execute($"dotnet build ../../build.proj -p:Solution={LabName} -t:Build");
    }

    public void Test()
    {
        Console.WriteLine($"Test {LabName}");
        ShellHelper.Execute($"dotnet build ../../build.proj -p:Solution={LabName} -t:Test");
    }

    public void Run(string inputFile, string outputFile)
    {
        Console.WriteLine($"Run {LabName}");

        string appDefinedInput = $"../../{LabName}/input.txt";
        File.Copy(inputFile, appDefinedInput, overwrite: true);

        ShellHelper.Execute($"dotnet build ../../build.proj -p:Solution={LabName} -t:Run");

        string appDefinedOutput = $"../../{LabName}/output.txt";
        File.Copy(appDefinedOutput, outputFile, overwrite: true);
    }
}
