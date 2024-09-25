namespace Lab2.Services;

public static class CalculateTimeService
{
    private const int MaxSecondsInHalfDay = 12 * 3600;
    private static int ConvertTimeToSeconds(string time)
    {
        int[] timeParts = time.Split(':').Select(int.Parse).ToArray();
        int hours = timeParts[0] % 12;
        int minutes = timeParts[1];
        int seconds = timeParts[2];

        return hours * 3600 + minutes * 60 + seconds;
    }

    private static string ConvertSecondsToTime(int totalSeconds)
    {
        int hours = totalSeconds / 3600 % 12;
        if (hours == 0) hours = 12;
        int minutes = totalSeconds % 3600 / 60;
        int seconds = totalSeconds % 60;

        return $"{hours}:{minutes:D2}:{seconds:D2}";
    }

    public static string CalculateMinimumAdjustmentTime(string[] times)
    {
        int[] adjustmentTimes = new int[MaxSecondsInHalfDay];

        foreach (var time in times)
        {
            int timeInSeconds = ConvertTimeToSeconds(time);
            for (int currentSecond = 0; currentSecond < MaxSecondsInHalfDay; currentSecond++)
            {
                int adjustment = (currentSecond - timeInSeconds + MaxSecondsInHalfDay) % MaxSecondsInHalfDay;
                adjustmentTimes[currentSecond] += adjustment;
            }
        }

        int optimalTimeInSeconds = Array.IndexOf(adjustmentTimes, adjustmentTimes.Min());

        return ConvertSecondsToTime(optimalTimeInSeconds);
    }
}
