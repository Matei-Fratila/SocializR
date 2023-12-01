using System;

namespace Utils;

public class DateGenerator
{
    private readonly Random generator = new();
    public DateTime MyDate { get; set; }
    public int Range { get; set; }

    public DateGenerator(DateTime myDate)
    {
        MyDate = myDate;
        Range = (DateTime.Today - myDate).Days;
    }

    public DateGenerator()
    {

    }

    public DateTime GetRandomDay()
    {
        var newDate = MyDate.AddDays(generator.Next(Range));
        return newDate;
    }

    public DateTime GetRandomFriendshipDay(DateTime date1, DateTime date2)
    {
        var max = date1 > date2 ? date1 : date2;
        var range = (DateTime.Today - max).Days;
        if (range == 0)
        {
            return DateTime.Now;
        }
        return max.AddDays(generator.Next(range));
    }
}
