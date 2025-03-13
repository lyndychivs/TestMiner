namespace TestMiner.Tests.Database
{
    using System;
    using System.Data;

    using Moq;

    using NUnit.Framework;

    using TestMiner.Database;
    using TestMiner.Logger;

    [TestFixture]
    public class DatabaseConstructorTests
    {
        private readonly Mock<ILogWrapper> _mockLogWrapper = new();

        private readonly Mock<IDbConnection> _mockDbConnection = new();

        private readonly Mock<IDynamicParametersWrapper> _mockDynamicParametersWrapper = new();

        [Test]
        public void Constructor_ValidParameters_ReturnsDatabase()
        {
            var database = new Database(_mockLogWrapper.Object, _mockDbConnection.Object, _mockDynamicParametersWrapper.Object);

            Assert.That(database, Is.Not.Null);
        }

        [Test]
        public void Constructor_ValidDbConnection_ReturnsDatabase()
        {
            var database = new Database(_mockDbConnection.Object);

            Assert.That(database, Is.Not.Null);
        }

        [Test]
        public void Constructor_NullLogWrapper_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new Database(null!, _mockDbConnection.Object, _mockDynamicParametersWrapper.Object));

                Assert.That(ex?.ParamName, Is.EqualTo("logWrapper"));
                Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'logWrapper')"));
            });
        }

        [Test]
        public void Constructor_NullDbConnection_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new Database(_mockLogWrapper.Object, null!, _mockDynamicParametersWrapper.Object));

                Assert.That(ex?.ParamName, Is.EqualTo("dbConnection"));
                Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'dbConnection')"));
            });
        }

        [Test]
        public void Constructor_NullDynamicParametersWrapper_ThrowsArgumentNullException()
        {
            Assert.Multiple(() =>
            {
                var ex = Assert.Throws<ArgumentNullException>(() => new Database(_mockLogWrapper.Object, _mockDbConnection.Object, null!));

                Assert.That(ex?.ParamName, Is.EqualTo("dynamicParametersWrapper"));
                Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'dynamicParametersWrapper')"));
            });
        }
    }
}