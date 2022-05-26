using System;
using Util;

namespace RestUtil.Request;

public interface IMapper
{
    object Map(RequestParameter parameter);
    object Map(object obj);
    object MapEnumeration(Type type, Enum valueToMap);
    Option<IOption> ToOption(Type type, object value);
}