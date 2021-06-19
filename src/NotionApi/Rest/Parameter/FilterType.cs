using RestUtil.Request.Attributes;

namespace NotionApi.Rest.Parameter
{
    public enum FilterType
    {
        None,

        [Mapping("object")] Object
    }
}