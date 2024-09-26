using Lab2.Services;

namespace Lab2.Test;
public class CalculateTimeServiceTest : IDisposable
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void ConvertTimeToSeconds_ValidTime_ReturnsCorrectSeconds()
    {
        string time = "12:34:56";

        int result = CalculateTimeService.ConvertTimeToSeconds(time);

        Assert.Equal(45296, result);
    }

    [Fact]
    public void ConvertSecondsToTime_ValidSeconds_ReturnsCorrectTime()
    {
        int totalSeconds = 45296;

        string result = CalculateTimeService.ConvertSecondsToTime(totalSeconds);

        Assert.Equal("12:34:56", result);
    }
}
