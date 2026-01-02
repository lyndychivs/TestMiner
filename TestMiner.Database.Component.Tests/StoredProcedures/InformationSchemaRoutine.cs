namespace TestMiner.Database.Component.Tests.StoredProcedures;

public class InformationSchemaRoutine
{
    required public string ROUTINE_CATALOG { get; init; }

    required public string ROUTINE_SCHEMA { get; init; }

    required public string ROUTINE_NAME { get; init; }

    required public string ROUTINE_TYPE { get; init; }
}
