using DataValidator = Lab1.Validation.DataValidator;

namespace Lab1.Test;

public class DataValidatorTests : IDisposable
{
    private readonly StringWriter _consoleOutput;

    public DataValidatorTests()
    {
        _consoleOutput = new StringWriter();
        Console.SetOut(_consoleOutput);
    }

    [Fact]
    public void ValidatePermutationData_TotalLessThanPlacement_ReturnsCorrectMessage()
    {
        int totalElementsCount = 5;
        int placementLength = 6;
        int[] placementElements = [1, 2, 3, 4, 5];

        string? result = DataValidator.ValidatePermutationData(totalElementsCount, placementLength, placementElements);


        Assert.Equal("The number of elements in the set must be greater than or equal to the length of the permutation.", result);
    }

    [Fact]
    public void ValidatePermutationData_PlacementLengthInvalid_ReturnsCorrectMessage()
    {
        int totalElementsCount = 10;
        int placementLength = 0;
        int[] placementElements = [1, 2, 3, 4, 5];

        string? result = DataValidator.ValidatePermutationData(totalElementsCount, placementLength, placementElements);
        Assert.Equal("The length of the permutation must be less than or equal to 1 and less than or equal to 12.", result);
    }

    [Fact]
    public void ValidatePermutationData_PlacementElementsLengthMismatch_ReturnsCorrectMessage()
    {
        int totalElementsCount = 10;
        int placementLength = 5;
        int[] placementElements = [1, 2, 3, 4];

        string? result = DataValidator.ValidatePermutationData(totalElementsCount, placementLength, placementElements);
        Assert.Equal("The number of elements in the permutation must be equal to the length of the permutation.", result);
    }

    [Fact]
    public void ValidatePermutationData_PlacementElementsOutOfRange_ReturnsCorrectMessage()
    {
        int totalElementsCount = 10;
        int placementLength = 5;
        int[] placementElements = [1, 2, 3, 4, 11];

        string? result = DataValidator.ValidatePermutationData(totalElementsCount, placementLength, placementElements);
        Assert.Equal("The elements of the permutation must be greater or equal than 1 and less than or equal to the number of elements in the set.", result);
    }

    [Fact]
    public void ValidatePermutationData_ValidInput_ReturnsNull()
    {
        int totalElementsCount = 10;
        int placementLength = 5;
        int[] placementElements = [1, 2, 3, 4, 5];

        string? result = DataValidator.ValidatePermutationData(totalElementsCount, placementLength, placementElements);
        Assert.Null(result);
    }

    [Fact]
    public void ValidateInputFile_InputFileTooShort_ReturnsCorrectMessage()
    {
        string[] input = ["1 2 3"];

        string? result = DataValidator.ValidateInputFile(input);
        Assert.Equal("The input file must contain two lines.", result);
    }

    [Fact]
    public void ValidateInputFile_InputFileLineDataTypeInvalid_ReturnsCorrectMessage()
    {
        string[] input = ["1 2 3", "4 5 x"];

        string? result = DataValidator.ValidateInputFile(input);
        Assert.Equal("The input values must be whole numbers.", result);
    }

    [Fact]
    public void ValidateInputFile_InputFileValid_ReturnsNull()
    {
        string[] input = ["1 2 3", "4 5 6"];

        string? result = DataValidator.ValidateInputFile(input);
        Assert.Null(result);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}