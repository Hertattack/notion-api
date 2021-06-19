using System;

namespace RestUtil.Request.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PathAttribute : Attribute
    {
        public string Path { get; }
        public PathAttribute(string path)
        {
            Path = path;
        }
    }
}