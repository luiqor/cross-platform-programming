using Lab2.Services;

namespace Lab2;

class Program
{
    private const int NumberOfTimesLineIndex = 0;
    private const int LinesToSkipBeforeTimes = 1;


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

        int numberOfTimes = int.Parse(input[NumberOfTimesLineIndex]);
        string[] times = input.Skip(LinesToSkipBeforeTimes).Take(numberOfTimes).ToArray();

        return CalculateTimeService.CalculateMinimumAdjustmentTime(times);
    }
}