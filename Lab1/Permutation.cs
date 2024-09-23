namespace Lab1;

public class PermutationService
{
    static private int GetFactorial(int value)
    {
        return value == 0 || value == 1 ? 1 : value * GetFactorial(value - 1);
    }

    static public int GetLexicographicPosition(int totalElementsCount, int placementLength, int[] placementElements)
    {
        bool[] used = new bool[totalElementsCount + 1];
        int permutationPosition = 0;

        for (int i = 0; i < placementLength; i++)
        {
            int smallerElementsCount = Enumerable.Range(1, placementElements[i] - 1)
                                     .Count(j => !used[j]);

            int remainingElementsCount = GetFactorial(totalElementsCount - i - 1) / GetFactorial(totalElementsCount - placementLength);

            permutationPosition += smallerElementsCount * remainingElementsCount;
            used[placementElements[i]] = true;
        }

        return permutationPosition + 1;
    }
}