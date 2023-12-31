﻿namespace SocializR.Web.Code.Converters;

public class GenderConverter : ITypeConverter<string, bool?>
{
    public bool? Convert(string source, bool? destination, ResolutionContext context)
    {
        switch (source)
        {
            case "Male":
                return true;
            case "Female":
                return false;
            case "Unspecified":
                return null;
            default: return null;
        }
    }
}
