namespace TestMiner.Database.ComponentTests.Tables
{
    using System.Data;
    using System.Linq;

    using Dapper;

    using NUnit.Framework;

    [TestFixture]
    [Explicit("Integration Tests require a live Database Connection.")]
    public class TablesTests : DatabaseTestsBase
    {
        [Test]
        public void Validate_EnvironmentMachines_Is_Deployed()
        {
            var expected = EnvironmentMachinesInformationSchema.Get();

            var actual = DbConnection.Query<InformationSchemaTable>(
                "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EnvironmentMachines'",
                commandType: CommandType.Text)
                .ToList();

            AssertTableInformationSchemasAreEqual(actual, expected);
        }

        [Test]
        public void Validate_EnvironmentUsers_Is_Deployed()
        {
            var expected = EnvironmentUsersInformationSchema.Get();

            var actual = DbConnection.Query<InformationSchemaTable>(
                "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'EnvironmentUsers'",
                commandType: CommandType.Text)
                .ToList();

            AssertTableInformationSchemasAreEqual(actual, expected);
        }

        [Test]
        public void Validate_TestClasses_Is_Deployed()
        {
            var expected = TestClassesInformationSchema.Get();

            var actual = DbConnection.Query<InformationSchemaTable>(
                "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TestClasses'",
                commandType: CommandType.Text)
                .ToList();

            AssertTableInformationSchemasAreEqual(actual, expected);
        }

        [Test]
        public void Validate_TestExecutions_Is_Deployed()
        {
            var expected = TestExecutionsInformationSchema.Get();

            var actual = DbConnection.Query<InformationSchemaTable>(
                "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TestExecutions'",
                commandType: CommandType.Text)
                .ToList();

            AssertTableInformationSchemasAreEqual(actual, expected);
        }

        [Test]
        public void Validate_TestMinerStatus_Is_Deployed()
        {
            var expected = TestMinerStatusInformationSchema.Get();

            var actual = DbConnection.Query<InformationSchemaTable>(
                "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TestMinerStatus'",
                commandType: CommandType.Text)
                .ToList();

            AssertTableInformationSchemasAreEqual(actual, expected);
        }

        [Test]
        public void Validate_TestResults_Is_Deployed()
        {
            var expected = TestResultsInformationSchema.Get();

            var actual = DbConnection.Query<InformationSchemaTable>(
                "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TestResults'",
                commandType: CommandType.Text)
                .ToList();

            AssertTableInformationSchemasAreEqual(actual, expected);
        }

        [Test]
        public void Validate_TestRuns_Is_Deployed()
        {
            var expected = TestRunsInformationSchema.Get();

            var actual = DbConnection.Query<InformationSchemaTable>(
                "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TestRuns'",
                commandType: CommandType.Text)
                .ToList();

            AssertTableInformationSchemasAreEqual(actual, expected);
        }

        [Test]
        public void Validate_Tests_Is_Deployed()
        {
            var expected = TestsInformationSchema.Get();

            var actual = DbConnection.Query<InformationSchemaTable>(
                "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Tests'",
                commandType: CommandType.Text)
                .ToList();

            AssertTableInformationSchemasAreEqual(actual, expected);
        }

        [Test]
        public void Validate_TestMinerStatus_Is_Populated()
        {
            var expected = TestMinerStatusData.Get();

            var actual = DbConnection.Query<TestMinerStatus>(
                "SELECT * FROM TestMiner.dbo.TestMinerStatus ORDER BY Id ASC",
                commandType: CommandType.Text)
                .ToList();

            Assert.That(actual, Is.Not.Empty, "TestMinerStatus table is empty.");
            Assert.That(actual, Has.Count.EqualTo(expected.Count), "Row Count does not match, are columns correct?");

            Assert.Multiple(() =>
            {
                for (int i = 0; i < actual.Count; i++)
                {
                    Assert.That(
                        actual[i].Id,
                        Is.EqualTo(expected[i].Id),
                        $"{actual[i].Status}: Id expected '{expected[i].Id}' but was '{actual[i].Id}'");
                    Assert.That(
                        actual[i].Status,
                        Is.EqualTo(expected[i].Status),
                        $"{actual[i].Status}: Status expected '{expected[i].Status}' but was '{actual[i].Status}'");
                }
            });
        }

        [Test]
        public void Validate_TestResults_Is_Populated()
        {
            var expected = TestResultsData.Get();

            var actual = DbConnection.Query<TestResult>(
                "SELECT * FROM TestMiner.dbo.TestResults ORDER BY Id ASC",
                commandType: CommandType.Text)
                .ToList();

            Assert.That(actual, Is.Not.Empty, "TestResults table is empty.");
            Assert.That(actual, Has.Count.EqualTo(expected.Count), "Row Count does not match, are columns correct?");

            Assert.Multiple(() =>
            {
                for (int i = 0; i < actual.Count; i++)
                {
                    Assert.That(
                        actual[i].Id,
                        Is.EqualTo(expected[i].Id),
                        $"{actual[i].Result}: Id expected '{expected[i].Id}' but was '{actual[i].Id}'");
                    Assert.That(
                        actual[i].Result,
                        Is.EqualTo(expected[i].Result),
                        $"{actual[i].Result}: Result expected '{expected[i].Result}' but was '{actual[i].Result}'");
                }
            });
        }
    }
}