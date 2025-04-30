namespace TestMiner.Logger
{
    using System;

    using Microsoft.Extensions.Logging;

    internal class LogWrapper : ILogWrapper
    {
        private readonly ILogger _logger;

        internal LogWrapper(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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