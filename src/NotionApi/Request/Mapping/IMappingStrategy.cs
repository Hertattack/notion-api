using System;
using NotionApi.Util;

namespace NotionApi.Request.Mapping
{
    public interface IMappingStrategy
    {
        Option<object> GetValue(Type type, object value);
    }
}