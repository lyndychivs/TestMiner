namespace TestMiner.Database.ComponentTests
{
    using System.Collections.Generic;

    using Microsoft.Data.SqlClient;

    using NUnit.Framework;

    using TestMiner.Database.ComponentTests.StoredProcedures;

    public class DatabaseTestsBase
    {
        protected const string ConnectionString = "";

        protected DatabaseTestsBase()
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                Assert.Ignore("ConnectionString is not set. Integration Tests require a live Database Connection.");
            }

            DbConnection = new SqlConnection(ConnectionString);
        }

        protected SqlConnection DbConnection { get; private init; }

        protected static void AssertTableInformationSchemasAreEqual(List<InformationSchemaTable> actual, List<InformationSchemaTable> expected)
        {
            Assert.That(actual, Is.Not.Empty);
            Assert.That(actual, Has.Count.EqualTo(expected.Count), "Row Count does not match, are columns correct?");

            Assert.Multiple(() =>
            {
                for (int i = 0; i < actual.Count; i++)
                {
                    Assert.That(
                        actual[i].TABLE_CATALOG,
                        Is.EqualTo(expected[i].TABLE_CATALOG),
                        $"{actual[i].COLUMN_NAME}: TABLE_CATALOG expected '{expected[i].TABLE_CATALOG}' but was '{actual[i].TABLE_CATALOG}'");
                    Assert.That(
                        actual[i].TABLE_SCHEMA,
                        Is.EqualTo(expected[i].TABLE_SCHEMA),
                        $"{actual[i].COLUMN_NAME}: TABLE_SCHEMA expected '{expected[i].TABLE_SCHEMA}' but was '{actual[i].TABLE_SCHEMA}'");
                    Assert.That(
                        actual[i].TABLE_NAME,
                        Is.EqualTo(expected[i].TABLE_NAME),
                        $"{actual[i].COLUMN_NAME}: TABLE_NAME expected '{expected[i].TABLE_NAME}' but was '{actual[i].TABLE_NAME}'");
                    Assert.That(
                        actual[i].COLUMN_NAME,
                        Is.EqualTo(expected[i].COLUMN_NAME),
                        $"{actual[i].COLUMN_NAME}: COLUMN_NAME expected '{expected[i].COLUMN_NAME}' but was '{actual[i].COLUMN_NAME}'");
                    Assert.That(
                        actual[i].ORDINAL_POSITION,
                        Is.EqualTo(expected[i].ORDINAL_POSITION),
                        $"{actual[i].COLUMN_NAME}: ORDINAL_POSITION expected '{expected[i].ORDINAL_POSITION}' but was '{actual[i].ORDINAL_POSITION}'");
                    Assert.That(
                        actual[i].IS_NULLABLE,
                        Is.EqualTo(expected[i].IS_NULLABLE),
                        $"{actual[i].COLUMN_NAME}: IS_NULLABLE expected '{expected[i].IS_NULLABLE}' but was '{actual[i].IS_NULLABLE}'");
                    Assert.That(
                        actual[i].DATA_TYPE,
                        Is.EqualTo(expected[i].DATA_TYPE),
                        $"{actual[i].COLUMN_NAME}: DATA_TYPE expected '{expected[i].DATA_TYPE}' but was '{actual[i].DATA_TYPE}'");
                    Assert.That(
                        actual[i].CHARACTER_MAXIMUM_LENGTH,
                        Is.EqualTo(expected[i].CHARACTER_MAXIMUM_LENGTH),
                        $"{actual[i].COLUMN_NAME}: CHARACTER_MAXIMUM_LENGTH expected '{expected[i].CHARACTER_MAXIMUM_LENGTH}' but was '{actual[i].CHARACTER_MAXIMUM_LENGTH}'");
                }
            });
        }

        protected static void AssertRoutineInformationSchemasAreEqual(List<InformationSchemaRoutine> actual, List<InformationSchemaRoutine> expected)
        {
            Assert.That(actual, Is.Not.Empty);
            Assert.That(actual, Has.Count.EqualTo(expected.Count), "Row Count does not match, are columns correct?");

            Assert.Multiple(() =>
            {
                for (int i = 0; i < actual.Count; i++)
                {
                    Assert.That(
                        actual[i].ROUTINE_NAME,
                        Is.EqualTo(expected[i].ROUTINE_NAME),
                        $"{actual[i].ROUTINE_NAME}: ROUTINE_NAME expected '{expected[i].ROUTINE_NAME}' but was '{actual[i].ROUTINE_NAME}'");
                    Assert.That(
                        actual[i].ROUTINE_CATALOG,
                        Is.EqualTo(expected[i].ROUTINE_CATALOG),
                        $"{actual[i].ROUTINE_NAME}: ROUTINE_CATALOG expected '{expected[i].ROUTINE_CATALOG}' but was '{actual[i].ROUTINE_CATALOG}'");
                    Assert.That(
                        actual[i].ROUTINE_SCHEMA,
                        Is.EqualTo(expected[i].ROUTINE_SCHEMA),
                        $"{actual[i].ROUTINE_NAME}: ROUTINE_SCHEMA expected '{expected[i].ROUTINE_SCHEMA}' but was '{actual[i].ROUTINE_SCHEMA}'");
                    Assert.That(
                        actual[i].ROUTINE_TYPE,
                        Is.EqualTo(expected[i].ROUTINE_TYPE),
                        $"{actual[i].ROUTINE_NAME}: ROUTINE_TYPE expected '{expected[i].ROUTINE_TYPE}' but was '{actual[i].ROUTINE_TYPE}'");
                }
            });
        }

        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            DbConnection?.Dispose();
        }
    }
}