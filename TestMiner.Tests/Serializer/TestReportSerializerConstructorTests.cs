namespace TestMiner.Tests.Serializer
{
    using System;

    using NUnit.Framework;

    using TestMiner.Serializer;

    [TestFixture]
    public class TestReportSerializerConstructorTests
    {
        [Test]
        public void Constructor_ValidParameters_ReturnsTestReportSerializer()
        {
            var testReportSerializer = new TestReportSerializer();

            Assert.That(testReportSerializer, Is.Not.Null);
        }

        [Test]
        public void Constructor_NullLogWrapper_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new TestReportSerializer(null!));

                Assert.That(ex?.ParamName, Is.EqualTo("logWrapper"));
                Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'logWrapper')"));
            });
        }
    }
}