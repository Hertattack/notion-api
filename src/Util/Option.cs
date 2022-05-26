using System;

namespace Util;

public struct Option<T> : IOption
{
    public bool HasValue { get; }

    private T value;

    public T Value =>
        HasValue ? value : throw new NullReferenceException("Option has no value.");

    public object GetValue()
    {
        return Value;
    }

    private Option(T value, bool hasValue)
    {
        HasValue = hasValue;
        this.value = value;
    }

    public static implicit operator Option<T>(NoneOption none)
    {
        return new(default, false);
    }

    public static implicit operator Option<T>(T value)
    {
        return new(value, !(value is null));
    }

    public static Option<T> From(T optionValue)
    {
        return new(optionValue, !(optionValue is null));
    }
}

public struct Option
{
    public static NoneOption None => new();
}

public struct NoneOption
{
}