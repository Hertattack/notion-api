using NotionApi.Request.Attributes;
using NotionApi.Request.Mapping;
using NotionApi.Util;

namespace NotionApi.Test.Mapping.Strategies.Fixtures
{
    public class BasicNestedStructure
    {
        [Mapping(Strategy = typeof(ToNestedObjectStrategy))]
        public Option<BasicNestedStructure> Nested { get; set; }

        [Mapping] public string SomeValue { get; set; }
    }
}