namespace SocializR.Web.Code.Converters;

public static class DateConverter
{
    public static string Convert(DateTime time)
    {
        var timeAgo = "";//time.TimeAgo(); 

        return timeAgo;

        //    if (period.Seconds == 0)
        //    {
        //        return "just now";
        //    }

        //    if(period.Minutes < 1)
        //    {
        //        return period.Seconds.ToString() + " seconds ago";
        //    }

        //    if (period.Hours < 1)
        //    {
        //        return period.Minutes.ToString() + " minutes ago";
        //    }

        //    if (period.Days < 1)
        //    {
        //        return period.Hours.ToString() + " hours ago";
        //    }

        //    if (period.Days % 7 == 0)
        //    {
        //        return (period.Days / 7).ToString() + " weeks ago";
        //    }

        //    if (period.Days / 7 == 0)
        //    {
        //        return period.Days.ToString() + " days ago";
        //    }

        //    if (period.Days / 7 > 1)
        //    {
        //        return (period.Days/7).ToString() +  "weeks and " + period.Days.ToString() + " days ago";
        //    }

        //    if (period.Days % 365 < 1)
        //    {
        //        return (period.Days % 365).ToString() + " years ago";
        //    }

        //    return period.Days.ToString() + " days ago";
        //}
    }
}
