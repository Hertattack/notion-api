using System;

namespace NotionApi.Request
{
    public interface IMapper
    {
        object Map(object search);
        object MapEnumeration(Type type, Enum valueToMap);
    }
}