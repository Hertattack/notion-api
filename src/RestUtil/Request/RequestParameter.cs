using System;

namespace RestUtil.Request
{
    public class RequestParameter
    {
        public RequestParameter(ParameterType type, object value)
        {
            Type = type;
            Value = value;
        }

        public ParameterType Type { get; }
        public Type Strategy { get; set; }
        public object Value { get; }
    }
}