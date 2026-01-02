namespace TestMiner.Database.Component.Tests;

public class InformationSchemaTable
{
    required public string TABLE_CATALOG { get; init; }

    required public string TABLE_SCHEMA { get; init; }

    required public string TABLE_NAME { get; init; }

    required public string COLUMN_NAME { get; init; }

    required public string ORDINAL_POSITION { get; init; }

    required public string IS_NULLABLE { get; init; }

    required public string DATA_TYPE { get; init; }

    public string? CHARACTER_MAXIMUM_LENGTH { get; init; }
}
