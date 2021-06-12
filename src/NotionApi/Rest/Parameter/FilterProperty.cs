using NotionApi.Request.Attributes;

namespace NotionApi.Rest.Parameter
{
    public enum FilterProperty
    {
        [Mapping("page")]
        Page,
        
        [Mapping("page")]
        Database
    }
}