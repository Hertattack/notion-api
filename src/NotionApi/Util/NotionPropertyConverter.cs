using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NotionApi.Rest.Common.Properties;

namespace NotionApi.Util
{
    public class NotionPropertyConverter : JsonConverter<NotionProperty>
    {
        private JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        private JsonSerializer _serializer;

        private JsonSerializer Serializer
        {
            get
            {
                if (_serializer == null)
                    _serializer = JsonSerializer.Create(settings);

                return _serializer;
            }
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, NotionProperty value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
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
                "relation" => propertyObject["relation"]?.GetType().IsArray == true ? typeof(OneToManyRelationProperty) : typeof(ManyToOneRelationshipProperty),
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
                Console.WriteLine(stringValue);
                Console.WriteLine(ex.Message);
                return JsonConvert.DeserializeObject<NotionProperty>(stringValue);
            }
        }
    }
}