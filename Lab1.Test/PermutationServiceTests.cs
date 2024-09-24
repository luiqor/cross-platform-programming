using PermutationService = Lab1.Services.PermutationService;

namespace Lab1.Test;

public class PermutationServiceTests : IDisposable
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void GetLexicographicPosition_ValidData_ReturnsCorrectResult()
    {
        int totalElementsCount = 3;
        int placementLength = 2;
        int[] placementElements = [3, 2];

        int result = PermutationService.GetLexicographicPosition(totalElementsCount, placementLength, placementElements);

        Assert.Equal(6, result);
    }


    [Fact]

    public void Factorial_ValidData_ReturnsCorrectResult()
    {
        int number = 3;

        int result = PermutationService.GetFactorial(number);

        Assert.Equal(6, result);
    }

}
