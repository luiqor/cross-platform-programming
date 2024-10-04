using DataValidator = Lab3.Validation.DataValidator;

namespace Lab3.Test;

public class DataValidatorTests : IDisposable
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void ValidateLevelRowColumsCount_IncorrectRowLength_ReturnsErrorMessage()
    {
        int lineElementCount = 4;
        int n = 5;
        int i = 0;
        int j = 0;

        string? result = DataValidator.ValidateLevelRowColumsCount(lineElementCount, n, i, j);

        Assert.Equal("Incorrect row length at level 1, row 1", result);
    }

    [Fact]
    public void ValidateLevelRowsCount_NotEnoughRows_ReturnsErrorMessage()
    {
        int nextLineIndex = 5;
        int totalLines = 4;
        int i = 0;

        string? result = DataValidator.ValidateLevelRowsCount(nextLineIndex, totalLines, i);

        Assert.Equal("Not enough rows for level 1", result);
    }
}