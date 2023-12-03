namespace Utils;
public static class DateTimeExtensions
{
    public static string TimeAgo(this DateTime dateTime)
    {
        var timeSpan = DateTime.Now - dateTime;

        return timeSpan.TotalSeconds switch
        { 
            <= 60 => $"{timeSpan.Seconds} seconds ago",
            _ => timeSpan.TotalMinutes switch
            {
                <= 1 => "1 minute ago",
                < 60 => $"{timeSpan.Minutes} minutes ago",
                _ => timeSpan.TotalHours switch
                {
                    <= 1 => "1 hour ago",
                    < 24 => $"{timeSpan.Hours} hours ago",
                    _ => timeSpan.TotalDays switch
                    {
                        <= 1 => "yesterday",
                        <= 30 => $"{timeSpan.Days} days ago",

                        <= 60 => "1 month ago",
                        < 365 => $"{timeSpan.Days / 30} months ago",

                        <= 365 * 2 => "1 year ago",
                        _ => $"{timeSpan.Days / 365} years ago"
                    }
                }
            }
        };
    }
}
