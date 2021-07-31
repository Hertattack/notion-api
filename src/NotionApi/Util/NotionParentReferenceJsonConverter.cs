using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NotionApi.Rest.Common;
using RestUtil.Conversion;

namespace NotionApi.Util
{
    public class NotionParentReferenceJsonConverter : CustomTypeDeserializer<ParentReference>
    {
        public NotionParentReferenceJsonConverter(ILogger<CustomTypeDeserializer<ParentReference>> logger) : base(logger)
        {
        }

        protected override ParentReference CreateInstance(JObject jObject)
        {
            var targetTypeProperty = jObject.Properties().FirstOrDefault(p => p.Name == "type");
            if (targetTypeProperty == null)
            {
                _logger.LogError("Type property not found in json data. Cannot determine type.");
                return null;
            }

            var typeName = targetTypeProperty.Value.ToString();
            switch (typeName)
            {
                case "database_id":
                    return new ParentDatabaseReference();
                case "page_id":
                    return new ParentPageReference();
                case "workspace":
                    return new ParentWorkspaceReference();
                default:
                    _logger.LogWarning($"Specific type: '{typeName}' not registered cannot deserialize reference target.");
                    return new ParentReference();
            }
        }
    }
}