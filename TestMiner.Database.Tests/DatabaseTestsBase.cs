namespace TestMiner.Database.Tests
{
    using Microsoft.Data.SqlClient;

    using NUnit.Framework;

    using TestMiner.Database;

    [TestFixture]
    [Explicit("Integration Tests require a live Database Connection.")]
    public class DatabaseTestsBase
    {
        protected readonly Database Database;

        private const string ConnectionString = "x";

        protected DatabaseTestsBase()
        {
            Database = new Database(new SqlConnection(ConnectionString));
        }
    }
}