using System;
using System.Collections.Generic;
using NotionApi.Util;

namespace NotionApi.Request.Mapping
{
    public class ToNestedObjectStrategy : BaseMappingStrategy
    {
        public ToNestedObjectStrategy(IMapper mapper) : base(mapper)
        {
        }

        public override Option<object> GetValue(Type genericTypeArgument, object value)
        {
            var result = new Dictionary<string, object>();
            
            
            
            return result;
        }
    }
}