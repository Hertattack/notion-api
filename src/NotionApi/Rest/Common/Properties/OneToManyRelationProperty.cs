using System.Collections.Generic;
using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Properties
{
    public class OneToManyRelationProperty : NotionProperty
    {
        [JsonProperty(PropertyName = "relation")]
        public IList<RelationReferenceProperty> Relations { get; set; } = new List<RelationReferenceProperty>();
    }
}