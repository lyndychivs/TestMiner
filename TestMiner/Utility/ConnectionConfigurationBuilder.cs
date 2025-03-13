namespace TestMiner.Utility
{
    using System;

    using Microsoft.Extensions.Configuration;

    internal class ConnectionConfigurationBuilder : IConnectionConfigurationBuilder
    {
        private readonly IConfigurationBuilder _configurationBuilder;

        public ConnectionConfigurationBuilder()
            : this(new ConfigurationBuilder())
        {
        }

        public ConnectionConfigurationBuilder(IConfigurationBuilder configurationBuilder)
        {
            _configurationBuilder = configurationBuilder ?? throw new ArgumentNullException(nameof(configurationBuilder));
        }

        public Connection? BuildConnection(string filePath)
        {
            return _configurationBuilder
                .AddJsonFile(filePath)
                .Build()
                .Get<Connection>();
        }
    }
}