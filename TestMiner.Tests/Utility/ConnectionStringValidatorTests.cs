namespace TestMiner.Tests.Utility;

using Moq;

using NUnit.Framework;

using TestMiner.Logger;
using TestMiner.Utility;

[TestFixture]
public class ConnectionStringValidatorTests
{
    [Test]
    public void IsConnectionStringValid_ValidConnectionString_ReturnsTrue()
    {
        var mockLogWrapper = new Mock<ILogWrapper>();
        var connectionStringValidator = new ConnectionStringValidator(mockLogWrapper.Object);

        var result = connectionStringValidator.IsConnectionStringValid("Server=localhost;Database=TestDB;User Id=testuser;Password=testpassword;");

        Assert.That(result, Is.True);
    }

    [TestCase("", "Connection String is empty or null.")]
    [TestCase(" ", "Connection String is empty or null.")]
    [TestCase(null, "Connection String is empty or null.")]
    [TestCase("a", "Connection String is invalid.")]
    public void IsConnectionStringValid_InvalidConnectionString_ReturnsFalse(string? connectionString, string expectedLog)
    {
        var mockLogWrapper = new Mock<ILogWrapper>();
        var connectionStringValidator = new ConnectionStringValidator(mockLogWrapper.Object);

        var result = connectionStringValidator.IsConnectionStringValid(connectionString!);

        Assert.That(result, Is.False);
        mockLogWrapper.Verify(log => log.Error(expectedLog), Times.Once);
    }
}