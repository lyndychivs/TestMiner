namespace TestMiner.Database.Component.Tests.Tables.Models;

using System.Collections.Generic;

using TestMiner.Database.Component.Tests.Models;

internal static class TestResultsInformationSchema
{
    private const string TableCatalog = "TestMiner";

    private const string TableSchema = "dbo";

    private const string TableName = "TestResults";

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
                DATA_TYPE = "tinyint",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "Result",
                ORDINAL_POSITION = "2",
                IS_NULLABLE = "NO",
                DATA_TYPE = "varchar",
                CHARACTER_MAXIMUM_LENGTH = "12",
            },
        ];
    }
}
