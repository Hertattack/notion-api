using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotionVisualizer.Util
{
    public class CommandLineParser
    {
        private readonly CommandLineOption[] _options;
        private readonly IDictionary<string, CommandLineOption> _argumentNames;
        private readonly IReadOnlyList<CommandLineOption> _required;

        public CommandLineParser(params CommandLineOption[] options)
        {
            _options = options;
            _argumentNames = options.ToDictionary(o => o.Name);
            _required = options.Where(o => o.Required).ToList();
        }

        public IEnumerable<CommandLineOptionValue> Parse(IEnumerable<string> args)
        {
            var values = ParseValues(args).ToList();

            var duplicates = values
                .GroupBy(v => v.Option)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicates.Any())
                throw new ArgumentException($"There are one or more duplicate arguments: {string.Join(", ", duplicates)}");

            var missing = _required
                .Except(values.Select(v => v.Option))
                .ToList();

            if (missing.Any(m => !m.Default.HasValue))
                throw new ArgumentException($"Missing arguments: {string.Join(", ", missing.Where(m => !m.Default.HasValue))}");

            values.AddRange(missing.Select(m => new CommandLineOptionValue(m, m.Default.Value)));

            return values;
        }

        private IEnumerable<CommandLineOptionValue> ParseValues(IEnumerable<string> args)
        {
            var values = new List<CommandLineOptionValue>();

            CommandLineOption context = null;
            var valueFound = false;
            foreach (var arg in args)
            {
                if (arg.StartsWith("--"))
                {
                    valueFound = false;
                    if (context != null)
                        throw new ArgumentException($"Expected value for command line option: {context.Name}.");

                    var optionName = arg[2..];
                    if (!_argumentNames.TryGetValue(optionName, out context))
                    {
                        throw new ArgumentException($"Unexpected command line option: {optionName}");
                    }

                    if (!context.HasValue)
                    {
                        values.Add(new CommandLineOptionValue(context));
                        context = null;
                    }
                }
                else
                {
                    if (context is null)
                        throw new ArgumentException($"Value found without argument name: {arg}");

                    if (valueFound)
                        throw new ArgumentException($"Multiple values for option: {context.Name}");

                    valueFound = true;
                    values.Add(new CommandLineOptionValue(context, arg));
                    context = null;
                }
            }

            return values;
        }

        public string GetDescription()
        {
            var stringBuilder = new StringBuilder();
            foreach (var option in _options.OrderBy(o => !o.Required).ThenBy(o => o.Name))
            {
                string optionName = null;
                if (option.Required)
                    optionName = option.Name;
                else
                    optionName = $"[{option.Name}]";

                stringBuilder.Append($"{optionName} : {option.Description}{Environment.NewLine}");
                if (option.HasValue)
                {
                    stringBuilder.Append($"\t--{option.Name} <value>{Environment.NewLine}");
                    if (option.Default.HasValue)
                        stringBuilder.Append($"\tdefault value: {option.Default}{Environment.NewLine}");
                }
                else
                    stringBuilder.Append($"\t--{option.Name}{Environment.NewLine}");
            }

            return stringBuilder.ToString();
        }
    }
}