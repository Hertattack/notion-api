using System;

namespace RestUtil.Request
{
    public class RequestParameter
    {
        public RequestParameter(string parameterName, ParameterType type, object value)
        {
            Name = parameterName;
            Type = type;
            Value = value;
        }

        public string Name { get; }
        public ParameterType Type { get; }
        public Type Strategy { get; set; }
        public object Value { get; }
    }
}