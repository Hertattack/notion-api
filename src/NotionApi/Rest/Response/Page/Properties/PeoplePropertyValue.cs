using System.Collections.Generic;
using Newtonsoft.Json;
using NotionApi.Rest.Response.Objects;
using Util;

namespace NotionApi.Rest.Response.Page.Properties;

public class PeoplePropertyValue : NotionPropertyValue
{
    [JsonProperty("people")] public Option<IList<UserObject>> People { get; set; }
}