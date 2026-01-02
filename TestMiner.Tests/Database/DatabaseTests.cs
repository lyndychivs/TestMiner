namespace TestMiner.Tests.Database;

using System;
using System.Data;

using Dapper;

using Moq;
using Moq.Dapper;

using NUnit.Framework;

using TestMiner.Database;
using TestMiner.Logger;

[TestFixture]
public class DatabaseTests
{
    private const string SpTestRunUpdateTestMinerStatus = "dbo.spTestRuns_UpdateTestMinerStatus";
    private const string SpTestRunAddTestRun = "dbo.spTestRuns_AddTestRun";
    private const string SpTestRunGetIdFromHex = "dbo.spTestRuns_GetIdFromHex";
    private const string SpTestExecutionAddTest = "dbo.spTestExecutions_AddTest";

    private readonly Mock<ILogWrapper> _mockLogWrapper;

    private readonly Mock<IDbConnection> _mockDbConnection;

    private readonly Mock<IDynamicParametersWrapper> _mockDynamicParametersWrapper;

    private readonly Database _database;

    public DatabaseTests()
    {
        _mockLogWrapper = new Mock<ILogWrapper>();
        _mockDbConnection = new Mock<IDbConnection>();

        _mockDynamicParametersWrapper = new Mock<IDynamicParametersWrapper>();
        _mockDynamicParametersWrapper.Setup(wrapper => wrapper.GetDynamicParameters()).Returns(new DynamicParameters());

        _database = new Database(_mockLogWrapper.Object, _mockDbConnection.Object, _mockDynamicParametersWrapper.Object);
    }

