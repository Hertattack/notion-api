using System;
using Microsoft.Extensions.Logging;

namespace NotionGraphDatabase.Test.Util;

public class NullLogger<TLogTarget> : ILogger<TLogTarget>
{
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return false;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return new NullDisposable();
    }
}