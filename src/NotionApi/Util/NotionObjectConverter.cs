using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NotionApi.Rest.Objects;
using RestUtil.Conversion;

namespace NotionApi.Util
{
    public class NotionObjectConverter : CustomTypeDeserializer<NotionObject>
    {
        public NotionObjectConverter(ILogger<CustomTypeDeserializer<NotionObject>> logger) : base(logger)
        {
        }

        protected override NotionObject CreateInstance(JObject jObject)
        {
            var targetTypeProperty = jObject.Properties().FirstOrDefault(p => p.Name == "object");
            if (targetTypeProperty == null)
            {
                _logger.LogError("Object property not found in json data. Cannot determine type.");
                return null;
            }

            var typeName = targetTypeProperty.Value.ToString();
            switch (typeName)
            {
                case "page":
                    return new PageObject();
                default:
                    _logger.LogWarning($"Specific type: '{typeName}' not registered, falling back to {nameof(NotionObject)}.");
                    return new NotionObject();
            }
        }
    }
}