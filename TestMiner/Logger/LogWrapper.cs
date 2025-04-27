namespace TestMiner.Logger
{
    using System;

    using Microsoft.Extensions.Logging;

    using Serilog;
    using Serilog.Extensions.Logging;

    using ILogger = Microsoft.Extensions.Logging.ILogger;

    internal class LogWrapper : ILogWrapper
    {
        private readonly ILogger _logger;

        internal LogWrapper()
            : this(new SerilogLoggerFactory(
                new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File($"Logs\\{nameof(TestMiner)}.log")
                .CreateLogger())
                  .CreateLogger<ILogWrapper>())
        {
        }

        internal LogWrapper(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Debug(string message)
        {
            _logger.LogDebug(message);
        }

        public void Error(string message)
        {
            _logger.LogError(message);
        }

        public void Error(Exception exception, string message)
        {
            _logger.LogError(exception, message);
        }

        public void Info(string message)
        {
            _logger.LogInformation(message);
        }

        public void Warning(string message)
        {
            _logger.LogWarning(message);
        }

        public void Warning(Exception exception, string message)
        {
            _logger.LogWarning(exception, message);
        }
    }
}