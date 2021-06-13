using NotionApi.Request.Attributes;
using NotionApi.Request.Mapping;

namespace NotionApi.Test.Mapping.Strategies.Fixtures
{
    public enum MappingTestEnumeration
    {
        SomeValue,

        [Mapping("should-use-this")] 
        OtherValue,

        [Mapping(Strategy = typeof(ToLowerCaseStrategy))]
        IShouldBecomeLowerCase
    }
}