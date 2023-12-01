using System;

namespace Utils;
public static class StringExtensions
{
    public static string RemoveControllerSuffix(this string original)
    => original.Replace("Controller", string.Empty, StringComparison.OrdinalIgnoreCase);

    public static string RemoveAsyncSuffix(this string original)
        => original.Replace("Async", string.Empty, StringComparison.OrdinalIgnoreCase);

}
