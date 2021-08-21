namespace NotionVisualizer.Util
{
    public class CommandLineOptionValue
    {
        public CommandLineOptionValue(CommandLineOption option, string value)
        {
            Option = option;
        }

        public CommandLineOption Option { get; }
        public string Value { get; }
    }
}