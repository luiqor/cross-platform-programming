using Lab1;

class Program
{
    static void Main()
    {
        string inputFilePath = Path.Combine("assets", "input.txt");
        string outputFilePath = Path.Combine("assets", "output.txt");

        string[] input = File.ReadAllLines(inputFilePath);
        string[] firstLine = input[0].Split();
        int totalElementsCount = int.Parse(firstLine[0]);
        int placementLength = int.Parse(firstLine[1]);
        int[] placementElements = Array.ConvertAll(input[1].Split(), int.Parse);

        int permutationPosition = PermutationService.GetLexicographicPosition(totalElementsCount, placementLength, placementElements);

        File.WriteAllText(outputFilePath, permutationPosition.ToString());
    }
}