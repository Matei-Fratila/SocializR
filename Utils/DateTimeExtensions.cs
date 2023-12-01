namespace Utils;
public static class DateTimeExtensions
{
    public static string TimeAgo(this DateTime dateTime)
    {
        var timeDifference = DateTime.Now - dateTime;

        if (timeDifference.Minutes < 1) return timeDifference.Seconds == 1 ? "1 second ago" : $"{timeDifference.Seconds} seconds ago";
        if (timeDifference.Hours < 1) return timeDifference.Minutes == 1 ? "1 minute ago" : $"{timeDifference.Minutes} minutes ago";

        switch (timeDifference.Days)
        {
            case < 1: return timeDifference.Hours == 1 ? "1 hour ago" : $"{timeDifference.Hours} hours ago";
            case < 7: return timeDifference.Days == 1 ? "1 day ago" : $"{timeDifference.Days} days ago";
            case < 30: return timeDifference.Days / 7 == 1 ? "1 week ago" : $"{timeDifference.Days / 7} weeks ago";
            case < 365: return timeDifference.Days / 30 == 1 ? "1 month ago" : $"{timeDifference.Days / 30} months ago";
            default: return timeDifference.Days / 365 == 1 ? "1 year ago" : $"{timeDifference.Days / 365} years ago";
        }
    }
}
