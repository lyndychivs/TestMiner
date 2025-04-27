namespace TestMiner.Tests.Logger
{
    using System;

    using Microsoft.Extensions.Logging;

    using Moq;

    using NUnit.Framework;

    using TestMiner.Logger;

    [TestFixture]
    public class LogWrapperConstructorTests
    {
        [Test]
        public void Constructor_WhenCalled_ReturnsLogWrapper()
        {
            var logWrapper = new LogWrapper();

            Assert.That(logWrapper, Is.Not.Null);
        }

        [Test]
        public void Constructor_ValidLogger_ReturnsLogWrapper()
        {
            var logger = new Mock<ILogger>();

            var logWrapper = new LogWrapper(logger.Object);

            Assert.That(logWrapper, Is.Not.Null);
        }

        [Test]
        public void Constructor_ValidLogFilePath_ReturnsLogWrapper()
        {
            var logWrapper = new LogWrapper("a");

            Assert.That(logWrapper, Is.Not.Null);
        }

        [TestCase("")]
        [TestCase(" ")]
        public void Constructor_InvalidLogFilePath_ThrowsArgumentException(string logFilePath)
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentException>(() => new LogWrapper(logFilePath!));

                Assert.That(ex?.ParamName, Is.EqualTo("logFilePath"));
                Assert.That(ex?.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'logFilePath')"));
            });
        }

        [Test]
        public void Constructor_NullLogFilePath_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new LogWrapper(logFilePath: null!));

                Assert.That(ex?.ParamName, Is.EqualTo("path"));
                Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'path')"));
            });
        }

        [Test]
        public void Constructor_NullILogger_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new LogWrapper(logger: null!));

                Assert.That(ex?.ParamName, Is.EqualTo("logger"));
                Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'logger')"));
            });
        }
    }
}