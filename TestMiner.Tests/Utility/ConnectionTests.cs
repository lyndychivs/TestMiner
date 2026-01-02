namespace TestMiner.Tests.Utility;

using NUnit.Framework;

using TestMiner.Utility;

[TestFixture]
public class ConnectionTests
{
    [Test]
    public void Constructor_WhenCalled_ReturnsConnection()
    {
        var connection = new Connection();

        Assert.That(connection, Is.Not.Null);
        Assert.That(connection.ConnectionString, Is.EqualTo(string.Empty));
    }
}
