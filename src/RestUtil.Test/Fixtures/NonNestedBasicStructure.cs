using RestUtil.Request.Attributes;

namespace RestUtil.Test.Fixtures
{
    public class NonNestedBasicStructure
    {
        [Mapping("a")] public bool First { get; set; }

        [Mapping("b")] public int Second { get; set; }

        [Mapping] public string Third { get; set; }

        public string Fourth { get; set; }
    }
}