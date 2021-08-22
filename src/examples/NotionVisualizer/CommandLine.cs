using System;
using System.Collections.Generic;
using NotionVisualizer.Util;
using Util;

namespace NotionVisualizer
{
    public class CommandLine
    {
        private static readonly CommandLineOption _sourceOption = new()
        {
            Name = "source",
            Required = false,
            Description = "The source json files to use instead of sending requests to Notion.",
            HasValue = true
        };

        private static readonly CommandLineOption _outputOption = new()
        {
            Name = "output",
            Required = true,
            Description = "The output folder to use.",
            HasValue = true
        };

        private static readonly CommandLineOption _cleanOption = new()
        {
            Name = "clean",
            Required = false,
            Description = "Clean the deployment folder before deploying.",
            HasValue = false
        };

        private static readonly CommandLineParser _parser = new(_sourceOption, _outputOption, _cleanOption);

        public static string GetDescription() =>
            _parser.GetDescription();

        public bool IsValid { get; }

        public bool Clean { get; }

        public Option<string> RequestSourcePath { get; }

        public string OutputPath { get; }

        public CommandLine(IEnumerable<string> args)
        {
            var valuesOption = Parse(args);

            IsValid = valuesOption.HasValue;

            if (!IsValid)
                return;

            foreach (var value in valuesOption.Value)
            {
                var optionName = value.Option.Name;
                if (optionName == _cleanOption.Name)
                    Clean = true;
                else if (optionName == _outputOption.Name)
                    OutputPath = value.Value;
                else if (optionName == _sourceOption.Name)
                    RequestSourcePath = value.Value;
            }
        }

        private static Option<IEnumerable<CommandLineOptionValue>> Parse(IEnumerable<string> args)
        {
            try
            {
                return Option<IEnumerable<CommandLineOptionValue>>.From(_parser.Parse(args));
            }
            catch
            {
                Console.WriteLine("The following command line options are available:");
                Console.WriteLine(_parser.GetDescription());
            }

            return Option.None;
        }
    }
}