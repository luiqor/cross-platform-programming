using Lab1.Validation.ConstantsSets;

namespace Lab1.Validation;

public static class DataValidator
{
    private const int SuccessfulExitCode = 0;
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

    public static string? ValidateFiles(string inputFilePath, string outputFilePath)
    {
        if (!File.Exists(inputFilePath))
        {
            return $"Input file not found at {Path.GetFullPath(inputFilePath)}.";
        }

        if (File.Exists(outputFilePath) && Path.GetFullPath(inputFilePath) == Path.GetFullPath(outputFilePath))
        {
            return "Input and output files must be different.";
        }

        return null;
    }

    public static string? ValidatePermutationData(int totalElementsCount, int placementLength, int[] placementElements)
    {
        if (totalElementsCount < placementLength)
        {
            return "The number of elements in the set must be greater than or equal to the length of the permutation.";
        }

        if (placementLength <= PermutationConfig.MinValue || totalElementsCount >= PermutationConfig.MaxValue)
        {
            return $"The length of the permutation must be less than or equal to {PermutationConfig.MinValue} and less than or equal to {PermutationConfig.MaxValue}.";
        }

        if (placementElements.Length != placementLength)
        {
            return "The number of elements in the permutation must be equal to the length of the permutation.";
        }

        if (placementElements.Any((elementValue) => elementValue < PermutationConfig.MinPlacementElementValue || elementValue > totalElementsCount))
        {
            return $"The elements of the permutation must be greater or equal than {PermutationConfig.MinPlacementElementValue} and less than or equal to the number of elements in the set.";
        }

        return null;
    }


    public static string? ValidateInputFile(string[] input)
    {
        if (input.Length < PermutationConfig.ValuesLinesQuantity)
        {
            return "The input file must contain two lines.";
        }

        foreach (string line in input)
        {
            string[] lineElements = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (ValidateLinesDataTypes(lineElements))
            {
                return "The input values must be whole numbers.";
            }
        }

        return null;
    }

    public static bool ValidateLinesDataTypes(string[] lineElements)
    {
        return !lineElements.All(x => int.TryParse(x, out _));
    }
}