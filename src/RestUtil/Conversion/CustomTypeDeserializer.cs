using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestUtil.Conversion
{
    public abstract class CustomTypeDeserializer<T> : JsonConverter
    {
        private static Type BaseType = typeof(T);

        protected readonly ILogger<CustomTypeDeserializer<T>> _logger;

        protected CustomTypeDeserializer(ILogger<CustomTypeDeserializer<T>> logger)
        {
            _logger = logger;
        }

        protected abstract T CreateInstance(JObject jObject);

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);

            var instance = CreateInstance(jObject);
            if (instance == null)
            {
                var exception = new Exception($"Cannot deserialize '{BaseType.FullName}' the specific type resulted in a null reference.");
                _logger.LogError(exception, "Error deserializing json data.", jObject.ToString());
                throw exception;
            }

            serializer.Populate(jObject.CreateReader(), instance);

            return instance;
        }


        public override bool CanConvert(Type objectType) =>
            objectType == BaseType;
    }
}