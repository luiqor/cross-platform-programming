using Lab1;
using Lab1.ConstantsSets;

class Program
{
    static void Main()
    {
        string inputFilePath = Path.Combine("assets", "input.txt");
        string outputFilePath = Path.Combine("assets", "output.txt");

        string[] input = File.ReadAllLines(inputFilePath);

        string[] firstLineElements = input[TextfileLineIndex.KAndN].Split();
        int totalElementsCount = int.Parse(firstLineElements[LineElementIndex.ValueN]);
        int placementLength = int.Parse(firstLineElements[LineElementIndex.ValueK]);

        string[] secondLineElements = input[TextfileLineIndex.PlacementElements].Split();
        int[] placementElements = Array.ConvertAll(secondLineElements, int.Parse);

        int permutationPosition = PermutationService.GetLexicographicPosition(totalElementsCount, placementLength, placementElements);

        File.WriteAllText(outputFilePath, permutationPosition.ToString());
    }
}