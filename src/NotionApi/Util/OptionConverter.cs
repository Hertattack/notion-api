using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Util;

namespace NotionApi.Util
{
    /// <summary>
    /// Notion uses "{}" as a value in the Json data for "null" or empty in cases like the create time and list-values. In Json this is an object and breaks
    /// the deserialization. This converter resolves that issue.
    ///
    /// If an Option<...> is used, the converter will translate the {} to null / the default.
    /// </summary>
    public class OptionConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }

        public override object ReadJson(JsonReader jsonReader, Type objectType, object existingValue, JsonSerializer jsonSerializer)
        {
            var actualType = objectType.GenericTypeArguments[0];
            var creator = objectType.GetMethod("From", BindingFlags.Static | BindingFlags.Public);

            if (creator is null)
                throw new Exception($"Error retrieving From method for type: {objectType.FullName}");

            object value = null;
            if (jsonReader.TokenType == JsonToken.Null
                || IsNonJsonObjectType(actualType) && jsonReader.TokenType == JsonToken.StartObject)
            {
                if (actualType.IsValueType)
                    value = Activator.CreateInstance(actualType);
            }
            else
                value = jsonSerializer.Deserialize(jsonReader, actualType);

            return creator.Invoke(null, new[] {value});
        }

        private static bool IsNonJsonObjectType(Type type)
        {
            return type.IsPrimitive
                   || type.IsArray
                   || type.IsEnum
                   || type.IsAssignableFrom(typeof(string))
                   || type.IsGenericType && type.GetGenericTypeDefinition().IsAssignableFrom(typeof(IList<>));
        }

        public override bool CanConvert(Type objectType) =>
            objectType.IsGenericType && objectType.GetGenericTypeDefinition().IsAssignableFrom(typeof(Option<>));
    }
}