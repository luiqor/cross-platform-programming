using Lab2.Services;
using Lab2.Validation;

namespace Lab2;

public static class Program
{
    private const int NumberOfTimesLineIndex = 0;
    private const int LinesToSkipBeforeTimes = 1;


    static void Main()
    {
        string inputFilePath = Path.Combine("input.txt");
        string outputFilePath = Path.Combine("output.txt");

        string optimalTime = ProcessData(inputFilePath);

        File.WriteAllText(outputFilePath, optimalTime);
    }

    public static string ProcessData(string inputFilePath)
    {
        string[] input = File.ReadAllLines(inputFilePath);
        DataValidator.Validate(() => DataValidator.ValidateInputFile(inputFilePath));

        string numberOfTimesLine = input[NumberOfTimesLineIndex];
        DataValidator.Validate(() => DataValidator.ValidateNumberOfTimesLine(numberOfTimesLine));

        int numberOfTimes = int.Parse(numberOfTimesLine);
        DataValidator.Validate(() => DataValidator.ValidateNumberOfTimes(numberOfTimes));

        string[] times = input.Skip(LinesToSkipBeforeTimes)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .ToArray();
        DataValidator.Validate(() => DataValidator.ValidateTimes(times, numberOfTimes));

        return CalculateTimeService.CalculateMinimumAdjustmentTime(times);
    }
}