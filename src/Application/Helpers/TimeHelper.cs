using System.Globalization;
using System.Text.RegularExpressions;

namespace Application.Helpers;

public static class TimeHelper
{
    public static TimeSpan ConvertToTimeSpan(string time)
    {
        if (IsValidTimeFormat(time))
        {
            var components = time.Split(':');
            int hours = int.Parse(components[0]);
            int minutes = int.Parse(components[1]);

            return new TimeSpan(hours, minutes, 0);
        }

        throw new ArgumentException("Invalid time format or value.", nameof(time));
    }

    private static bool IsValidTimeFormat(string time)
    {
        return Regex.IsMatch(time, @"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");
    }
}