using System.Collections.Generic;

namespace NotionApi.Request.Mapping
{
    public class ToNestedObjectStrategy : BaseMappingStrategy
    {
        public ToNestedObjectStrategy(IMapper mapper) : base(mapper)
        {
        }

        public override object GetValue(object valueToMap)
        {
            var result = new Dictionary<string, object>();
            
            
            
            return result;
        }
    }
}