using System;
using System.Collections;
using System.Collections.Generic;

namespace NotionApi.Request.Mapping
{
    public class DefaultMappingStrategy : BaseMappingStrategy
    {
        public DefaultMappingStrategy(IMapper mapper) : base(mapper)
        {
        }

        public override object GetValue(object valueToMap)
        {
            if (valueToMap is null)
                return null;

            var type = valueToMap.GetType();

            if (typeof(IEnumerable).IsAssignableFrom(type))
                return MapEnumerable(valueToMap);

            if (type.IsEnum)
                return _mapper.MapEnumeration(type, (Enum) valueToMap);

            if (type.IsClass)
                return _mapper.Map(valueToMap);

            return valueToMap.ToString();
        }


        private object MapEnumerable(object valueToMap)
        {
            var values = new List<object>();
            foreach (var item in (IEnumerable) valueToMap)
            {
                if (item == null)
                {
                    values.Add("");
                    continue;
                }

                if (item.GetType().IsPrimitive)
                    values.Add(item.ToString());
                else
                    values.Add(_mapper.Map(item));
            }

            return values;
        }
    }
}