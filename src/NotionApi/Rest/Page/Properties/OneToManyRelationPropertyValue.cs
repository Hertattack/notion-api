using System.Collections.Generic;
using Newtonsoft.Json;

namespace NotionApi.Rest.Properties
{
    public class OneToManyRelationPropertyValue : NotionPropertyValue
    {
        [JsonProperty(PropertyName = "relation")]
        public IList<RelationReferenceProperty> Relations { get; set; } = new List<RelationReferenceProperty>();
    }
}