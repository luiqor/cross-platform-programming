using Lab3;

namespace LabLibrary;

public class Lab3 : LabBase
{
    public override string Run(string input)
    {
        int bestTimeToPrincess = Program.ProcessData(input);
        return bestTimeToPrincess.ToString();
    }
}