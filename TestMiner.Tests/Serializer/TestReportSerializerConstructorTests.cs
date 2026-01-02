namespace TestMiner.Tests.Serializer;

using System;

using Moq;

using NUnit.Framework;

using TestMiner.Logger;
using TestMiner.Serializer;

[TestFixture]
public class TestReportSerializerConstructorTests
{
    [Test]
    public void Constructor_ValidParameters_ReturnsTestReportSerializer()
    {
        var logWrapper = new Mock<ILogWrapper>().Object;

        var testReportSerializer = new TestReportSerializer(logWrapper);

        Assert.That(testReportSerializer, Is.Not.Null);
    }

    [Test]
    public void Constructor_NullLogWrapper_ThrowsArgumentNullException()
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new TestReportSerializer(null!));

            Assert.That(ex?.ParamName, Is.EqualTo("logWrapper"));
            Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'logWrapper')"));
        }
    }
}
