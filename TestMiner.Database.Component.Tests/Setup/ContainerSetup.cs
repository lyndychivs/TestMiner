namespace TestMiner.Database.Component.Tests.Setup;

using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

using NUnit.Framework;

internal static class ContainerSetup
{
    private const string SqlUser = "sa";
    private const string DatabaseName = "TestMiner";
    private const string Endpoint = "localhost";
    private const int Port = 1433;

    private static readonly SemaphoreSlim _semaphore = new (1, 1);

    private static IContainer? _dbContainer;

    private static string? _connectionString;

    public static async Task<string> CreateTestMinerDatabase()
    {
        await _semaphore.WaitAsync();
        try
        {
            if (_connectionString is not null)
            {
                return _connectionString;
            }

            string password = TestContext.Parameters.Get("SqlPassword", string.Empty);
            if (string.IsNullOrWhiteSpace(password))
            {
                Assert.Fail("Environment variable; SqlPassword not set.");
            }

            _dbContainer = new ContainerBuilder("testminer-db:latest")
                .WithName($"testminer-db-{Guid.NewGuid()}")
                .WithPortBinding(Port, true)
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithEnvironment("MSSQL_PID", "Express")
                .WithEnvironment("MSSQL_SA_PASSWORD", password)
                .WithEnvironment("MSSQL_PORT", Port.ToString(CultureInfo.InvariantCulture))
                .WithWaitStrategy(Wait.ForUnixContainer()
                    .UntilMessageIsLogged("TestMiner.Database deployed successfully!"))
                .Build();

            await _dbContainer.StartAsync().ConfigureAwait(false);

            _connectionString = $"Data Source={Endpoint},{_dbContainer.GetMappedPublicPort(Port)};Database={DatabaseName};User ID={SqlUser};Password={password};Encrypt=true;TrustServerCertificate=true;";

            return _connectionString;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public static async Task CleanupTestMinerDatabase()
    {
        await _semaphore.WaitAsync();
        try
        {
            if (_dbContainer is not null)
            {
                await _dbContainer.StopAsync().ConfigureAwait(false);
                await _dbContainer.DisposeAsync().ConfigureAwait(false);
                _dbContainer = null;
                _connectionString = null;
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
