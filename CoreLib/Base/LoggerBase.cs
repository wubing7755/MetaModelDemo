using Serilog;

namespace CoreLib.Base;

public abstract class LoggerBase
{
    protected readonly ILogger _logger;

    protected LoggerBase()
    {
        _logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
    }
}
