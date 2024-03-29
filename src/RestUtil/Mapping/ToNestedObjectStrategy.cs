﻿using System;
using RestUtil.Request;
using Util;

namespace RestUtil.Mapping;

public class ToNestedObjectStrategy : BaseMappingStrategy
{
    public ToNestedObjectStrategy(IMapper mapper) : base(mapper)
    {
    }

    public override Option<object> GetValue(Type type, object value)
    {
        if (value == null)
            return null;

        var optionValue = mapper.ToOption(type, value);
        if (optionValue.HasValue && !optionValue.Value.HasValue)
            return Option.None;

        return mapper.Map(optionValue.HasValue ? optionValue.Value : value);
    }
}