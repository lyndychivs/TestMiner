namespace TestMiner.Database.IntegrationTests.StoredProcedures
{
    using System.Collections.Generic;

    internal static class TestRunsUpdateTestMinerStatusInformationSchema
    {
        internal static List<InformationSchemaRoutine> Get()
        {
            return
                [
                new ()
                {
                    ROUTINE_CATALOG = "TestMiner",
                    ROUTINE_SCHEMA = "dbo",
                    ROUTINE_NAME = "spTestRuns_UpdateTestMinerStatus",
                    ROUTINE_TYPE = "PROCEDURE",
                },
                ];
        }
    }
}