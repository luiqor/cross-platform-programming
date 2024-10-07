using DataValidator = Lab2.Validation.DataValidator;

namespace Lab2.Test;

public class DataValidatorTests : IDisposable
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void Validate_NumberOfTimesLineNotWholeNumber_ReturnsErrorMessage()
    {
        string line = "abc";

        string? result = DataValidator.ValidateNumberOfTimesLine(line);

        Assert.Equal("Number of times must be a whole number.", result);
    }

    [Fact]
    public void Validate_NumberOfTimesGreaterThanMaxTimes_ReturnsErrorMessage()
    {
        int times = 60000;

        string? result = DataValidator.ValidateNumberOfTimes(times);

        Assert.Equal("Number of times must be less than or equal to 50000.", result);
    }

    [Fact]
    public void Validate_InvalidValidateTimes_ReturnsErrorMessage()
    {
        string[] times =
        [
            "k:kk:kk",
            "08:05:11",
            "12:50:07"
        ];
        int numberOfTimes = 3;

        string? result = DataValidator.ValidateTimes(times, numberOfTimes);

        Assert.Equal("Invalid time format: k:kk:kk. Expected format is HH:MM:SS.", result);
    }
}