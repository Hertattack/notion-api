using System;
using RestUtil.Request;
using Util;

namespace RestUtil.Mapping;

public abstract class BaseMappingStrategy : IMappingStrategy
{
    protected readonly IMapper mapper;

    protected BaseMappingStrategy(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public abstract Option<object?> GetValue(Type type, object? value);
}