namespace TestMiner.Database
{
    using System;
    using System.Data;

    using Dapper;

    using TestMiner.Logger;

    internal class Database : IDatabase
    {
        private const string SpTestRunUpdateTestMinerStatus = "dbo.spTestRuns_UpdateTestMinerStatus";
        private const string SpTestRunAddTestRun = "dbo.spTestRuns_AddTestRun";
        private const string SpTestRunGetIdFromHex = "dbo.spTestRuns_GetIdFromHex";
        private const string SpTestExecutionAddTest = "dbo.spTestExecutions_AddTest";

        private const int TimeoutInSeconds = 30;

        private readonly ILogWrapper _logWrapper;

        private readonly IDbConnection _dbConnection;

        private readonly IDynamicParametersWrapper _dynamicParametersWrapper;

        internal Database(IDbConnection dbConnection)
            : this(new LogWrapper(typeof(Database)), dbConnection, new DynamicParametersWrapper())
        {
        }

        internal Database(ILogWrapper logWrapper, IDbConnection dbConnection, IDynamicParametersWrapper dynamicParametersWrapper)
        {
            _logWrapper = logWrapper ?? throw new ArgumentNullException(nameof(logWrapper));
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _dynamicParametersWrapper = dynamicParametersWrapper ?? throw new ArgumentNullException(nameof(dynamicParametersWrapper));
        }

        public int GetTestRunIdFromHex(string md5Hash)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(md5Hash);

            _dynamicParametersWrapper.Add("@hex", md5Hash, DbType.String);
            DynamicParameters dynamicParameters = _dynamicParametersWrapper.GetDynamicParameters();

            int testRunId;
            try
            {
                testRunId = _dbConnection.ExecuteScalar<int>(SpTestRunGetIdFromHex, dynamicParameters, commandTimeout: TimeoutInSeconds, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logWrapper.Error(ex, "Failed to get TestRunId from Hex.");
                throw;
            }
            finally
            {
                _dynamicParametersWrapper.Clear();
            }

            return testRunId;
        }

        public void AddTestExecution(
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
            string reason)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(testRunId);

            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(className);
            ArgumentException.ThrowIfNullOrWhiteSpace(result);

            ArgumentOutOfRangeException.ThrowIfNegative(seed);

            ArgumentNullException.ThrowIfNull(label);

            ArgumentOutOfRangeException.ThrowIfEqual(startTime, default);
            ArgumentOutOfRangeException.ThrowIfEqual(endTime, default);

            ArgumentOutOfRangeException.ThrowIfNegative(duration);
            ArgumentOutOfRangeException.ThrowIfNegative(asserts);

            ArgumentNullException.ThrowIfNull(failureMessage);
            ArgumentNullException.ThrowIfNull(stackTrace);
            ArgumentNullException.ThrowIfNull(reason);

            _dynamicParametersWrapper.Add("@testRunId", testRunId, DbType.Int32);
            _dynamicParametersWrapper.Add("@name", name, DbType.String);
            _dynamicParametersWrapper.Add("@class", className, DbType.String);
            _dynamicParametersWrapper.Add("@result", result, DbType.String);
            _dynamicParametersWrapper.Add("@seed", seed, DbType.Int64);
            _dynamicParametersWrapper.Add("@label", label.GetNullable(), DbType.String);
            _dynamicParametersWrapper.Add("@startTime", startTime, DbType.DateTime);
            _dynamicParametersWrapper.Add("@endTime", endTime, DbType.DateTime);
            _dynamicParametersWrapper.Add("@duration", duration, DbType.Int64);
            _dynamicParametersWrapper.Add("@assertCount", asserts, DbType.Int32);
            _dynamicParametersWrapper.Add("@failureMessage", label.GetNullable(), DbType.String);
            _dynamicParametersWrapper.Add("@stackTrace", label.GetNullable(), DbType.String);
            _dynamicParametersWrapper.Add("@reason", label.GetNullable(), DbType.String);
            DynamicParameters dynamicParameters = _dynamicParametersWrapper.GetDynamicParameters();