    [Test]
    public void GetTestRunIdFromHex_NullMd5Hash_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _database.GetTestRunIdFromHex(null!));

            Assert.That(ex?.ParamName, Is.EqualTo("md5Hash"));
            Assert.That(ex?.Message, Does.Contain("Value cannot be null. (Parameter 'md5Hash')"));
        });
    }

    [TestCase("")]
    [TestCase(" ")]
    public void GetTestRunIdFromHex_InvalidMd5Hash_ThrowsArgumentException(string? invalidMd5Hash)
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentException>(() => _database.GetTestRunIdFromHex(invalidMd5Hash!));

            Assert.That(ex?.ParamName, Is.EqualTo("md5Hash"));
            Assert.That(ex?.Message, Does.Contain("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'md5Hash')"));
        });
    }

    [Test]
    public void GetTestRunIdFromHex_ValidMd5Hash_ReturnsTestRunId()
    {
        _mockDbConnection.SetupDapper(db => db.ExecuteScalar<int>(SpTestRunGetIdFromHex, null, null, null, CommandType.StoredProcedure)).Returns(1);

        var result = _database.GetTestRunIdFromHex("a");

        Assert.That(result, Is.EqualTo(1));
        _mockDbConnection.Verify(db => db.Open(), Times.Once);
        _mockDbConnection.Verify(db => db.Close(), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@hex", "a", DbType.String), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Clear(), Times.Once);
    }

    [Test]
    public void GetTestRunIdFromHex_DatabaseCallThrowsAnException_ThrowsException()
    {
        _mockDbConnection.Setup(db => db.Close()).Throws(new Exception("Database error"));

        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<Exception>(() => _database.GetTestRunIdFromHex("a"));

            Assert.That(ex?.Message, Is.EqualTo("Database error"));

            _mockLogWrapper.Verify(log => log.Error(It.IsAny<Exception>(), "Failed fetching Test Run Id for Hex from the Database. Hex=a"), Times.Once);
            _mockDynamicParametersWrapper.Verify(dp => dp.Clear(), Times.Once);
        });
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void UpdateTestRunTestMinerStatus_InvalidTestRunId_ThrowsArgumentOutOfRangeException(int testRunId)
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _database.UpdateTestRunTestMinerStatus(testRunId, 1));

            Assert.That(ex?.ParamName, Is.EqualTo("testRunId"));
            Assert.That(ex?.Message, Does.Contain($"testRunId ('{testRunId}') must be a non-negative and non-zero value. (Parameter 'testRunId')"));
        });
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void UpdateTestRunTestMinerStatus_InvalidTestMinerStatusId_ThrowsArgumentOutOfRangeException(int testMinerStatusId)
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _database.UpdateTestRunTestMinerStatus(1, testMinerStatusId));

            Assert.That(ex?.ParamName, Is.EqualTo("testMinerStatusId"));
            Assert.That(ex?.Message, Does.Contain($"testMinerStatusId ('{testMinerStatusId}') must be a non-negative and non-zero value. (Parameter 'testMinerStatusId')"));
        });
    }

    [Test]
    public void UpdateTestRunTestMinerStatus_ValidParameters_CallsDatabase()
    {
        _mockDbConnection.SetupDapper(db => db.Execute(SpTestRunUpdateTestMinerStatus, null, null, null, CommandType.StoredProcedure)).Returns(1);

        _database.UpdateTestRunTestMinerStatus(1, 2);

        _mockDbConnection.Verify(db => db.Open(), Times.Once);
        _mockDbConnection.Verify(db => db.Close(), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@testRunId", 1, DbType.Int32), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@testMinerStatusId", 2, DbType.Byte), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Clear(), Times.Once);
    }

    [Test]
    public void UpdateTestRunTestMinerStatus_DatabaseCallThrowsAnException_ThrowsException()
    {
        _mockDbConnection.Setup(db => db.Close()).Throws(new Exception("Database error"));

        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<Exception>(() => _database.UpdateTestRunTestMinerStatus(1, 1));

            Assert.That(ex?.Message, Is.EqualTo("Database error"));

            _mockLogWrapper.Verify(log => log.Error(It.IsAny<Exception>(), "Failed to update Test Miner Status in Database for Test Run. TestRunId=1"), Times.Once);
            _mockDynamicParametersWrapper.Verify(dp => dp.Clear(), Times.Once);
        });
    }

    [Test]
    public void AddTestRun_ValidParameters_CallsDatabase()
    {
        _mockDbConnection.SetupDapper(db => db.ExecuteScalar<int>(SpTestRunAddTestRun, null, null, null, CommandType.StoredProcedure)).Returns(1);

        var result = _database.AddTestRun(DateTime.UtcNow, DateTime.UtcNow, 0, 1, 2, 3, 4, 5, 6, 7, "a", "b", "c");

        Assert.That(result, Is.EqualTo(1));
        _mockDbConnection.Verify(db => db.Open(), Times.Once);
        _mockDbConnection.Verify(db => db.Close(), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@startTime", It.IsAny<DateTime>(), DbType.DateTime), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@endTime", It.IsAny<DateTime>(), DbType.DateTime), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@duration", 0L, DbType.Int64), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@total", 1, DbType.Int32), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@inconclusive", 2, DbType.Int32), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@passed", 3, DbType.Int32), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@warning", 4, DbType.Int32), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@skipped", 5, DbType.Int32), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@failed", 6, DbType.Int32), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@error", 7, DbType.Int32), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@user", "a", DbType.String), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@machine", "b", DbType.String), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@hex", "c", DbType.String), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Clear(), Times.Once);
    }

    [Test]
    public void AddTestRun_DatabaseCallThrowsAnException_ThrowsException()
    {
        _mockDbConnection.Setup(db => db.Close()).Throws(new Exception("Database error"));

        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<Exception>(()
                => _database.AddTestRun(DateTime.UtcNow, DateTime.UtcNow, 0, 0, 0, 0, 0, 0, 0, 0, "a", "b", "c"));

            Assert.That(ex?.Message, Is.EqualTo("Database error"));

            _mockLogWrapper.Verify(log => log.Error(It.IsAny<Exception>(), "Failed to add Test Run into Database."), Times.Once);
            _mockDynamicParametersWrapper.Verify(dp => dp.Clear(), Times.Once);
        });
    }

    [Test]
    public void AddTestRun_MinValueStartTime_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => _database.AddTestRun(DateTime.MinValue, DateTime.UtcNow, 0, 0, 0, 0, 0, 0, 0, 0, "a", "b", "c"));

            Assert.That(ex?.ParamName, Is.EqualTo("startTime"));
            Assert.That(ex?.Message, Does.Contain("startTime ('01/01/0001 00:00:00') must not be equal to '01/01/0001 00:00:00'. (Parameter 'startTime')"));
        });
    }

    [Test]
    public void AddTestRun_MinValueEndTime_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => _database.AddTestRun(DateTime.UtcNow, DateTime.MinValue, 0, 0, 0, 0, 0, 0, 0, 0, "a", "b", "c"));

            Assert.That(ex?.ParamName, Is.EqualTo("endTime"));
            Assert.That(ex?.Message, Does.Contain("endTime ('01/01/0001 00:00:00') must not be equal to '01/01/0001 00:00:00'. (Parameter 'endTime')"));
        });
    }

    [Test]
    public void AddTestRun_NegativeDuration_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => _database.AddTestRun(DateTime.UtcNow, DateTime.UtcNow, -1, 0, 0, 0, 0, 0, 0, 0, "a", "b", "c"));

            Assert.That(ex?.ParamName, Is.EqualTo("duration"));
            Assert.That(ex?.Message, Does.Contain("duration ('-1') must be a non-negative value. (Parameter 'duration')"));
        });
    }

    [Test]
    public void AddTestRun_NegativeTotal_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => _database.AddTestRun(DateTime.UtcNow, DateTime.UtcNow, 0, -1, 0, 0, 0, 0, 0, 0, "a", "b", "c"));

            Assert.That(ex?.ParamName, Is.EqualTo("total"));
            Assert.That(ex?.Message, Does.Contain("total ('-1') must be a non-negative value. (Parameter 'total')"));
        });
    }

    [Test]
    public void AddTestRun_NegativeInconclusive_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => _database.AddTestRun(DateTime.UtcNow, DateTime.UtcNow, 0, 0, -1, 0, 0, 0, 0, 0, "a", "b", "c"));

            Assert.That(ex?.ParamName, Is.EqualTo("inconclusive"));
            Assert.That(ex?.Message, Does.Contain("inconclusive ('-1') must be a non-negative value. (Parameter 'inconclusive')"));
        });
    }

    [Test]
    public void AddTestRun_NegativePassed_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => _database.AddTestRun(DateTime.UtcNow, DateTime.UtcNow, 0, 0, 0, -1, 0, 0, 0, 0, "a", "b", "c"));

            Assert.That(ex?.ParamName, Is.EqualTo("passed"));
            Assert.That(ex?.Message, Does.Contain("passed ('-1') must be a non-negative value. (Parameter 'passed')"));
        });
    }

    [Test]
    public void AddTestRun_NegativeWarning_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => _database.AddTestRun(DateTime.UtcNow, DateTime.UtcNow, 0, 0, 0, 0, -1, 0, 0, 0, "a", "b", "c"));

            Assert.That(ex?.ParamName, Is.EqualTo("warning"));
            Assert.That(ex?.Message, Does.Contain("warning ('-1') must be a non-negative value. (Parameter 'warning')"));
        });
    }

    [Test]
    public void AddTestRun_NegativeSkipped_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => _database.AddTestRun(DateTime.UtcNow, DateTime.UtcNow, 0, 0, 0, 0, 0, -1, 0, 0, "a", "b", "c"));

            Assert.That(ex?.ParamName, Is.EqualTo("skipped"));
            Assert.That(ex?.Message, Does.Contain("skipped ('-1') must be a non-negative value. (Parameter 'skipped')"));
        });
    }

    [Test]
    public void AddTestRun_NegativeFailed_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => _database.AddTestRun(DateTime.UtcNow, DateTime.UtcNow, 0, 0, 0, 0, 0, 0, -1, 0, "a", "b", "c"));

            Assert.That(ex?.ParamName, Is.EqualTo("failed"));
            Assert.That(ex?.Message, Does.Contain("failed ('-1') must be a non-negative value. (Parameter 'failed')"));
        });
    }

    [Test]
    public void AddTestRun_NegativeError_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => _database.AddTestRun(DateTime.UtcNow, DateTime.UtcNow, 0, 0, 0, 0, 0, 0, 0, -1, "a", "b", "c"));

            Assert.That(ex?.ParamName, Is.EqualTo("error"));
            Assert.That(ex?.Message, Does.Contain("error ('-1') must be a non-negative value. (Parameter 'error')"));
        });
    }

    [TestCase("")]
    [TestCase(" ")]
    public void AddTestRun_InvalidUser_ThrowsArgumentException(string? user)
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentException>(()
                => _database.AddTestRun(DateTime.UtcNow, DateTime.UtcNow, 0, 0, 0, 0, 0, 0, 0, 0, user!, "b", "c"));

            Assert.That(ex?.ParamName, Is.EqualTo("user"));
            Assert.That(ex?.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'user')"));
        });
    }

    [Test]
    public void AddTestRun_NullUser_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(()
                => _database.AddTestRun(DateTime.UtcNow, DateTime.UtcNow, 0, 0, 0, 0, 0, 0, 0, 0, null!, "b", "c"));

            Assert.That(ex?.ParamName, Is.EqualTo("user"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'user')"));
        });
    }

    [TestCase("")]
    [TestCase(" ")]
    public void AddTestRun_InvalidMachine_ThrowsArgumentException(string? machine)
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentException>(()
                => _database.AddTestRun(DateTime.UtcNow, DateTime.UtcNow, 0, 0, 0, 0, 0, 0, 0, 0, "a", machine!, "c"));

            Assert.That(ex?.ParamName, Is.EqualTo("machine"));
            Assert.That(ex?.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'machine')"));
        });
    }

    [Test]
    public void AddTestRun_InvalidMachine_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(()
                => _database.AddTestRun(DateTime.UtcNow, DateTime.UtcNow, 0, 0, 0, 0, 0, 0, 0, 0, "a", null!, "c"));

            Assert.That(ex?.ParamName, Is.EqualTo("machine"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'machine')"));
        });
    }

    [TestCase("")]
    [TestCase(" ")]
    public void AddTestRun_InvalidHex_ThrowsArgumentException(string? hex)
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentException>(()
                => _database.AddTestRun(DateTime.UtcNow, DateTime.UtcNow, 0, 0, 0, 0, 0, 0, 0, 0, "a", "b", hex!));

            Assert.That(ex?.ParamName, Is.EqualTo("hex"));
            Assert.That(ex?.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'hex')"));
        });
    }

    [Test]
    public void AddTestRun_NullHex_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(()
                => _database.AddTestRun(DateTime.UtcNow, DateTime.UtcNow, 0, 0, 0, 0, 0, 0, 0, 0, "a", "b", null!));

            Assert.That(ex?.ParamName, Is.EqualTo("hex"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'hex')"));
        });
    }

    [Test]
    public void AddTestExecution_ValidParameters_CallsDatabase()
    {
        _mockDbConnection.SetupDapper(db => db.Execute(SpTestExecutionAddTest, null, null, null, CommandType.StoredProcedure)).Returns(1);

        _database.AddTestExecution(1, "a", "b", "c", 2, "d", DateTime.UtcNow, DateTime.UtcNow, 3, 4, "e", "f", "g");

        _mockDbConnection.Verify(db => db.Open(), Times.Once);
        _mockDbConnection.Verify(db => db.Close(), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@testRunId", 1, DbType.Int32), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@name", "a", DbType.String), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@class", "b", DbType.String), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@result", "c", DbType.String), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@seed", 2L, DbType.Int64), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@label", "d", DbType.String), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@startTime", It.IsAny<DateTime>(), DbType.DateTime), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@endTime", It.IsAny<DateTime>(), DbType.DateTime), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@duration", 3L, DbType.Int64), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@assertCount", 4, DbType.Int32), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@failureMessage", "e", DbType.String), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@stackTrace", "f", DbType.String), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Add("@reason", "g", DbType.String), Times.Once);
        _mockDynamicParametersWrapper.Verify(dp => dp.Clear(), Times.Once);
    }

    [Test]
    public void AddTestExecution_DatabaseCallThrowsAnException_ThrowsException()
    {
        _mockDbConnection.Setup(db => db.Close()).Throws(new Exception("Database error"));

        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<Exception>(()
                => _database.AddTestExecution(1, "a", "b", "c", 0, "d", DateTime.UtcNow, DateTime.UtcNow, 0, 0, "e", "f", "g"));

            Assert.That(ex?.Message, Is.EqualTo("Database error"));
            _mockLogWrapper.Verify(log => log.Error(It.IsAny<Exception>(), "Failed to add Test Execution into Database."), Times.Once);
            _mockDynamicParametersWrapper.Verify(dp => dp.Clear(), Times.Once);
        });
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void AddTestExecution_InvalidTestRunId_ThrowsArgumentOutOfRangeException(int testRunId)
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => _database.AddTestExecution(testRunId, "a", "b", "c", 0, "d", DateTime.UtcNow, DateTime.UtcNow, 0, 0, "e", "f", "g"));

            Assert.That(ex?.ParamName, Is.EqualTo("testRunId"));
            Assert.That(ex?.Message, Does.Contain($"testRunId ('{testRunId}') must be a non-negative and non-zero value. (Parameter 'testRunId')"));
        });
    }

    [TestCase("")]
    [TestCase(" ")]
    public void AddTestExecution_InvalidName_ThrowsArgumentException(string? invalidName)
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentException>(()
                => _database.AddTestExecution(1, invalidName!, "b", "c", 0, "d", DateTime.UtcNow, DateTime.UtcNow, 0, 0, "e", "f", "g"));

            Assert.That(ex?.ParamName, Is.EqualTo("name"));
            Assert.That(ex?.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'name')"));
        });
    }

    [Test]
    public void AddTestExecution_NullName_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(()
                => _database.AddTestExecution(1, null!, "b", "c", 0, "d", DateTime.UtcNow, DateTime.UtcNow, 0, 0, "e", "f", "g"));

            Assert.That(ex?.ParamName, Is.EqualTo("name"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'name')"));
        });
    }

    [TestCase("")]
    [TestCase(" ")]
    public void AddTestExecution_InvalidClassName_ThrowsArgumentException(string? invalidClassName)
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentException>(()
                => _database.AddTestExecution(1, "a", invalidClassName!, "c", 0, "d", DateTime.UtcNow, DateTime.UtcNow, 0, 0, "e", "f", "g"));

            Assert.That(ex?.ParamName, Is.EqualTo("className"));
            Assert.That(ex?.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'className')"));
        });
    }

    [Test]
    public void AddTestExecution_NullClassName_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(()
                => _database.AddTestExecution(1, "a", null!, "c", 0, "d", DateTime.UtcNow, DateTime.UtcNow, 0, 0, "e", "f", "g"));

            Assert.That(ex?.ParamName, Is.EqualTo("className"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'className')"));
        });
    }

    [TestCase("")]
    [TestCase(" ")]
    public void AddTestExecution_InvalidResult_ThrowsArgumentException(string? invalidResult)
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentException>(()
                => _database.AddTestExecution(1, "a", "b", invalidResult!, 0, "d", DateTime.UtcNow, DateTime.UtcNow, 0, 0, "e", "f", "g"));

            Assert.That(ex?.ParamName, Is.EqualTo("result"));
            Assert.That(ex?.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'result')"));
        });
    }

    [Test]
    public void AddTestExecution_NullResult_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(()
                => _database.AddTestExecution(1, "a", "b", null!, 0, "d", DateTime.UtcNow, DateTime.UtcNow, 0, 0, "e", "f", "g"));

            Assert.That(ex?.ParamName, Is.EqualTo("result"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'result')"));
        });
    }

    [TestCase]
    public void AddTestExecution_NegativeSeed_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => _database.AddTestExecution(1, "a", "b", "c", -1, "d", DateTime.UtcNow, DateTime.UtcNow, 0, 0, "e", "f", "g"));

            Assert.That(ex?.ParamName, Is.EqualTo("seed"));
            Assert.That(ex?.Message, Does.Contain("seed ('-1') must be a non-negative value. (Parameter 'seed')"));
        });
    }

    [Test]
    public void AddTestExecution_NullLabel_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(()
                => _database.AddTestExecution(1, "a", "b", "c", 0, null!, DateTime.UtcNow, DateTime.UtcNow, 0, 0, "e", "f", "g"));

            Assert.That(ex?.ParamName, Is.EqualTo("label"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'label')"));
        });
    }

    [Test]
    public void AddTestExecution_MinStartTime_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => _database.AddTestExecution(1, "a", "b", "c", 0, "d", DateTime.MinValue, DateTime.UtcNow, 0, 0, "e", "f", "g"));

            Assert.That(ex?.ParamName, Is.EqualTo("startTime"));
            Assert.That(ex?.Message, Does.Contain("startTime ('01/01/0001 00:00:00') must not be equal to '01/01/0001 00:00:00'. (Parameter 'startTime')"));
        });
    }

    [Test]
    public void AddTestExecution_MinEndTime_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => _database.AddTestExecution(1, "a", "b", "c", 0, "d", DateTime.UtcNow, DateTime.MinValue, 0, 0, "e", "f", "g"));

            Assert.That(ex?.ParamName, Is.EqualTo("endTime"));
            Assert.That(ex?.Message, Does.Contain("endTime ('01/01/0001 00:00:00') must not be equal to '01/01/0001 00:00:00'. (Parameter 'endTime')"));
        });
    }

    [Test]
    public void AddTestExecution_NegativeDuration_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => _database.AddTestExecution(1, "a", "b", "c", 0, "d", DateTime.UtcNow, DateTime.UtcNow, -1, 0, "e", "f", "g"));

            Assert.That(ex?.ParamName, Is.EqualTo("duration"));
            Assert.That(ex?.Message, Does.Contain("duration ('-1') must be a non-negative value. (Parameter 'duration')"));
        });
    }

    [Test]
    public void AddTestExecution_NegativeAsserts_ThrowsArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => _database.AddTestExecution(1, "a", "b", "c", 0, "d", DateTime.UtcNow, DateTime.UtcNow, 0, -1, "e", "f", "g"));

            Assert.That(ex?.ParamName, Is.EqualTo("asserts"));
            Assert.That(ex?.Message, Does.Contain("asserts ('-1') must be a non-negative value. (Parameter 'asserts')"));
        });
    }

    [Test]
    public void AddTestExecution_NullFailureMessage_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(()
                => _database.AddTestExecution(1, "a", "b", "c", 0, "d", DateTime.UtcNow, DateTime.UtcNow, 0, 0, null!, "f", "g"));

            Assert.That(ex?.ParamName, Is.EqualTo("failureMessage"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'failureMessage')"));
        });
    }

    [Test]
    public void AddTestExecution_NullStackTrace_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(()
                => _database.AddTestExecution(1, "a", "b", "c", 0, "d", DateTime.UtcNow, DateTime.UtcNow, 0, 0, "e", null!, "g"));

            Assert.That(ex?.ParamName, Is.EqualTo("stackTrace"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'stackTrace')"));
        });
    }

    [Test]
    public void AddTestExecution_NullReason_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(()
                => _database.AddTestExecution(1, "a", "b", "c", 0, "d", DateTime.UtcNow, DateTime.UtcNow, 0, 0, "e", "f", null!));

            Assert.That(ex?.ParamName, Is.EqualTo("reason"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'reason')"));
        });
    }
}
