using Lab1;

namespace LabLibrary;

public class Lab1 : LabBase
{
    public override string Run(string input)
    {
        int processedData = Program.ProcessData(input);
        return processedData.ToString();
    }
}