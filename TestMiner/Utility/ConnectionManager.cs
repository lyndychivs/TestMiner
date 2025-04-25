namespace TestMiner.Utility
{
    using System;
    using System.IO;

    using TestMiner.Logger;

    internal class ConnectionManager
    {
        private readonly ILogWrapper _logWrapper;

        private readonly IConnectionConfigurationBuilder _connectionConfigurationBuilder;

        private readonly IFileWrapper _fileWrapper;

        private readonly IConnectionSerializer _connectionSerializer;

        private readonly string _connectionFileName;

        public ConnectionManager()
            : this(
                  new LogWrapper(typeof(ConnectionManager)),
                  new ConnectionConfigurationBuilder(),
                  new FileWrapper(),
                  new ConnectionSerializer())
        {
        }

        public ConnectionManager(
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

            _logWrapper.Info($"Connection String not provided as parameter. Getting Connection String from {_connectionFileName}");

            if (!_fileWrapper.Exists(_connectionFileName))
            {
                var fileNotFoundException = new FileNotFoundException(nameof(_connectionFileName), _connectionFileName);

                _logWrapper.Error(fileNotFoundException, $"Connection String file not found: {_connectionFileName}");

                throw fileNotFoundException;
            }

            Connection? connection = _connectionConfigurationBuilder.BuildConnection(_connectionFileName);

            if (connection == null)
            {
                var nullReferenceException = new NullReferenceException($"{nameof(connection)} cannot be null.");

                _logWrapper.Error(nullReferenceException, $"Connection String not found in {_connectionFileName} or provided via Commandline arguments.");

                throw nullReferenceException;
            }

            if (string.IsNullOrWhiteSpace(connection.ConnectionString))
            {
                var nullReferenceException = new NullReferenceException($"{nameof(connection.ConnectionString)} cannot be null.");

                _logWrapper.Error(nullReferenceException, $"Connection String not found in {_connectionFileName} or provided via Commandline arguments.");

                throw nullReferenceException;
            }

            return connection.ConnectionString;
        }

        public int SaveConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                _logWrapper.Error(new ArgumentNullException(nameof(connectionString)), "Connection String cannot be null or empty.");

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
                _logWrapper.Error(exception, "Failed to Save Connection String.");

                return 1;
            }

            return 0;
        }
    }
}