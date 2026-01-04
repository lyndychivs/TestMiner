namespace TestMiner.Database.Component.Tests.Tables.Models;

using System.Collections.Generic;

using TestMiner.Database.Component.Tests.Models;

internal static class TestRunsInformationSchema
{
    private const string TableCatalog = "TestMiner";

    private const string TableSchema = "dbo";

    private const string TableName = "TestRuns";

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
                COLUMN_NAME = "StartTime",
                ORDINAL_POSITION = "2",
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
                ORDINAL_POSITION = "3",
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
                ORDINAL_POSITION = "4",
                IS_NULLABLE = "NO",
                DATA_TYPE = "bigint",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "Total",
                ORDINAL_POSITION = "5",
                IS_NULLABLE = "NO",
                DATA_TYPE = "int",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "Inconclusive",
                ORDINAL_POSITION = "6",
                IS_NULLABLE = "NO",
                DATA_TYPE = "int",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "Passed",
                ORDINAL_POSITION = "7",
                IS_NULLABLE = "NO",
                DATA_TYPE = "int",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "Warning",
                ORDINAL_POSITION = "8",
                IS_NULLABLE = "NO",
                DATA_TYPE = "int",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "Skipped",
                ORDINAL_POSITION = "9",
                IS_NULLABLE = "NO",
                DATA_TYPE = "int",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "Failed",
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
                COLUMN_NAME = "Error",
                ORDINAL_POSITION = "11",
                IS_NULLABLE = "NO",
                DATA_TYPE = "int",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "UserId",
                ORDINAL_POSITION = "12",
                IS_NULLABLE = "NO",
                DATA_TYPE = "int",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "MachineId",
                ORDINAL_POSITION = "13",
                IS_NULLABLE = "NO",
                DATA_TYPE = "int",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "Hex",
                ORDINAL_POSITION = "14",
                IS_NULLABLE = "NO",
                DATA_TYPE = "nvarchar",
                CHARACTER_MAXIMUM_LENGTH = "32",
            },
            new ()
            {
                TABLE_CATALOG = TableCatalog,
                TABLE_SCHEMA = TableSchema,
                TABLE_NAME = TableName,
                COLUMN_NAME = "TestMinerStatusId",
                ORDINAL_POSITION = "15",
                IS_NULLABLE = "NO",
                DATA_TYPE = "tinyint",
                CHARACTER_MAXIMUM_LENGTH = null,
            },
        ];
    }
}
