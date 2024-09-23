using Lab1.Services;
using Lab1.ConstantsSets;
using Lab1.Validation;

namespace Lab1;
public class Program
{
    public static void Main()
    {
        string baseDirectory = Environment.CurrentDirectory;
        string inputFilePath = Path.Combine(baseDirectory, "assets", "input.txt");
        string outputFilePath = Path.Combine(baseDirectory, "assets", "output.txt");

        int processedData = ProcessData(inputFilePath);
        File.WriteAllText(outputFilePath, processedData.ToString());
    }

    public static int ProcessData(string inputFilePath)
    {
        DataValidator.Validate(() => DataValidator.ValidateFiles(inputFilePath, string.Empty));

        string[] input = File.ReadAllLines(inputFilePath);

        DataValidator.Validate(() => DataValidator.ValidateInputFile(input));

        string[] firstLineElements = input[TextfileLineIndex.KAndN].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string[] secondLineElements = input[TextfileLineIndex.PlacementElements].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        int totalElementsCount = int.Parse(firstLineElements[LineElementIndex.ValueN]);
        int placementLength = int.Parse(firstLineElements[LineElementIndex.ValueK]);
        int[] placementElements = Array.ConvertAll(secondLineElements, int.Parse);

        DataValidator.Validate(() => DataValidator.ValidatePermutationData(totalElementsCount, placementLength, placementElements));

        return PermutationService.GetLexicographicPosition(totalElementsCount, placementLength, placementElements);
    }
}