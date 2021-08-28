using System;

namespace RestUtil.Request.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ParameterAttribute : MappingAttribute
    {
        public ParameterType Type = ParameterType.Query;

        public ParameterAttribute()
        {
        }

        public ParameterAttribute(string name) : base(name)
        {
        }
    }
}