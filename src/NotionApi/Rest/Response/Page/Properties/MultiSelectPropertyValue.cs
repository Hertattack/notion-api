using System.Collections.Generic;
using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Response.Page.Properties
{
    public class MultiSelectPropertyValue : NotionPropertyValue
    {
        [JsonProperty("multi_select")] public Option<IList<SelectOptionValue>> SelectedOptions { get; set; } = new List<SelectOptionValue>();
    }
}