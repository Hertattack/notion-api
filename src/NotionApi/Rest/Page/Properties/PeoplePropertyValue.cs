using System.Collections.Generic;
using Newtonsoft.Json;
using NotionApi.Rest.Objects;
using Util;

namespace NotionApi.Rest.Properties
{
    public class PeoplePropertyValue : NotionPropertyValue
    {
        [JsonProperty("people")] public Option<IList<UserObject>> People { get; set; }
    }
}