namespace TestMiner.Tests.DataAccessLayer;

using System;

using Moq;

using NUnit.Framework;

using TestMiner.DataAccessLayer;
using TestMiner.Database;
using TestMiner.Logger;

[TestFixture]
public class TestMinerDalConstructorTests
{
    private readonly Mock<IDatabase> _mockDatabase = new();

    private readonly Mock<ILogWrapper> _mockLogWrapper = new();

    [Test]
    public void Constructor_ValidParameters_ReturnstestMinerDal()
    {
        var testMinerDal = new TestMinerDal(_mockLogWrapper.Object, _mockDatabase.Object);

        Assert.That(testMinerDal, Is.Not.Null);
    }

    [Test]
    public void Constructor_NullLogWrapper_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new TestMinerDal(null!, _mockDatabase.Object));

            Assert.That(ex?.ParamName, Is.EqualTo("logWrapper"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'logWrapper')"));
        });
    }

    [Test]
    public void Constructor_NullDatabase_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new TestMinerDal(_mockLogWrapper.Object, database: null!));

            Assert.That(ex?.ParamName, Is.EqualTo("database"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'database')"));
        });
    }

    [TestCase("")]
    [TestCase(" ")]
    public void Constructor_InvalidConnectionString_ThrowsArgumentException(string? connectionString)
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentException>(() => new TestMinerDal(_mockLogWrapper.Object, connectionString!));

            Assert.That(ex?.ParamName, Is.EqualTo("connectionString"));
            Assert.That(ex?.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'connectionString')"));
        });
    }

    [Test]
    public void Constructor_NullConnectionString_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new TestMinerDal(_mockLogWrapper.Object, connectionString: null!));

            Assert.That(ex?.ParamName, Is.EqualTo("connectionString"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'connectionString')"));
        });
    }

    [Test]
    public void ConstructorTwo_NullLogWrapper_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new TestMinerDal(null!, "Data Source=localhost\\Database=DatabaseName;"));

            Assert.That(ex?.ParamName, Is.EqualTo("logWrapper"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'logWrapper')"));
        });
    }
}
