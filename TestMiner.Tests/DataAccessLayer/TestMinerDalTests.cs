namespace TestMiner.Tests.DataAccessLayer;

using System;

using Moq;

using NUnit.Framework;

using TestMiner.DataAccessLayer;
using TestMiner.Database;
using TestMiner.Logger;
using TestMiner.Models.TestRun;

[TestFixture]
public class TestMinerDalTests
{
    private readonly Mock<IDatabase> _mockDatabase;

    private readonly Mock<ILogWrapper> _mockLogWrapper;

    private readonly Mock<ITestRunDto> _mockTestRunDto;

    private readonly Mock<ITestDto> _mockTestDto;

    private readonly TestMinerDal _testMinerDal;

    public TestMinerDalTests()
    {
        _mockDatabase = new Mock<IDatabase>();
        _mockLogWrapper = new Mock<ILogWrapper>();

        _mockTestRunDto = new Mock<ITestRunDto>();
        var mockEnvironmentDto = new Mock<IEnvironmentDto>();
        _mockTestRunDto.Setup(tr => tr.Environment).Returns(mockEnvironmentDto.Object);

        _mockTestDto = new Mock<ITestDto>();

        _testMinerDal = new TestMinerDal(_mockLogWrapper.Object, _mockDatabase.Object);
    }

    [TestCase("")]
    [TestCase(" ")]
    public void IsTestRunPreviouslyRecorded_InvalidMd5Hash_ThrowsArgumentException(string? md5Hash)
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentException>(() => _testMinerDal.IsTestRunPreviouslyRecorded(md5Hash!));

