namespace TestMiner.Database.IntegrationTests.StoredProcedures
{
    public class InformationSchemaRoutine
    {
        required public string ROUTINE_CATALOG { get; set; }

        required public string ROUTINE_SCHEMA { get; set; }

        required public string ROUTINE_NAME { get; set; }

        required public string ROUTINE_TYPE { get; set; }
    }
}