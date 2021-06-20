using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NotionApi.Rest.Common.Properties;

namespace NotionApi.Util
{
    public class NotionPropertyConverter : JsonConverter<NotionProperty>
    {
        private readonly ILogger<NotionPropertyConverter> _logger;

        private JsonSerializer Serializer { get; }

        public NotionPropertyConverter(ILogger<NotionPropertyConverter> logger, OptionConverter optionConverter)
        {
            _logger = logger;

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter> {optionConverter}
            };

            Serializer = JsonSerializer.Create(settings);
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, NotionProperty value, JsonSerializer serializer)
        {
        }

        public override NotionProperty ReadJson(
            JsonReader jsonReader,
            Type objectType,
            NotionProperty existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (jsonReader.TokenType == JsonToken.Null || jsonReader.TokenType == JsonToken.EndObject)
                return null;

            if (hasExistingValue)
                return null;

            var propertyObject = JObject.Load(jsonReader);

            var targetType = (string) propertyObject["type"] switch
            {
                "created_time" => typeof(CreateTimeProperty),
                "last_edited_time" => typeof(LastEditedProperty),
                "relation" => propertyObject["relation"]?.Type == JTokenType.Array ? typeof(OneToManyRelationProperty) : typeof(ManyToOneRelationshipProperty),
                "rich_text" => typeof(RichTextProperty),
                "title" => typeof(TitleProperty),
                _ => typeof(NotionProperty)
            };

            jsonReader.Skip();

            using var propertyReader = propertyObject.CreateReader();
            try
            {
                return (NotionProperty) Serializer.Deserialize(propertyReader, targetType);
            }
            catch (Exception ex)
            {
                var stringValue = propertyObject.ToString();
                _logger.LogError(ex, $"Error deserializing json data.{Environment.NewLine}{stringValue}", stringValue);

                return JsonConvert.DeserializeObject<NotionProperty>(stringValue);
            }
        }
    }
}