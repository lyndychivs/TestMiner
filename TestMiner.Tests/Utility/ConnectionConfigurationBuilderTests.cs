namespace TestMiner.Tests.Utility;

using System;

using Microsoft.Extensions.Configuration;

using Moq;

using NUnit.Framework;

using TestMiner.Utility;

[TestFixture]
public class ConnectionConfigurationBuilderTests
{
    private readonly Mock<IConfigurationBuilder> _mockConfigurationBuilder = new();

    [Test]
    public void BuildConnection_EmptyFilePath_ThrowsArgumentException()
    {
        var connectionConfigurationBuilder = new ConnectionConfigurationBuilder(_mockConfigurationBuilder.Object);

        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentException>(() => connectionConfigurationBuilder.BuildConnection(string.Empty));

            Assert.That(ex?.Message, Does.Contain("File path must be a non-empty string. (Parameter 'path')"));
        });
    }

    [Test]
    public void BuildConnection_WhitespaceFilePath_ThrowsNullReferenceException()
    {
        var connectionConfigurationBuilder = new ConnectionConfigurationBuilder(_mockConfigurationBuilder.Object);

        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<NullReferenceException>(() => connectionConfigurationBuilder.BuildConnection(" "));

            Assert.That(ex?.Message, Does.Contain("Object reference not set to an instance of an object."));
        });
    }

    [Test]
    public void BuildConnection_NullFilePath_ThrowsArgumentException()
    {
        var connectionConfigurationBuilder = new ConnectionConfigurationBuilder(_mockConfigurationBuilder.Object);

        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentException>(() => connectionConfigurationBuilder.BuildConnection(null!));

            Assert.That(ex?.Message, Does.Contain("File path must be a non-empty string. (Parameter 'path')"));
        });
    }
}
