using Lab1;

namespace LabLibrary;

public class Lab1 : LabBase
{
    protected override string LabName => "Lab1";
    public override void Run(string inputFile, string outputFile)
    {
        Console.WriteLine($"Run {LabName}");

        int processedData = Program.ProcessData(inputFile);
        File.WriteAllText(outputFile, processedData.ToString());
    }
}