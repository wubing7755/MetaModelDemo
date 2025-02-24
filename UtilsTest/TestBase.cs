using Serilog;
using Xunit.Abstractions;

namespace UtilsTest;

public abstract class TestBase
{
    protected readonly ITestOutputHelper _output;
    
    protected readonly ILogger _logger;

    protected TestBase(ITestOutputHelper testOutputHelper)
    {
        _output = testOutputHelper;

        _logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("testBase.log",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();
    }
}
