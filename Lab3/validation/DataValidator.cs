using Lab3.ConstantsSets;

namespace Lab3.Validation;

public static partial class DataValidator
{

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
        if (dimensions.Length != 3)
        {
            return "Invalid format of the first line. Expected three numbers separated by spaces.";
        }

        if (dimensions[0] < 2 || dimensions[1] < 1 || dimensions[2] < 1 || dimensions[0] > 50 || dimensions[1] > 50 || dimensions[2] > 50)
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
        return nextLineIndex > totalLines ? $"Not enough rows for level {i + 1}" : null;
    }

    public static string? ValidateLevelRowColumsCount(int lineElementCount, int n, int i, int j)
    {
        return lineElementCount != n ? $"Incorrect row length at level {i + 1}, row {j + 1}" : null;
    }

    public static string? ValidateEmptyLineAfterLevel(int i, int h, int lineIndex, string[] lines)
    {
        return i < h - 1 && lineIndex < lines.Length && !string.IsNullOrWhiteSpace(lines[lineIndex])
            ? $"Missing empty line after level {i + 1}"
            : null;
    }
}