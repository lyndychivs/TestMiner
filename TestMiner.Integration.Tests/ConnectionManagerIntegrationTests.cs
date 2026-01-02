namespace TestMiner.Integration.Tests;

using System.IO;

using Moq;

using NUnit.Framework;

using TestMiner.Logger;
using TestMiner.Utility;

[TestFixture]
public class ConnectionManagerIntegrationTests
{
    [Test]
    [Explicit("Limited Mocks Integration Test; Performs IO operations.")]
    public void ConnectionManager_WhenSaveConnectionStringIsCalled_OverwritesConnectionFile()
    {
        File.Delete("Connection.json");

        var mockLogWrapper = new Mock<ILogWrapper>();

        var connectionManager = new ConnectionManager(
            mockLogWrapper.Object,
            new ConnectionConfigurationBuilder(),
            new FileWrapper(mockLogWrapper.Object),
            new ConnectionSerializer());

        var connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";

        connectionManager.SaveConnectionString(connectionString);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(File.Exists("Connection.json"), Is.True);
            Assert.That(File.ReadAllText("Connection.json"), Is.EqualTo(GetConnectionStringContent(connectionString)));
        }
    }

    private static string GetConnectionStringContent(string connectionString)
    {
        return $@"{{""ConnectionString"":""{connectionString}""}}";
    }
}
