namespace TestMiner.Logger
{
    using System;

    using Microsoft.Extensions.Logging;

    internal class LogWrapper : ILogWrapper
    {
        private readonly ILogger _logger;

        internal LogWrapper(Type type)
            : this(LoggerFactory.Create(builder => builder.SetMinimumLevel(LogLevel.Trace).AddConsole()).CreateLogger(type))
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