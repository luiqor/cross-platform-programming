using Lab3.ConstantsSets;

using CrossPlatformProgramming.Lab3.Validation.ConstantsSets;

namespace Lab3.Validation;

public static partial class DataValidator
{
    private const int LastIndex = 1;
    private const int ComputerOffset = 1;


    public delegate string? ValidatorCallback();


    public static void Validate(ValidatorCallback callback)
    {
        string? errorMessage = callback();

        if (errorMessage != null)
        {
            Console.WriteLine(errorMessage);
            Environment.Exit(EnviromentCode.SuccessfulExit);
        }
    }

    public static string? ValidateInputFile(string inputFilePath)
    {
        return !File.Exists(inputFilePath) ? $"Input file not found at {Path.GetFullPath(inputFilePath)}." : null;
    }

    public static string? ValidateNumberOfLines(string[] lines)
    {
        return lines.Length == 0 ? "File is empty." : null;
    }

    public static string? ValidateLabyrinthDimensions(int[] dimensions)
    {
        if (dimensions.Length != Dimension.TotalCount)
        {
            return "Invalid format of the first line. Expected three numbers separated by spaces.";
        }

        if (dimensions.Any(dimension => dimension < Dimension.Min || dimension > Dimension.Max))
        {
            return "Invalid labyrinth dimensions. Expected 2 ≤ h, m, n ≤ 50";
        }

        return null;
    }

    public static string? ValidateLabyrinthLineLength(string line, int expectedLength)
    {
        return line.Length != expectedLength ? $"Invalid length of the line. Expected {expectedLength} characters." : null;
    }


    public static string? ValidateLevelRowsCount(int nextLineIndex, int totalLines, int i)
    {
        return nextLineIndex > totalLines ? $"Not enough rows for level {i + ComputerOffset}" : null;
    }

    public static string? ValidateLevelRowColumsCount(int lineElementCount, int n, int i, int j)
    {
        return lineElementCount != n ? $"Incorrect row length at level {i + ComputerOffset}, row {j + ComputerOffset}" : null;
    }

    public static string? ValidateEmptyLineAfterLevel(int i, int h, int lineIndex, string[] lines)
    {
        return i < h - LastIndex && lineIndex < lines.Length && !string.IsNullOrWhiteSpace(lines[lineIndex])
            ? $"Missing empty line after level {i + ComputerOffset}"
            : null;
    }
}