using System;

namespace NotionApi.Request.Attributes
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