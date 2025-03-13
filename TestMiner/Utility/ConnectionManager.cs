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

        public ConnectionManager()
            : this(new LogWrapper(typeof(ConnectionManager)), new ConnectionConfigurationBuilder(), new FileWrapper())
        {
        }

        public ConnectionManager(ILogWrapper logWrapper, IConnectionConfigurationBuilder connectionConfigurationBuilder, IFileWrapper fileWrapper)
        {
            _logWrapper = logWrapper ?? throw new ArgumentNullException(nameof(logWrapper));
            _connectionConfigurationBuilder = connectionConfigurationBuilder ?? throw new ArgumentNullException(nameof(connectionConfigurationBuilder));
            _fileWrapper = fileWrapper ?? throw new ArgumentNullException(nameof(fileWrapper));
        }

        public string GetConnectionString(string connectionString)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                _logWrapper.Info($"Connection String provided as parameter.");

                return connectionString;
            }

            string connectionFile = $"{nameof(Connection)}.json";

            _logWrapper.Info($"Connection String not provided as parameter. Getting Connection String from {connectionFile}");

            if (!_fileWrapper.Exists(connectionFile))
            {
                var fileNotFoundException = new FileNotFoundException(nameof(connectionFile), connectionFile);

                _logWrapper.Error(fileNotFoundException, $"Connection String file not found: {connectionFile}");

                throw fileNotFoundException;
            }

            Connection? connection = _connectionConfigurationBuilder.BuildConnection(connectionFile);

            if (connection == null)
            {
                var nullReferenceException = new NullReferenceException($"{nameof(connection)} cannot be null.");

                _logWrapper.Error(nullReferenceException, $"Connection String not found in {connectionFile} or provided via Commandline arguments.");

                throw nullReferenceException;
            }

            if (string.IsNullOrWhiteSpace(connection.ConnectionString))
            {
                var nullReferenceException = new NullReferenceException($"{nameof(connection.ConnectionString)} cannot be null.");

                _logWrapper.Error(nullReferenceException, $"Connection String not found in {connectionFile} or provided via Commandline arguments.");

                throw nullReferenceException;
            }

            return connection.ConnectionString;
        }
    }
}