

using Serilog;
using Serilog.Core;

public struct SEQBootstrapper
{
    private Logger logger;
    public SEQBootstrapper()
    {
        logger = new LoggerConfiguration()
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();
    }

    public void Write(LogCategory _cat,  string _message)
    {
        switch(_cat)
        {
            case LogCategory.Information:
                logger.Information(_message);
                    break;
            case LogCategory.Warning:
                logger.Warning(_message);
                break;
            case LogCategory.Error:
                logger.Error(_message);
                break;
        }
        
        logger.Dispose();
    }
}