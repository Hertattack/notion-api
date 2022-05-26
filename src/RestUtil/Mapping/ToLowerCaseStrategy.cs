using System;
using RestUtil.Request;
using Util;

namespace RestUtil.Mapping;

public class ToLowerCaseStrategy : BaseMappingStrategy
{
    public ToLowerCaseStrategy(IMapper mapper) : base(mapper)
    {
    }

    public override Option<object> GetValue(Type genericTypeArgument, object value)
    {
        if (value == null)
            return "";

        return value.ToString()?.ToLower() ?? "";
    }
}