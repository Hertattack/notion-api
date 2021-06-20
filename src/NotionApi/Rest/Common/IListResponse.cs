using System.Collections.Generic;
using Newtonsoft.Json;

namespace NotionApi.Rest.Common
{
    public interface IListResponse<TListType>
    {
        [JsonProperty(PropertyName = "object")]
        string ObjectType { get; set; }

        [JsonProperty(PropertyName = "results")]
        IList<TListType> Results { get; set; }
    }
}