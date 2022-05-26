using RestUtil.Request.Attributes;

namespace NotionApi.Rest.Request.Parameter;

public enum FilterProperty
{
    [Mapping("page")] Page,

    [Mapping("page")] Database
}