            try
            {
                _ = _dbConnection.Execute(SpTestExecutionAddTest, dynamicParameters, commandTimeout: TimeoutInSeconds, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logWrapper.Error(ex, "Failed to add TestExecution.");
                throw;
            }
            finally
            {
                _dynamicParametersWrapper.Clear();
            }
        }

        public int AddTestRun(
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
            string hex)
        {
            ArgumentOutOfRangeException.ThrowIfEqual(startTime, default);
            ArgumentOutOfRangeException.ThrowIfEqual(endTime, default);
            ArgumentOutOfRangeException.ThrowIfNegative(duration);
            ArgumentOutOfRangeException.ThrowIfNegative(total);
            ArgumentOutOfRangeException.ThrowIfNegative(inconclusive);
            ArgumentOutOfRangeException.ThrowIfNegative(passed);
            ArgumentOutOfRangeException.ThrowIfNegative(warning);
            ArgumentOutOfRangeException.ThrowIfNegative(skipped);
            ArgumentOutOfRangeException.ThrowIfNegative(failed);
            ArgumentOutOfRangeException.ThrowIfNegative(error);
            ArgumentException.ThrowIfNullOrWhiteSpace(user);
            ArgumentException.ThrowIfNullOrWhiteSpace(machine);
            ArgumentException.ThrowIfNullOrWhiteSpace(hex);

            _dynamicParametersWrapper.Add("@startTime", startTime, DbType.DateTime);
            _dynamicParametersWrapper.Add("@endTime", endTime, DbType.DateTime);
            _dynamicParametersWrapper.Add("@duration", duration, DbType.Int64);
            _dynamicParametersWrapper.Add("@total", total, DbType.Int32);
            _dynamicParametersWrapper.Add("@inconclusive", inconclusive, DbType.Int32);
            _dynamicParametersWrapper.Add("@passed", passed, DbType.Int32);
            _dynamicParametersWrapper.Add("@warning", warning, DbType.Int32);
            _dynamicParametersWrapper.Add("@skipped", skipped, DbType.Int32);
            _dynamicParametersWrapper.Add("@failed", failed, DbType.Int32);
            _dynamicParametersWrapper.Add("@error", error, DbType.Int32);
            _dynamicParametersWrapper.Add("@user", user, DbType.String);
            _dynamicParametersWrapper.Add("@machine", machine, DbType.String);
            _dynamicParametersWrapper.Add("@hex", hex, DbType.String);
            DynamicParameters dynamicParameters = _dynamicParametersWrapper.GetDynamicParameters();

            int testRunId;
            try
            {
                testRunId = _dbConnection.Execute(SpTestRunAddTestRun, dynamicParameters, commandTimeout: TimeoutInSeconds, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logWrapper.Error(ex, "Failed to add TestRun.");
                throw;
            }
            finally
            {
                _dynamicParametersWrapper.Clear();
            }

            return testRunId;
        }

        public void UpdateTestRunTestMinerStatus(int testRunId, int testMinerStatusId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(testRunId);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(testMinerStatusId);

            _dynamicParametersWrapper.Add("@testRunId", testRunId, DbType.Int32);
            _dynamicParametersWrapper.Add("@testMinerStatusId", testMinerStatusId, DbType.Byte);
            DynamicParameters dynamicParameters = _dynamicParametersWrapper.GetDynamicParameters();

            try
            {
                _ = _dbConnection.Execute(SpTestRunUpdateTestMinerStatus, dynamicParameters, commandTimeout: TimeoutInSeconds, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logWrapper.Error(ex, $"Failed to update TestMinerStatus for TestRun. TestRunId={testRunId}");
                throw;
            }
            finally
            {
                _dynamicParametersWrapper.Clear();
            }
        }
    }
}