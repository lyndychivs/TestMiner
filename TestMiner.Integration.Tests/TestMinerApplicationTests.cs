namespace TestMiner.Integration.Tests;

using System;

using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;

using TestMiner.DataAccessLayer;
using TestMiner.Database;
using TestMiner.Logger;
using TestMiner.Mapping;
using TestMiner.Serializer;
using TestMiner.Utility;

[TestFixture]
public class TestMinerApplicationTests
{
    [Test]
    public void TestMinerApplication_WhenCalledWithFilesToMine_AddsTestRunToTheDatabase()
    {
        var mockLogger = new Mock<ILogger>();
        var mockDatabase = new Mock<IDatabase>();

        mockDatabase.Setup(db => db.GetTestRunIdFromHex(It.IsAny<string>())).Returns(0);
        mockDatabase.Setup(db => db.AddTestRun(
            It.IsAny<DateTime>(),
            It.IsAny<DateTime>(),
            It.IsAny<long>(),
            6,
            1,
            1,
            1,
            1,
            1,
            1,
            "Administrator",
            "DESKTOP",
            "43CCE649D657AD3159C3CD6628E5AC40"))
            .Returns(1);

        var logWrapper = new LogWrapper(mockLogger.Object);
        var testMinerDal = new TestMinerDal(logWrapper, mockDatabase.Object);
        var testMinerApplication = new TestMinerApplication(
            logWrapper,
            new FileWrapper(logWrapper),
            new TestReportSerializer(logWrapper),
            new TestRunMapper(logWrapper),
            testMinerDal);

        int result = testMinerApplication.MineFiles(["SampleData\\TestResultSample.xml"]);

        Assert.That(result, Is.Zero);

        mockDatabase.Verify(db => db.GetTestRunIdFromHex(It.IsAny<string>()), Times.Once());

        mockDatabase.Verify(
            db => db.AddTestExecution(
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
            Times.Exactly(6));

        mockDatabase.Verify(db => db.UpdateTestRunTestMinerStatus(1, 2), Times.Once());

        mockLogger.VerifyLogging("Finished mining File: 43CCE649D657AD3159C3CD6628E5AC40 : SampleData\\TestResultSample.xml", LogLevel.Information, Times.Once());
    }
}
