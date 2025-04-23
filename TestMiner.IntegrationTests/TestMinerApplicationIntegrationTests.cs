namespace TestMiner.IntegrationTests
{
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
    public class TestMinerApplicationIntegrationTests
    {
        [Test]
        [Explicit("Limited Mocks Integration Test; Performs IO operations.")]
        public void TestMinerApplication_WhenCalledWithFilesToProcess_AddsTestRunToTheDatabase()
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
                "033BB4EC0C66302D5A01B467C18111E2"))
                .Returns(1);

            var logWrapper = new LogWrapper(mockLogger.Object);
            var testMinerDal = new TestMinerDal(logWrapper, mockDatabase.Object);
            var testMinerApplication = new TestMinerApplication(
                logWrapper,
                new FileWrapper(),
                new TestReportSerializer(),
                new TestRunMapper(),
                testMinerDal);

            var result = testMinerApplication.ProcessFiles(["SampleData\\TestResultSample.xml"]);

            Assert.That(result, Is.EqualTo(0));

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

            mockLogger.VerifyLogging("Finished Processing File with Hash: 033BB4EC0C66302D5A01B467C18111E2 - SampleData\\TestResultSample.xml", LogLevel.Information, Times.Once());
        }
    }
}