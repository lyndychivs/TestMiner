namespace TestMiner.Database.ComponentTests.Tables;

using System.Collections.Generic;

internal static class TestExecutionsInformationSchema
{
    private const string TableCatalog = "TestMiner";

    private const string TableSchema = "dbo";

    private const string TableName = "TestExecutions";

    public static List<InformationSchemaTable> Get()
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
                COLUMN_NAME = "TestId",
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
                COLUMN_NAME = "TestResultId",
                ORDINAL_POSITION = "3",
                IS_NULLABLE = "NO",
                DATA_TYPE = "tinyint",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "TestRunId",
                ORDINAL_POSITION = "4",
                IS_NULLABLE = "NO",
                DATA_TYPE = "int",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "StartTime",
                ORDINAL_POSITION = "5",
                IS_NULLABLE = "NO",
                DATA_TYPE = "datetime",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "EndTime",
                ORDINAL_POSITION = "6",
                IS_NULLABLE = "NO",
                DATA_TYPE = "datetime",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "Duration",
                ORDINAL_POSITION = "7",
                IS_NULLABLE = "NO",
                DATA_TYPE = "bigint",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "Seed",
                ORDINAL_POSITION = "8",
                IS_NULLABLE = "NO",
                DATA_TYPE = "bigint",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "Label",
                ORDINAL_POSITION = "9",
                IS_NULLABLE = "YES",
                DATA_TYPE = "nvarchar",
                CHARACTER_MAXIMUM_LENGTH = "500",
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "AssertCount",
                ORDINAL_POSITION = "10",
                IS_NULLABLE = "NO",
                DATA_TYPE = "int",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "Reason",
                ORDINAL_POSITION = "11",
                IS_NULLABLE = "YES",
                DATA_TYPE = "nvarchar",
                CHARACTER_MAXIMUM_LENGTH = "500",
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "FailureMessage",
                ORDINAL_POSITION = "12",
                IS_NULLABLE = "YES",
                DATA_TYPE = "nvarchar",
                CHARACTER_MAXIMUM_LENGTH = "500",
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "StackTrace",
                ORDINAL_POSITION = "13",
                IS_NULLABLE = "YES",
                DATA_TYPE = "nvarchar",
                CHARACTER_MAXIMUM_LENGTH = "2000",
            },
            ];
    }
}
