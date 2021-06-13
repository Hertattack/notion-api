using System;
using NotionApi.Util;

namespace NotionApi.Request.Mapping
{
    public class ToNestedObjectStrategy : BaseMappingStrategy
    {
        public ToNestedObjectStrategy(IMapper mapper) : base(mapper)
        {
        }

        public override Option<object> GetValue(Type type, object value)
        {
            if (value == null)
                return null;

            var optionValue = _mapper.ToOption(type, value);
            if (optionValue.HasValue && !optionValue.Value.HasValue)
                return Option.None;

            return _mapper.Map(optionValue.HasValue ? optionValue.Value : value);
        }
    }
}