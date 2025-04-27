namespace TestMiner.DataAccessLayer
{
    using System;

    using Microsoft.Data.SqlClient;

    using TestMiner.Database;
    using TestMiner.Logger;
    using TestMiner.Models.TestRun;

    internal class TestMinerDal : ITestMinerDal
    {
        private readonly ILogWrapper _logWrapper;

        private readonly IDatabase _database;

        internal TestMinerDal(string connectionString)
            : this(new LogWrapper(), new Database(new SqlConnection(connectionString)))
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
        }

        internal TestMinerDal(ILogWrapper logWrapper, IDatabase database)
        {
            _logWrapper = logWrapper ?? throw new ArgumentNullException(nameof(logWrapper));
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public bool IsTestRunPreviouslyRecorded(string md5Hash)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(md5Hash);

            return _database.GetTestRunIdFromHex(md5Hash) > 0;
        }

        public void RecordTestRun(ITestRunDto testRunDto)
        {
            ArgumentNullException.ThrowIfNull(testRunDto);

            _logWrapper.Info($"Recording Test Run:\n{testRunDto}");

            int testRunId;
            try
            {
                testRunId = _database.AddTestRun(
                    testRunDto.StartTime,
                    testRunDto.EndTime,
                    testRunDto.Duration.Ticks,
                    testRunDto.Total,
                    testRunDto.Inconclusive,
                    testRunDto.Passed,
                    testRunDto.Warning,
                    testRunDto.Skipped,
                    testRunDto.Failed,
                    testRunDto.Error,
                    testRunDto.Environment.User,
                    testRunDto.Environment.MachineName,
                    testRunDto.CalculateMd5Hash());
            }
            catch (Exception exception)
            {
                _logWrapper.Error(exception, $"Failed to add Test Run into Database.");
                return;
            }

            if (testRunId < 1)
            {
                _logWrapper.Warning($"Test Run Id cannot be less than 1.");
                return;
            }

            foreach (ITestDto testDto in testRunDto.Tests)
            {
                _logWrapper.Info($"Recording Test:\n{testDto}");

                try
                {
                    _database.AddTestExecution(
                        testRunId,
                        testDto.Name,
                        testDto.ClassName,
                        testDto.Result.ToString(),
                        testDto.Seed,
                        testDto.Label,
                        testDto.StartTime,
                        testDto.EndTime,
                        testDto.Duration.Ticks,
                        testDto.Asserts,
                        testDto.FailureMessage,
                        testDto.StackTrace,
                        testDto.Reason);
                }
                catch (Exception exception)
                {
                    _logWrapper.Error(exception, $"Failed to add Test Execution into Database. TestRunId={testRunId}");

                    _database.UpdateTestRunTestMinerStatus(testRunId, (int)TestMinerStatus.Failed);

                    return;
                }
            }

            _database.UpdateTestRunTestMinerStatus(testRunId, (int)TestMinerStatus.Complete);
        }
    }
}