using System.Collections.Generic;
using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Properties
{
    public class SelectProperty : NotionProperty
    {
        [JsonProperty(PropertyName = "options")]
        public IList<SelectOptionValue> PossibleValues { get; set; } = new List<SelectOptionValue>();
    }
}