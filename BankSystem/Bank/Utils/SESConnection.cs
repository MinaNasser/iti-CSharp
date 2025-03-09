using Serilog;
using Serilog.Core;

public class SEQConnection : IDisposable
{
    private static readonly Logger _logger;

    static SEQConnection()
    {
        _logger = new LoggerConfiguration()
            .WriteTo.Seq("http://localhost:5341/")
            .CreateLogger();
    }

    public void Write(LogCategory cat, string message)
    {
        switch (cat)
        {
            case LogCategory.Error:
                _logger.Error(message);
                break;
            case LogCategory.Warning:
                _logger.Warning(message);
                break;
            case LogCategory.Information:
                _logger.Information(message);
                break;
        }
    }

    public void Dispose()
    {
        _logger.Dispose();
    }
}
