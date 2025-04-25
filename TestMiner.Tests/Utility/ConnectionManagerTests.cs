namespace TestMiner.Tests.Utility
{
    using System;
    using System.IO;

    using Moq;

    using NUnit.Framework;

    using TestMiner.Logger;

    using TestMiner.Utility;

    [TestFixture]
    public class ConnectionManagerTests
    {
        private const string FileName = "Connection.json";

        private readonly Mock<ILogWrapper> _mockLogWrapper;

        private readonly Mock<IConnectionConfigurationBuilder> _mockConnectionConfigurationBuilder;

        private readonly Mock<IFileWrapper> _mockFileWrapper;

        private readonly Mock<IConnectionSerializer> _mockConnectionSerializer;

        private readonly ConnectionManager _connectionManager;

        public ConnectionManagerTests()
        {
            _mockLogWrapper = new Mock<ILogWrapper>();
            _mockConnectionConfigurationBuilder = new Mock<IConnectionConfigurationBuilder>();
            _mockFileWrapper = new Mock<IFileWrapper>();
            _mockConnectionSerializer = new Mock<IConnectionSerializer>();

            _connectionManager = new ConnectionManager(
                _mockLogWrapper.Object,
                _mockConnectionConfigurationBuilder.Object,
                _mockFileWrapper.Object,
                _mockConnectionSerializer.Object);
        }

        [Test]
        public void GetConnectionString_ValidConnectionString_ReturnsConnectionString()
        {
            var connectionString = _connectionManager.GetConnectionString("a");

            Assert.That(connectionString, Is.EqualTo("a"));
            _mockLogWrapper.Verify(log => log.Info("Connection String provided as parameter."), Times.Once);
        }

        [Test]
        public void GetConnectionString_InvalidConnectionString_ReturnsConnectionStringFromFile()
        {
            _mockFileWrapper.Setup(file => file.Exists(FileName)).Returns(true);
            _mockConnectionConfigurationBuilder.Setup(builder => builder.BuildConnection(FileName)).Returns(new Connection { ConnectionString = "a" });

            var connectionString = _connectionManager.GetConnectionString(string.Empty);

            Assert.That(connectionString, Is.EqualTo("a"));
            _mockLogWrapper.Verify(log => log.Info($"Connection String not provided as parameter. Getting Connection String from {FileName}"), Times.Once);
        }

        [Test]
        public void GetConnectionString_FileNotFound_ThrowsFileNotFoundException()
        {
            _mockFileWrapper.Setup(file => file.Exists(FileName)).Returns(false);

            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<FileNotFoundException>(() => _connectionManager.GetConnectionString(string.Empty));

                Assert.That(ex?.FileName, Is.EqualTo(FileName));
                _mockLogWrapper.Verify(log => log.Error(It.IsAny<FileNotFoundException>(), $"Connection String file not found: {FileName}"), Times.Once);
            });
        }

        [Test]
        public void GetConnectionString_NullConnection_ThrowsNullReferenceException()
        {
            _mockFileWrapper.Setup(file => file.Exists(FileName)).Returns(true);
            _mockConnectionConfigurationBuilder.Setup(builder => builder.BuildConnection(FileName)).Returns((Connection)null!);

            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<NullReferenceException>(() => _connectionManager.GetConnectionString(string.Empty));

                Assert.That(ex?.Message, Is.EqualTo($"connection cannot be null."));
                _mockLogWrapper.Verify(log => log.Error(It.IsAny<NullReferenceException>(), $"Connection String not found in {FileName} or provided via Commandline arguments."), Times.Once);
            });
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]

        public void GetConnectionString_InvalidConnectionString_ThrowsNullReferenceException(string? connectionString)
        {
            _mockFileWrapper.Setup(file => file.Exists(FileName)).Returns(true);
            _mockConnectionConfigurationBuilder.Setup(builder => builder.BuildConnection(FileName)).Returns(new Connection { ConnectionString = connectionString! });

            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<NullReferenceException>(() => _connectionManager.GetConnectionString(string.Empty));

                Assert.That(ex?.Message, Is.EqualTo($"ConnectionString cannot be null."));
                _mockLogWrapper.Verify(log => log.Error(It.IsAny<NullReferenceException>(), $"Connection String not found in {FileName} or provided via Commandline arguments."), Times.Once);
            });
        }

        [Test]
        public void SaveConnectionString_ValidConnectionString_ReturnsZero()
        {
            _mockConnectionSerializer.Setup(serializer => serializer.Serialize(It.IsAny<Connection>())).Returns("b");
            _mockFileWrapper.Setup(file => file.WriteAllText(It.IsAny<string>(), "b"));

            var result = _connectionManager.SaveConnectionString("a");

            Assert.That(result, Is.EqualTo(0));
            _mockConnectionSerializer.Verify(
                serializer => serializer.Serialize(
                It.Is<Connection>(
                    c =>
                    c.ConnectionString.Equals("a"))),
                Times.Once);
            _mockLogWrapper.Verify(log => log.Info($"Connection String saved."), Times.Once);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void SaveConnectionString_InvalidConnectionString_ReturnsOne(string? connectionString)
        {
            var result = _connectionManager.SaveConnectionString(connectionString!);

            Assert.That(result, Is.EqualTo(1));
            _mockLogWrapper.Verify(log => log.Error(It.IsAny<ArgumentNullException>(), $"Connection String cannot be null or empty."), Times.Once);
        }

        [Test]
        public void SaveConnectionString_SerializerThrowsException_ReturnsOne()
        {
            _mockConnectionSerializer.Setup(serializer => serializer.Serialize(It.IsAny<Connection>())).Throws(new Exception());

            var result = _connectionManager.SaveConnectionString("a");

            Assert.That(result, Is.EqualTo(1));
            _mockLogWrapper.Verify(log => log.Error(It.IsAny<Exception>(), $"Failed to Save Connection String."), Times.Once);
        }

        [Test]
        public void SaveConnectionString_WriteAllTextThrowsExection_ReturnsOne()
        {
            _mockConnectionSerializer.Setup(serializer => serializer.Serialize(It.IsAny<Connection>())).Returns("a");
            _mockFileWrapper.Setup(file => file.WriteAllText(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            var result = _connectionManager.SaveConnectionString("a");

            Assert.That(result, Is.EqualTo(1));
            _mockLogWrapper.Verify(log => log.Error(It.IsAny<Exception>(), $"Failed to Save Connection String."), Times.Once);
        }
    }
}