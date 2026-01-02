namespace TestMiner.Database.Component.Tests.Tables;

using System.Collections.Generic;

using TestMiner.Database.Component.Tests;

internal sealed class EnvironmentMachinesInformationSchema
{
    private const string TableCatalog = "TestMiner";

    private const string TableSchema = "dbo";

    private const string TableName = "EnvironmentMachines";

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
                COLUMN_NAME = "Machine",
                ORDINAL_POSITION = "2",
                IS_NULLABLE = "NO",
                DATA_TYPE = "nvarchar",
                CHARACTER_MAXIMUM_LENGTH = "200",
            },
            ];
    }
}
