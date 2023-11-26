namespace SocializR.DataAccess.Logger;

class SocializRConsoleLoggerFactory : ILoggerFactory
{
    private static readonly ILoggerProvider _loggerProvider = new ConsoleLoggerProvider();

    public void AddProvider(ILoggerProvider provider)
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return _loggerProvider.CreateLogger(categoryName);
    }

    public void Dispose()
    {
        _loggerProvider?.Dispose();
    }
}

class ConsoleLoggerProvider : ILoggerProvider
{
    private static readonly ILogger _logger = new ConsoleLogger();

    public ILogger CreateLogger(string categoryName)
    {
        return _logger;
    }

    public void Dispose()
    {
    }
}

class ConsoleLogger : ILogger
{
    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (eventId.Id != 20101)
            return;

        Console.WriteLine($"{eventId.Id} {state}");
        Console.WriteLine();
    }
}
