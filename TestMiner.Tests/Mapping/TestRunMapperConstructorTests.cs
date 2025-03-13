namespace TestMiner.Tests.Mapping
{
    using System;

    using NUnit.Framework;

    using TestMiner.Mapping;

    [TestFixture]
    public class TestRunMapperConstructorTests
    {
        [Test]
        public void Constructor_ValidParameters_ReturnsTestRunMapper()
        {
            var testRunMapper = new TestRunMapper();

            Assert.That(testRunMapper, Is.Not.Null);
        }

        [Test]
        public void Constructor_NullLogWrapper_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new TestRunMapper(null!));

                Assert.That(ex?.ParamName, Is.EqualTo("logWrapper"));
                Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'logWrapper')"));
            });
        }
    }
}