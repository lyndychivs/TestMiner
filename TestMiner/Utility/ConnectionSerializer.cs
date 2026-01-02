namespace TestMiner.Utility;

using System;
using System.Text.Json;

internal class ConnectionSerializer : IConnectionSerializer
{
    public string Serialize(Connection connection)
    {
        ArgumentNullException.ThrowIfNull(connection);

        return JsonSerializer.Serialize(connection);
    }
}
