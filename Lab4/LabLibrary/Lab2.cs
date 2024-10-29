using Lab2;

namespace LabLibrary;

public class Lab2 : LabBase
{
    protected override string LabName => "Lab2";
    public override void Run(string inputFile, string outputFile)
    {
        Console.WriteLine($"Run {LabName}");

        string optimalTime = Program.ProcessData(inputFile);
        File.WriteAllText(outputFile, optimalTime);
    }
}