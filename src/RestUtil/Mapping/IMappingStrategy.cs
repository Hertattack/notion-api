using System;
using Util;

namespace RestUtil.Mapping;

public interface IMappingStrategy
{
    Option<object?> GetValue(Type type, object? value);
}