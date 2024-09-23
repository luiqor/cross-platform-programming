using Lab1.Validation.ConstantsSets;

namespace Lab1.Validation;

public static class DataValidator
{
    private const int SuccessfulExitCode = 0;


    public delegate bool ValidatorCallback();


    public static void Validate(ValidatorCallback callback)
    {
        bool isError = callback();

        if (isError)
        {
            Environment.Exit(SuccessfulExitCode);
        }
    }

    public static bool ValidateFiles(string inputFilePath, string outputFilePath)
    {
        if (!File.Exists(inputFilePath))
        {
            Console.Error.WriteLine($"Input file not found at {Path.GetFullPath(inputFilePath)}.");
            return true;
        }

        if (File.Exists(outputFilePath) && Path.GetFullPath(inputFilePath) == Path.GetFullPath(outputFilePath))
        {
            Console.Error.WriteLine("Input and output files must be different.");
            return true;
        }

        return false;
    }

    public static bool ValidatePermutationData(int totalElementsCount, int placementLength, int[] placementElements)
    {
        if (totalElementsCount < placementLength)
        {
            Console.Error.WriteLine("The number of elements in the set must be greater than or equal to the length of the permutation.");
            return true;
        }

        if (placementLength < PermutationConfig.MinValue || totalElementsCount > PermutationConfig.MaxValue)
        {
            Console.Error.WriteLine("The length of the permutation must be greater than 0 and less than or equal to 12.");
            return true;
        }

        if (placementElements.Length != placementLength)
        {
            Console.Error.WriteLine("The number of elements in the permutation must be equal to the length of the permutation.");
            return true;
        }

        if (placementElements.Any((elementValue) => elementValue < PermutationConfig.MinPlacementElementValue || elementValue > totalElementsCount))
        {
            Console.Error.WriteLine("The elements of the permutation must be greater than 0 and less than or equal to the number of elements in the set.");
            return true;
        }

        return false;
    }


    public static bool ValidateInputFile(string[] input)
    {
        if (input.Length < PermutationConfig.ValuesLinesQuantity)
        {
            Console.Error.WriteLine("The input file must contain two lines.");
            return true;
        }

        foreach (string line in input)
        {
            string[] lineElements = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (ValidateLinesDataTypes(lineElements))
            {
                Console.Error.WriteLine("The input values must be whole numbers.");
                return true;
            }
        }

        return false;
    }

    public static bool ValidateLinesDataTypes(string[] lineElements)
    {
        return !lineElements.All(x => int.TryParse(x, out _));
    }
}