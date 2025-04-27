namespace TestMiner.Tests.Logger
{
    using System;

    using Microsoft.Extensions.Logging;

    using Moq;

    using NUnit.Framework;

    using TestMiner.Logger;

    [TestFixture]
    public class LogWrapperTests
    {
        private readonly Mock<ILogger> _mockLogger = new();

        private readonly LogWrapper _logWrapper;

        public LogWrapperTests()
        {
            _logWrapper = new LogWrapper(_mockLogger.Object);
        }

        [Test]
        public void Info_WhenCalled_CallsLogInformation()
        {
            _logWrapper.Info("a");

            _mockLogger.VerifyLogging("a", LogLevel.Information, Times.Once());
        }

        [Test]
        public void Warning_WhenCalled_CallsLogWarning()
        {
            _logWrapper.Warning("a");

            _mockLogger.VerifyLogging("a", LogLevel.Warning, Times.Once());
        }

        [Test]
        public void Error_WhenCalled_CallsLogError()
        {
            _logWrapper.Error("b");

            _mockLogger.VerifyLogging("b", LogLevel.Error, Times.Once());
        }

        [Test]
        public void Error_WhenCalledWithException_CallsLogErrorAndDebug()
        {
            var exception = new Exception("a");

            _logWrapper.Error(exception, "b");

            _mockLogger.VerifyLogging("b", LogLevel.Error, Times.Once());
            _mockLogger.VerifyLogging("b", LogLevel.Debug, Times.Once());
        }
    }
}