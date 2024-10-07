namespace Lab1.Services;

public static class PermutationService
{
    private const int ZeroBasedOffset = 1;

    public static int GetFactorial(int value)
    {
        return value == 0 || value == 1 ? 1 : value * GetFactorial(value - 1);
    }

    public static int GetLexicographicPosition(int totalElementsCount, int placementLength, int[] placementElements)
    {
        bool[] used = new bool[totalElementsCount + ZeroBasedOffset];
        int permutationPosition = 0;

        for (int i = 0; i < placementLength; i++)
        {
            int smallerElementsCount = Enumerable.Range(1, placementElements[i] - ZeroBasedOffset)
                                     .Count(j => !used[j]);

            int remainingElementsCount = GetFactorial(totalElementsCount - i - 1) / GetFactorial(totalElementsCount - placementLength);

            permutationPosition += smallerElementsCount * remainingElementsCount;
            used[placementElements[i]] = true;
        }

        return permutationPosition + ZeroBasedOffset;
    }
}