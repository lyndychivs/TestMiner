namespace TestMiner.Database.ComponentTests.StoredProcedures
{
    using System.Data;
    using System.Linq;

    using Dapper;

    using NUnit.Framework;

    [TestFixture]
    [Explicit("Tests require a live Database Connection.")]
    public class StoredProceduresTests : DatabaseTestsBase
    {
        [Test]
        public void Validate_Stored_Procedure_TestExecutions_Add_Test_Exists()
        {
            var expected = TestExecutionsAddTestInformationSchema.Get();

            var actual = DbConnection.Query<InformationSchemaRoutine>(
                "SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'spTestExecutions_AddTest'",
                commandType: CommandType.Text)
                .ToList();

            AssertRoutineInformationSchemasAreEqual(actual, expected);
        }

        [Test]
        public void Validate_Stored_Procedure_TestRuns_Add_Test_Run_Exists()
        {
            var expected = TestRunsAddTestRunInformationSchema.Get();

            var actual = DbConnection.Query<InformationSchemaRoutine>(
                "SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'spTestRuns_AddTestRun'",
                commandType: CommandType.Text)
                .ToList();

            AssertRoutineInformationSchemasAreEqual(actual, expected);
        }

        [Test]
        public void Validate_Stored_Procedure_TestRuns_Get_Id_From_Hex_Exists()
        {
            var expected = TestRunsGetIdFromHexInformationSchema.Get();

            var actual = DbConnection.Query<InformationSchemaRoutine>(
                "SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'spTestRuns_GetIdFromHex'",
                commandType: CommandType.Text)
                .ToList();

            AssertRoutineInformationSchemasAreEqual(actual, expected);
        }

        [Test]
        public void Validate_Stored_Procedure_TestRuns_Update_TestMinerStatus_Exists()
        {
            var expected = TestRunsUpdateTestMinerStatusInformationSchema.Get();

            var actual = DbConnection.Query<InformationSchemaRoutine>(
                "SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'spTestRuns_UpdateTestMinerStatus'",
                commandType: CommandType.Text)
                .ToList();

            AssertRoutineInformationSchemasAreEqual(actual, expected);
        }
    }
}