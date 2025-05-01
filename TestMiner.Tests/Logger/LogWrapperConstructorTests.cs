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
        public void Constructor_ValidLogger_ReturnsLogWrapper()
        {
            var logger = new Mock<ILogger>();

            var logWrapper = new LogWrapper(logger.Object);

            Assert.That(logWrapper, Is.Not.Null);
        }

        [Test]
        public void Constructor_NullILogger_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new LogWrapper(null!));

                Assert.That(ex?.ParamName, Is.EqualTo("logger"));
                Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'logger')"));
            });
        }
    }
}