namespace TestMiner.Database.ComponentTests.StoredProcedures
{
    using System.Collections.Generic;

    internal static class TestRunsGetIdFromHexInformationSchema
    {
        internal static List<InformationSchemaRoutine> Get()
        {
            return
                [
                new ()
                {
                    ROUTINE_CATALOG = "TestMiner",
                    ROUTINE_SCHEMA = "dbo",
                    ROUTINE_NAME = "spTestRuns_GetIdFromHex",
                    ROUTINE_TYPE = "PROCEDURE",
                },
                ];
        }
    }
}