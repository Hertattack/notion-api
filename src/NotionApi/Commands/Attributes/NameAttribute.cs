using System;

namespace NotionApi.Commands.Search.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NameAttribute : Attribute
    {
        public string Name { get; }

        public NameAttribute(string name)
        {
            Name = name;
        }
    }
}