            Assert.That(ex?.ParamName, Is.EqualTo("md5Hash"));
            Assert.That(ex?.Message, Is.EqualTo("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'md5Hash')"));
        });
    }

    [Test]
    public void IsTestRunPreviouslyRecorded_NullMd5Hash_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _testMinerDal.IsTestRunPreviouslyRecorded(null!));

            Assert.That(ex?.ParamName, Is.EqualTo("md5Hash"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'md5Hash')"));
        });
    }

    [Test]
    public void IsTestRunPreviouslyRecorded_ValidMd5HashFoundInDatabase_ReturnsTrue()
    {
        string md5Hash = "a";
        _mockDatabase.Setup(db => db.GetTestRunIdFromHex(md5Hash)).Returns(1);

        bool result = _testMinerDal.IsTestRunPreviouslyRecorded(md5Hash);

        Assert.That(result, Is.True);
    }

    [Test]
    public void IsTestRunPreviouslyRecorded_ValidMd5HashNotFoundInDatabase_ReturnsFalse()
    {
        string md5Hash = "a";
        _mockDatabase.Setup(db => db.GetTestRunIdFromHex(md5Hash)).Returns(0);

        bool result = _testMinerDal.IsTestRunPreviouslyRecorded(md5Hash);

        Assert.That(result, Is.False);
    }

    [Test]
    public void RecordTestRun_NullTestRunDto_ThrowsArgumentNullException()
    {
        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _testMinerDal.RecordTestRun(null!));

            Assert.That(ex?.ParamName, Is.EqualTo("testRunDto"));
            Assert.That(ex?.Message, Is.EqualTo("Value cannot be null. (Parameter 'testRunDto')"));
        });
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void RecordTestRun_TestRunIdIsLessThanOne_LogsWarning(int testRunId)
    {
        // Arrange
        _mockDatabase.Setup(db => db.AddTestRun(
            It.IsAny<DateTime>(),
            It.IsAny<DateTime>(),
            It.IsAny<long>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()))
            .Returns(testRunId);

        // Act
        _testMinerDal.RecordTestRun(_mockTestRunDto.Object);

        // Assert
        _mockLogWrapper.Verify(log => log.Warning("Test Run Id cannot be less than 1."), Times.Once);
    }

    [Test]
    public void RecordTestRun_NoTestsInTestRunDto_RecordsTestRun()
    {
        // Arrange
        _mockDatabase.Setup(db => db.AddTestRun(
            It.IsAny<DateTime>(),
            It.IsAny<DateTime>(),
            It.IsAny<long>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()))
            .Returns(1);

        _mockTestRunDto.Setup(tr => tr.Tests).Returns(Array.Empty<ITestDto>());

        // Act
        _testMinerDal.RecordTestRun(_mockTestRunDto.Object);

        // Assert
        _mockDatabase.Verify(db => db.UpdateTestRunTestMinerStatus(1, 2), Times.Once);
    }

    [Test]
    public void RecordTestRun_ValidTestsInTestRunDto_RecordsTestRun()
    {
        // Arrange
        _mockTestRunDto.Setup(tr => tr.Tests).Returns([_mockTestDto.Object]);

        _mockDatabase.Setup(db => db.AddTestRun(
            It.IsAny<DateTime>(),
            It.IsAny<DateTime>(),
            It.IsAny<long>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()))
            .Returns(1);

        // Act
        _testMinerDal.RecordTestRun(_mockTestRunDto.Object);

        // Assert
        _mockLogWrapper.Verify(
            log =>
            log.Info(It.Is<string>(m =>
            m.Contains("Recording Test:"))),
            Times.Once);

        _mockDatabase.Verify(
            db =>
            db.AddTestExecution(
                1,
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<long>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<long>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);

        _mockDatabase.Verify(db => db.UpdateTestRunTestMinerStatus(1, 2), Times.Once);
    }

    [Test]
    public void RecordTestRun_MoreThanOneTestInTestRunDto_RecordsTestRun()
    {
        // Arrange
        _mockTestRunDto.Setup(tr => tr.Tests).Returns([_mockTestDto.Object, _mockTestDto.Object]);

        _mockDatabase.Setup(db => db.AddTestRun(
            It.IsAny<DateTime>(),
            It.IsAny<DateTime>(),
            It.IsAny<long>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()))
            .Returns(1);

        // Act
        _testMinerDal.RecordTestRun(_mockTestRunDto.Object);

        // Assert
        _mockLogWrapper.Verify(
            log =>
            log.Info(It.Is<string>(m =>
            m.Contains("Recording Test:"))),
            Times.Exactly(2));

        _mockDatabase.Verify(
            db =>
            db.AddTestExecution(
                1,
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<long>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<long>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Exactly(2));

        _mockDatabase.Verify(db => db.UpdateTestRunTestMinerStatus(1, 2), Times.Once);
    }

    [Test]
    public void RecordTestRun_AddTestRunThrowsAnException_ReturnsEarly()
    {
        // Arrange
        _mockDatabase.Setup(db => db.AddTestRun(
            It.IsAny<DateTime>(),
            It.IsAny<DateTime>(),
            It.IsAny<long>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()))
            .Throws(new Exception());

        // Act
        _testMinerDal.RecordTestRun(_mockTestRunDto.Object);

        // Assert
        _mockLogWrapper.Verify(log => log.Error(It.IsAny<Exception>(), "Failed to add Test Run into Database."), Times.Once);
    }

    [Test]
    public void RecordTestRun_AddTestExecutionThrowsAnException_RecordsTestRunAsFailed()
    {
        // Arrange
        _mockTestRunDto.Setup(tr => tr.Tests).Returns([_mockTestDto.Object]);

        _mockDatabase.Setup(db => db.AddTestRun(
            It.IsAny<DateTime>(),
            It.IsAny<DateTime>(),
            It.IsAny<long>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()))
            .Returns(1);

        _mockDatabase.Setup(db => db.AddTestExecution(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<long>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<long>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
            .Throws(new Exception());

        // Act
        _testMinerDal.RecordTestRun(_mockTestRunDto.Object);

        // Assert
        _mockDatabase.Verify(db => db.UpdateTestRunTestMinerStatus(1, 3), Times.Once);
        _mockLogWrapper.Verify(log => log.Error(It.IsAny<Exception>(), "Failed to add Test Execution into Database. TestRunId=1"), Times.Once);
    }
}
