﻿using System;
using NotionApi.Util;

namespace NotionApi.Request
{
    public interface IMapper
    {
        object Map(object search);
        object MapEnumeration(Type type, Enum valueToMap);
        Option<IOption> ToOption(Type type, object value);
    }
}