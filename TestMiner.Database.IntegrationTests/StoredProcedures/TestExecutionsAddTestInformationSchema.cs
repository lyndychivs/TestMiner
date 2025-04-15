namespace TestMiner.Database.IntegrationTests.StoredProcedures
{
    using System.Collections.Generic;

    internal static class TestExecutionsAddTestInformationSchema
    {
        internal static List<InformationSchemaRoutine> Get()
        {
            return
                [
                new ()
                {
                    ROUTINE_CATALOG = "TestMiner",
                    ROUTINE_SCHEMA = "dbo",
                    ROUTINE_NAME = "spTestExecutions_AddTest",
                    ROUTINE_TYPE = "PROCEDURE",
                },
                ];
        }
    }
}