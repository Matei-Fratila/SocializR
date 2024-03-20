namespace Utils;
public static class DateTimeExtensions
{
    public static string TimeAgo(this DateTime dateTime)
    {
        var timeSpan = DateTime.Now - dateTime;

        return timeSpan.TotalSeconds switch
        { 
            <= 60 => $"acum {timeSpan.Seconds} secunde",
            _ => timeSpan.TotalMinutes switch
            {
                <= 1 => "acum 1 minut",
                < 60 => $"acum {timeSpan.Minutes} minute",
                _ => timeSpan.TotalHours switch
                {
                    <= 1 => "acum 1 ora",
                    < 24 => $"acum {timeSpan.Hours} ore",
                    _ => timeSpan.TotalDays switch
                    {
                        <= 1 => "ieri",
                        <= 30 => $"acum {timeSpan.Days} zile",

                        <= 60 => "acum 1 luna",
                        < 365 => $"acum {timeSpan.Days / 30} luni",

                        <= 365 * 2 => "acum 1 an",
                        _ => $"acum {timeSpan.Days / 365} ani"
                    }
                }
            }
        };
    }
}
