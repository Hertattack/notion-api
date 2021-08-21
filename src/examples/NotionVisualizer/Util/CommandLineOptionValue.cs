namespace NotionVisualizer.Util
{
    public class CommandLineOptionValue
    {
        public CommandLineOptionValue(CommandLineOption option, string value)
        {
            Option = option;
            Value = value;
            HasValue = true;
        }

        public CommandLineOptionValue(CommandLineOption option)
        {
            Option = option;
            HasValue = false;
        }

        public CommandLineOption Option { get; }
        public string Value { get; }

        public bool HasValue { get; }
    }
}