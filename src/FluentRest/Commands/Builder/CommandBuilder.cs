using System;

namespace FluentRest.Commands.Builder
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandBuilderAttribute : Attribute
    {
        public Type Type { get; }

        public CommandBuilderAttribute(Type forType)
        {
            Type = forType;
        }
    }
}