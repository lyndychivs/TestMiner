namespace TestMiner.Utility
{
    using System;

    using Microsoft.Data.SqlClient;

    using TestMiner.Logger;

    internal class ConnectionStringValidator
    {
        private readonly ILogWrapper _logWrapper;

        public ConnectionStringValidator()
            : this(new LogWrapper())
        {
        }

        public ConnectionStringValidator(ILogWrapper logWrapper)
        {
            _logWrapper = logWrapper ?? throw new ArgumentNullException(nameof(logWrapper));
        }

        public bool IsConnectionStringValid(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                _logWrapper.Error("Connection String is empty or null.");
                return false;
            }

            try
            {
                _ = new SqlConnection(connectionString);
                return true;
            }
            catch (Exception)
            {
                _logWrapper.Error("Connection String is invalid.");
                return false;
            }
        }
    }
}