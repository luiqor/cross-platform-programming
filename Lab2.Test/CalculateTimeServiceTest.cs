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
}
