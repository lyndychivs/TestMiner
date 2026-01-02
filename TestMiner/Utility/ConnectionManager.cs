namespace TestMiner.Utility;

using System;

using TestMiner.Logger;

public class ConnectionManager
{
    private readonly ILogWrapper _logWrapper;

    private readonly IConnectionConfigurationBuilder _connectionConfigurationBuilder;

    private readonly IFileWrapper _fileWrapper;

    private readonly IConnectionSerializer _connectionSerializer;

    private readonly string _connectionFileName;

    public ConnectionManager(ILogWrapper logWrapper)
        : this(
              logWrapper,
              new ConnectionConfigurationBuilder(),
              new FileWrapper(logWrapper),
              new ConnectionSerializer())
    {
    }

    internal ConnectionManager(
        ILogWrapper logWrapper,
        IConnectionConfigurationBuilder connectionConfigurationBuilder,
        IFileWrapper fileWrapper,
        IConnectionSerializer connectionSerializer)
    {
        _logWrapper = logWrapper ?? throw new ArgumentNullException(nameof(logWrapper));
        _connectionConfigurationBuilder = connectionConfigurationBuilder ?? throw new ArgumentNullException(nameof(connectionConfigurationBuilder));
        _fileWrapper = fileWrapper ?? throw new ArgumentNullException(nameof(fileWrapper));
        _connectionSerializer = connectionSerializer ?? throw new ArgumentNullException(nameof(connectionSerializer));

        _connectionFileName = $"{nameof(Connection)}.json";
    }

    public string GetConnectionString(string connectionString)
    {
        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            _logWrapper.Info($"Connection String provided as parameter.");

            return connectionString;
        }

        _logWrapper.Info($"Fetching Connection String from {_connectionFileName}");

        if (!_fileWrapper.Exists(_connectionFileName))
        {
            _logWrapper.Error($"Connection String file not found: {_connectionFileName}");

            return string.Empty;
        }

        Connection? connection = _connectionConfigurationBuilder.BuildConnection(_connectionFileName);

        if (connection == null || string.IsNullOrWhiteSpace(connection.ConnectionString))
        {
            _logWrapper.Error($"Connection String not found in {_connectionFileName}; Try 'save' argument?");

            return string.Empty;
        }

        return connection.ConnectionString;
    }

    public int SaveConnectionString(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            _logWrapper.Error("Connection String cannot be null or empty.");

            return 1;
        }

        var connection = new Connection
        {
            ConnectionString = connectionString,
        };

        try
        {
            var connectionSerialized = _connectionSerializer.Serialize(connection);

            _fileWrapper.WriteAllText(_connectionFileName, connectionSerialized);

            _logWrapper.Info($"Connection String saved.");
        }
        catch (Exception exception)
        {
            _logWrapper.Error(exception, "Failed to save Connection String.");

            return 1;
        }

        return 0;
    }
}
