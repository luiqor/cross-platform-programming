using Lab2.ConstantsSets;
using Lab2.Services.ConstantsSets;

namespace Lab2.Services;

public static class CalculateTimeService
{
    private const int NoTime = 0;


    private static int ConvertTimeToSeconds(string time)
    {
        int[] timeParts = time.Split(":").Select(int.Parse).ToArray();
        int hours = timeParts[TimePartIndex.Hours] % TimeInTime.HoursInHalfDay;
        int minutes = timeParts[TimePartIndex.Minutes];
        int seconds = timeParts[TimePartIndex.Seconds];

        return hours * TimeInTime.SecondsInHour + minutes * TimeInTime.SecondsInMinute + seconds;
    }

    private static string ConvertSecondsToTime(int totalSeconds)
    {
        int hours = totalSeconds / TimeInTime.SecondsInHour % TimeInTime.HoursInHalfDay;

        if (hours == NoTime)
        {
            hours = TimeInTime.HoursInHalfDay;
        }

        int minutes = totalSeconds % TimeInTime.SecondsInHour / TimeInTime.MinutesInHour;
        int seconds = totalSeconds % TimeInTime.SecondsInMinute;

        return $"{hours}:{minutes:D2}:{seconds:D2}";
    }

    public static string CalculateMinimumAdjustmentTime(string[] times)
    {
        int[] adjustmentTimes = new int[TimeInTime.MaxSecondsInHalfDay];

        foreach (var time in times)
        {
            int timeInSeconds = ConvertTimeToSeconds(time);

            for (int currentSecond = NoTime; currentSecond < TimeInTime.MaxSecondsInHalfDay; currentSecond++)
            {
                int adjustment =
                    (currentSecond - timeInSeconds + TimeInTime.MaxSecondsInHalfDay)
                    % TimeInTime.MaxSecondsInHalfDay;
                adjustmentTimes[currentSecond] += adjustment;
            }
        }

        int optimalTimeInSeconds = Array.IndexOf(adjustmentTimes, adjustmentTimes.Min());

        return ConvertSecondsToTime(optimalTimeInSeconds);
    }
}
