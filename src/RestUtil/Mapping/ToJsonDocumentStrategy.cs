using System;
using Newtonsoft.Json;
using RestUtil.Request;
using Util;

namespace RestUtil.Mapping;

public class ToJsonDocumentStrategy : BaseMappingStrategy
{
    public ToJsonDocumentStrategy(IMapper mapper) : base(mapper)
    {
    }

    public override Option<object> GetValue(Type genericTypeArgument, object value)
    {
        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Include
        };

        return JsonConvert.SerializeObject(value, settings);
    }
}