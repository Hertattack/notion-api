using System;
using System.Collections;
using System.Collections.Generic;
using NotionApi.Util;

namespace NotionApi.Request.Mapping
{
    public class DefaultMappingStrategy : BaseMappingStrategy
    {
        public DefaultMappingStrategy(IMapper mapper) : base(mapper)
        {
        }

        public override Option<object> GetValue(Type genericTypeArgument, object value)
        {
            if (value is null)
                return Option.None;

            var type = value.GetType();

            if (type == typeof(string))
                return value;

            if (typeof(IEnumerable).IsAssignableFrom(type))
                return MapEnumerable(value);

            if (type.IsEnum)
                return _mapper.MapEnumeration(type, (Enum) value);

            if (type.IsClass)
                return _mapper.Map(value);

            return value;
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