using RestUtil.Mapping;
using RestUtil.Request.Attributes;
using Util;

namespace RestUtil.Test.Fixtures;

public class BasicNestedStructure
{
    [Mapping(Strategy = typeof(ToNestedObjectStrategy))]
    public Option<BasicNestedStructure> Nested { get; set; }

    [Mapping] public string SomeValue { get; set; }
}