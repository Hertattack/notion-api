using RestUtil.Mapping;
using RestUtil.Request.Attributes;

namespace RestUtil.Test.Fixtures;

public enum MappingTestEnumeration
{
    SomeValue,

    [Mapping("should-use-this")] OtherValue,

    [Mapping(Strategy = typeof(ToLowerCaseStrategy))]
    // ReSharper disable once InconsistentNaming is a test.
    IShouldBecomeLowerCase
}