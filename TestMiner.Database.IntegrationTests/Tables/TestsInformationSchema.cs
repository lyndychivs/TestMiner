namespace TestMiner.Database.IntegrationTests.Tables
{
    using System.Collections.Generic;

    internal static class TestsInformationSchema
    {
        private const string TableCatalog = "TestMiner";

        private const string TableSchema = "dbo";

        private const string TableName = "Tests";

        internal static List<InformationSchemaTable> Get()
        {
            return
                [
                new ()
                {
                    TABLE_CATALOG = TableCatalog,
                    TABLE_SCHEMA = TableSchema,
                    TABLE_NAME = TableName,
                    COLUMN_NAME = "Id",
                    ORDINAL_POSITION = "1",
                    IS_NULLABLE = "NO",
                    DATA_TYPE = "int",
                    CHARACTER_MAXIMUM_LENGTH = null,
                },
                new ()
                {
                    TABLE_CATALOG = TableCatalog,
                    TABLE_SCHEMA = TableSchema,
                    TABLE_NAME = TableName,
                    COLUMN_NAME = "TestClassId",
                    ORDINAL_POSITION = "2",
                    IS_NULLABLE = "NO",
                    DATA_TYPE = "int",
                    CHARACTER_MAXIMUM_LENGTH = null,
                },
                new ()
                {
                    TABLE_CATALOG = TableCatalog,
                    TABLE_SCHEMA = TableSchema,
                    TABLE_NAME = TableName,
                    COLUMN_NAME = "Name",
                    ORDINAL_POSITION = "3",
                    IS_NULLABLE = "NO",
                    DATA_TYPE = "nvarchar",
                    CHARACTER_MAXIMUM_LENGTH = "500",
                },
                ];
        }
    }
}