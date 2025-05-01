namespace TestMiner.Tests.Utility
{
    using System;

    using Moq;

    using NUnit.Framework;

    using TestMiner.Logger;
    using TestMiner.Utility;

    [TestFixture]
    public class FileWrapperConstructorTests
    {
        [Test]
        public void Constructor_ValidParameters_ReturnsFileWrapper()
        {
            var logWrapper = new Mock<ILogWrapper>().Object;

            var fileWrapper = new FileWrapper(logWrapper);

            Assert.That(fileWrapper, Is.Not.Null);
        }

        [Test]
        public void Constructor_NullLogWrapper_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new FileWrapper(null!));

                Assert.That(ex?.ParamName, Is.EqualTo("logWrapper"));
                Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'logWrapper')"));
            });
        }
    }
}