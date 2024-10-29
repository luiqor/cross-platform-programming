using Lab1;

namespace LabLibrary;

public class Lab1 : LabBase
{
    public override void Run(string inputFile, string outputFile)
    {
        int processedData = Program.ProcessData(inputFile);
        File.WriteAllText(outputFile, processedData.ToString());
    }
}