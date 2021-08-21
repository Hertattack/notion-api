using Util;

namespace NotionVisualizer.Util
{
    public class CommandLineOption
    {
        public string Name { get; set; }
        public bool Required { get; set; }
        public string Description { get; set; }
        public bool HasValue { get; set; }

        public Option<string> Default { get; set; }
    }
}