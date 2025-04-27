namespace TestMiner.Logger
{
    using System;

    using Microsoft.Extensions.Logging;

    using Serilog;
    using Serilog.Events;
    using Serilog.Extensions.Logging;

    using ILogger = Microsoft.Extensions.Logging.ILogger;

    internal class LogWrapper : ILogWrapper
    {
        private readonly ILogger _logger;

        internal LogWrapper()
            : this($"Logs\\{nameof(TestMiner)}.log")
        {
        }

        internal LogWrapper(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        internal LogWrapper(string logFilePath)
            : this(new SerilogLoggerFactory(
                new LoggerConfiguration()
                .WriteTo.Console(
                    restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo.File(
                    logFilePath,
                    restrictedToMinimumLevel: LogEventLevel.Verbose,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 3)
                .CreateLogger())
                  .CreateLogger<ILogWrapper>())
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(logFilePath);
        }

        public void Error(string message)
        {
            _logger.LogError(message);
        }

        public void Error(Exception exception, string message)
        {
            _logger.LogError(message);
            _logger.LogDebug(exception, message);
        }

        public void Info(string message)
        {
            _logger.LogInformation(message);
        }

        public void Warning(string message)
        {
            _logger.LogWarning(message);
        }
    }
}