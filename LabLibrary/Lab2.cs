using Lab2;

namespace LabLibrary;

public class Lab2 : LabBase
{
    public override void Run(string inputFile, string outputFile)
    {
        string optimalTime = Program.ProcessData(inputFile);
        File.WriteAllText(outputFile, optimalTime);
    }
}