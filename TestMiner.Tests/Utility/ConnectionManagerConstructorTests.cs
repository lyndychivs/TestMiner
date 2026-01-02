namespace TestMiner.Tests.Utility;

using System;

using Moq;

using NUnit.Framework;

using TestMiner.Logger;
using TestMiner.Utility;

[TestFixture]
public class ConnectionManagerConstructorTests
{
    private readonly Mock<ILogWrapper> _mockLogWrapper = new();

    private readonly Mock<IConnectionConfigurationBuilder> _mockConnectionConfigurationBuilder = new();

    private readonly Mock<IFileWrapper> _mockFileWrapper = new();

    private readonly Mock<IConnectionSerializer> _mockConnectionSerializer = new();

    [Test]
    public void Constructor_ValidParameters_ReturnsConnectionManager()
    {
        var connectionManager = new ConnectionManager(_mockLogWrapper.Object, _mockConnectionConfigurationBuilder.Object, _mockFileWrapper.Object, _mockConnectionSerializer.Object);

        Assert.That(connectionManager, Is.Not.Null);
    }

    [Test]
    public void Constructor_NullLogWrapper_ThrowsArgumentNullException()
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ConnectionManager(null!, _mockConnectionConfigurationBuilder.Object, _mockFileWrapper.Object, _mockConnectionSerializer.Object));

            Assert.That(ex?.ParamName, Is.EqualTo("logWrapper"));
            Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'logWrapper')"));
        }
    }

    [Test]
    public void ConstructorTwo_NullLogWrapper_ThrowsArgumentNullException()
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ConnectionManager(null!));

            Assert.That(ex?.ParamName, Is.EqualTo("logWrapper"));
            Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'logWrapper')"));
        }
    }

    [Test]
    public void Constructor_NullConnectionConfigurationBuilder_ThrowsArgumentNullException()
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ConnectionManager(_mockLogWrapper.Object, null!, _mockFileWrapper.Object, _mockConnectionSerializer.Object));

            Assert.That(ex?.ParamName, Is.EqualTo("connectionConfigurationBuilder"));
            Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'connectionConfigurationBuilder')"));
        }
    }

    [Test]
    public void Constructor_NullFileWrapper_ThrowsArgumentNullException()
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ConnectionManager(_mockLogWrapper.Object, _mockConnectionConfigurationBuilder.Object, null!, _mockConnectionSerializer.Object));

            Assert.That(ex?.ParamName, Is.EqualTo("fileWrapper"));
            Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'fileWrapper')"));
        }
    }

    [Test]
    public void Constructor_NullConnectionSerializer_ThrowsArgumentNullException()
    {
        using (Assert.EnterMultipleScope())
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ConnectionManager(_mockLogWrapper.Object, _mockConnectionConfigurationBuilder.Object, _mockFileWrapper.Object, null!));

            Assert.That(ex?.ParamName, Is.EqualTo("connectionSerializer"));
            Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'connectionSerializer')"));
        }
    }
}
