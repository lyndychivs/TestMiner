namespace TestMiner.Tests.Utility
{
    using System;

    using Moq;

    using NUnit.Framework;

    using TestMiner.Logger;
    using TestMiner.Utility;

    [TestFixture]
    public class ConnectionStringValidatorConstructorTests
    {
        [Test]
        public void Constructor_WhenCalled_ReturnsConnectionStringValidator()
        {
            var connectionStringValidator = new ConnectionStringValidator();

            Assert.That(connectionStringValidator, Is.Not.Null);
        }

        [Test]
        public void Constructor_WithLogWrapper_ReturnsConnectionStringValidator()
        {
            var mockLogWrapper = new Mock<ILogWrapper>();

            var connectionStringValidator = new ConnectionStringValidator(mockLogWrapper.Object);

            Assert.That(connectionStringValidator, Is.Not.Null);
        }

        [Test]
        public void Constructor_NullLogWrapper_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new ConnectionStringValidator(null!));

                Assert.That(ex?.ParamName, Is.EqualTo("logWrapper"));
                Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'logWrapper')"));
            });
        }
    }
}
