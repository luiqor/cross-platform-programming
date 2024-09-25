using Lab2.Services;

namespace Lab2;

class Program
{
    static void Main()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string inputFilePath = Path.Combine(baseDirectory, "assets", "lab2", "input.txt");
        string outputFilePath = Path.Combine(baseDirectory, "assets", "lab2", "output.txt");

        string optimalTime = ProcessData(inputFilePath);

        File.WriteAllText(outputFilePath, optimalTime);
    }

    public static string ProcessData(string inputFilePath)
    {
        string[] input = File.ReadAllLines(inputFilePath);

        int numberOfTimes = int.Parse(input[0]);
        string[] times = input.Skip(1).Take(numberOfTimes).ToArray();

        return CalculateTimeService.CalculateMinimumAdjustmentTime(times);
    }
}