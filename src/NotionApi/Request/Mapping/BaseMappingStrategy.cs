using System;
using NotionApi.Util;

namespace NotionApi.Request.Mapping
{
    public abstract class BaseMappingStrategy : IMappingStrategy
    {
        protected readonly IMapper _mapper;

        protected BaseMappingStrategy(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        public abstract Option<object> GetValue(Type type, object value);
    }
}