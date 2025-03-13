namespace TestMiner.Database
{
    using System;

    internal interface IDatabase
    {
        int GetTestRunIdFromHex(string md5Hash);

        int AddTestRun(
            DateTime startTime,
            DateTime endTime,
            long duration,
            int total,
            int inconclusive,
            int passed,
            int warning,
            int skipped,
            int failed,
            int error,
            string user,
            string machine,
            string hex);

        void AddTestExecution(
            int testRunId,
            string name,
            string className,
            string result,
            long seed,
            string label,
            DateTime startTime,
            DateTime endTime,
            long duration,
            int asserts,
            string failureMessage,
            string stackTrace,
            string reason);

        void UpdateTestRunTestMinerStatus(int testRunId, int testMinerStatus);
    }
}