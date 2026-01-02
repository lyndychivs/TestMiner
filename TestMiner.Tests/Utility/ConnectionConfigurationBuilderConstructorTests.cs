namespace TestMiner.Tests.Utility;

using System;

using Microsoft.Extensions.Configuration;

using Moq;

using NUnit.Framework;

using TestMiner.Utility;

[TestFixture]
public class ConnectionConfigurationBuilderConstructorTests
{
    private readonly Mock<IConfigurationBuilder> _configurationBuilderMock = new();

    [Test]
    public void Constructor_WhenCalled_ReturnsConnectionConfigurationBuilder()
    {
        var connectionConfigurationBuilder = new ConnectionConfigurationBuilder();

        Assert.That(connectionConfigurationBuilder, Is.Not.Null);
    }

    [Test]
    public void Constructor_WithValidParameters_ReturnsConnectionConfigurationBuilder()
    {
        var connectionConfigurationBuilder = new ConnectionConfigurationBuilder(_configurationBuilderMock.Object);

        Assert.That(connectionConfigurationBuilder, Is.Not.Null);
    }

    [Test]
    public void Constructor_NullConfigurationBuilder_ThrowsArgumentNullException()
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ConnectionConfigurationBuilder(null!));

            Assert.That(ex?.ParamName, Is.EqualTo("configurationBuilder"));
            Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'configurationBuilder')"));
        }
    }
}
