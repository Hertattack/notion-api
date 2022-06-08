using Util;

namespace NotionGraphDatabase.Util;

public static class OptionExtensions
{
    public static TResult ValueOrElse<TResult>(this Option<TResult> option, TResult elseValue)
    {
        return option.HasValue ? option.Value : elseValue;
    }

    public static TResult? ValueOrDefault<TResult>(this Option<TResult> option)
    {
        return option.HasValue ? option.Value : default;
    }
}