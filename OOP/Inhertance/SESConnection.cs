
using Serilog;
using Serilog.Core;

public class SEQConnection
{
    private Logger _logger;
    public SEQConnection()
    {
        _logger = new LoggerConfiguration()
            .WriteTo.Seq("http://localhost:5341/")
            .CreateLogger();
    }
    public void write(LogCategory _cat , string _Message)
    {
        switch (_cat)
        {
            case LogCategory.Error:
                _logger.Error(_Message);
                break;
            case LogCategory.Warning:
                _logger.Warning(_Message);
                break;
            case LogCategory.Information:
                _logger.Information(_Message);
                break;
        }
        _logger.Dispose();

    }


}