using System;
using FluentRest.DynamicTyping;

namespace FluentRest.Extensions
{
    public static class BodySpecificationExtensions
    {
        public static T IsRequired<T>(this T container)
        {
            if (!(container is IDynamicType))
                throw new Exception("Cannot process non-dynamic type.");

            return container;
        }
    }
}