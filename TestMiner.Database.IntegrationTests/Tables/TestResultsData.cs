namespace TestMiner.Database.IntegrationTests.Tables
{
    using System.Collections.Generic;

    internal static class TestResultsData
    {
        internal static List<TestResult> Get()
        {
            return
                [
                new ()
                {
                    Id = 1,
                    Result = "Inconclusive",
                },
                new ()
                {
                    Id = 2,
                    Result = "Passed",
                },
                new ()
                {
                    Id = 3,
                    Result = "Warning",
                },
                new ()
                {
                    Id = 4,
                    Result = "Skipped",
                },
                new ()
                {
                    Id = 5,
                    Result = "Failed",
                },
                new ()
                {
                    Id = 6,
                    Result = "Error",
                },
                ];
        }
    }
}