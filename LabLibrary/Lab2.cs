using Lab2;

namespace LabLibrary;

public class Lab2 : LabBase
{
    public override string Run(string input)
    {
        string optimalTime = Program.ProcessData(input);
        return optimalTime;
    }
}