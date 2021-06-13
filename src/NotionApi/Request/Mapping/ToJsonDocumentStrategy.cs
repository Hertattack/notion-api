using System;
using Newtonsoft.Json;
using NotionApi.Util;

namespace NotionApi.Request.Mapping
{
    public class ToJsonDocumentStrategy : BaseMappingStrategy
    {
        public ToJsonDocumentStrategy(IMapper mapper) : base(mapper)
        {
        }

        public override Option<object> GetValue(Type genericTypeArgument, object value)
        {
            var settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            settings.NullValueHandling = NullValueHandling.Include;

            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }
    }
}