using System;
using Microsoft.Extensions.DependencyInjection;
using NotionVisualizer.Util;

namespace NotionVisualizer;

internal static class Program
{
    private static int Main(string[] args)
    {
        var serviceProvider = DependencyInjection.CreateServiceProvider();

        var commandLine = new CommandLine(args);

        if (!commandLine.IsValid)
        {
            Console.WriteLine("The following command line options are available:");
            Console.WriteLine(CommandLine.GetDescription());
            return 1;
        }

        var visualizer = serviceProvider.GetService<Visualizer>();

        return visualizer?.Execute(commandLine.OutputPath, commandLine.Clean) ?? 2;
    }
}