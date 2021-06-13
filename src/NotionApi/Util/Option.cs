using System;

namespace NotionApi.Util
{
    public struct Option<T> : IOption
    {
        public bool HasValue { get; }

        private T value;

        public T Value =>
            HasValue ? value : throw new NullReferenceException("Option has no value.");

        public object GetValue() =>
            Value;

        private Option(T value, bool hasValue)
        {
            HasValue = hasValue;
            this.value = value;
        }

        public static implicit operator Option<T>(NoneOption none) =>
            new Option<T>(default, false);

        public static implicit operator Option<T>(T value) =>
            new Option<T>(value, !(value is null));
    }

    public struct Option
    {
        public static NoneOption None => new NoneOption();
    }

    public struct NoneOption
    {
    }
}