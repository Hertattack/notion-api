using NotionApi.Request.Attributes;

namespace NotionApi.Rest.Parameter
{
    public enum FilterType
    {
        None,

        [Mapping("object")] Object
    }
}