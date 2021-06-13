using System;
using NotionApi.Util;

namespace NotionApi.Request.Mapping
{
    public class ToLowerCaseStrategy : BaseMappingStrategy
    {
        public ToLowerCaseStrategy(IMapper mapper) : base(mapper)
        {
        }

        public override Option<object> GetValue(Type genericTypeArgument, object value)
        {
            if (value == null)
                return "";

            return value.ToString()?.ToLower() ?? "";
        }
    }
}