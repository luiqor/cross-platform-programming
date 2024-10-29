using Lab3;

namespace LabLibrary;

public class Lab3 : LabBase
{
    protected override string LabName => "Lab3";
    public override void Run(string inputFile, string outputFile)
    {
        Console.WriteLine($"Run {LabName}");

        int bestTimeToPrincess = Program.ProcessData(inputFile);
        File.WriteAllText(outputFile, bestTimeToPrincess.ToString());
    }
}