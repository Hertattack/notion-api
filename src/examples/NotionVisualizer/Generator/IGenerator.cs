using System.Collections.Generic;
using NotionApi.Cache;
using NotionApi.Rest.Objects;

namespace NotionVisualizer.Generator
{
    public interface IGenerator
    {
        void Generate(string outputPath, INotionCache cache, IList<NotionObject> notionObjects);
    }
}