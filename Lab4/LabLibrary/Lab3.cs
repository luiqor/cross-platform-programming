using Lab3;

namespace LabLibrary;

public class Lab3 : LabBase
{
    public override void Run(string inputFile, string outputFile)
    {
        int bestTimeToPrincess = Program.ProcessData(inputFile);
        File.WriteAllText(outputFile, bestTimeToPrincess.ToString());
    }
}