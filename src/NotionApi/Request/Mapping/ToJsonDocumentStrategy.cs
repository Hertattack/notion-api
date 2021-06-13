using System;
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
            throw new System.NotImplementedException();
        }
    }
}