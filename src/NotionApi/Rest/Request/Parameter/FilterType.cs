using RestUtil.Request.Attributes;

namespace NotionApi.Rest.Request.Parameter;

public enum FilterType
{
    None,

    [Mapping("object")] Object
}