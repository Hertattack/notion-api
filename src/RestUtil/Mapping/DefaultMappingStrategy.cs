using System;
using System.Collections;
using System.Collections.Generic;
using RestUtil.Request;
using Util;

namespace RestUtil.Mapping;

public class DefaultMappingStrategy : BaseMappingStrategy
{
    public DefaultMappingStrategy(IMapper mapper) : base(mapper)
    {
    }

    public override Option<object?> GetValue(Type type, object? value)
    {
        if (value is null)
            return Option.None;

        if (type == typeof(string))
            return value;

        if (type == typeof(DateTime))
            return ((DateTime) value).ToString("O");

        if (typeof(IEnumerable).IsAssignableFrom(type))
            return MapEnumerable(value);

        if (type.IsEnum)
            return mapper.MapEnumeration(type, (Enum) value);

        return type.IsClass ? mapper.Map(value) : value;
    }


    private object MapEnumerable(object valueToMap)
    {
        var values = new List<object?>();
        foreach (var item in (IEnumerable) valueToMap)
        {
            if (item is null)
            {
                values.Add("");
                continue;
            }

            values.Add(item.GetType().IsPrimitive ? item.ToString() : mapper.Map(item));
        }

        return values;
    }
}