using System.Text.RegularExpressions;

using Lab2.ConstantsSets;
using Lab2.Validation.ConstantsSets;

namespace Lab2.Validation;

public static partial class DataValidator
{
    private const int SuccessfulExitCode = 0;


    [GeneratedRegex(@"^(\d?\d):(\d?\d):(\d?\d)$")]
    private static partial Regex TimeFormatRegex();


    public delegate string? ValidatorCallback();


    public static void Validate(ValidatorCallback callback)
    {
        string? errorMessage = callback();

        if (errorMessage != null)
        {
            Console.WriteLine(errorMessage);
            Environment.Exit(SuccessfulExitCode);
        }
    }

    public static string? ValidateInputFile(string inputFilePath)
    {
        if (!File.Exists(inputFilePath))
        {
            return $"Input file not found at {Path.GetFullPath(inputFilePath)}.";
        }

        return null;
    }

    public static string? ValidateNumberOfTimesLine(string line)
    {
        if (!int.TryParse(line, out int _))
        {
            return "Number of times must be an integer.";
        }

        return null;
    }

    public static string? ValidateNumberOfTimes(int times)
    {
        if (times > TimeConfig.MaxTimes)
        {
            return "Number of times must be less than or equal to 50000.";
        }

        return null;
    }

    public static string? ValidateTimes(string[] times, int numberOfTimes)
    {
        if (times.Length != numberOfTimes)
        {
            return "Number of times does not match the specified number of times.";
        }

        Regex timeFormatRegex = TimeFormatRegex();

        foreach (string time in times)
        {
            if (!timeFormatRegex.IsMatch(time))
            {
                return $"Invalid time format: {time}. Expected format is HH:MM:SS.";
            }

            int[] timeParts = time.Split(":").Select(int.Parse).ToArray();
            int hours = timeParts[TimePartIndex.Hours];
            int minutes = timeParts[TimePartIndex.Minutes];
            int seconds = timeParts[TimePartIndex.Seconds];

            if (hours < TimeConfig.MinHours || hours > TimeConfig.MaxHours)
            {
                return $"Invalid time value: {time}. Hours should be between 1 and 12.";
            }

            if (minutes < TimeConfig.MinMinutes || minutes > TimeConfig.MaxMinutes)
            {
                return $"Invalid time value: {time}. Minutes should be between 0 and 59.";
            }

            if (seconds < TimeConfig.MinSeconds || seconds > TimeConfig.MaxSeconds)
            {
                return $"Invalid time value: {time}. Seconds should be between 0 and 59.";
            }
        }


        return null;
    }
}