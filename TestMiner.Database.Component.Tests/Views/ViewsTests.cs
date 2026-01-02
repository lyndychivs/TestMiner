namespace TestMiner.Database.Component.Tests.Views;

using System.Data;
using System.Linq;

using Dapper;

using NUnit.Framework;

using TestMiner.Database.Component.Tests;

[TestFixture]
[Explicit("Tests require a live Database Connection.")]
public class ViewsTests : DatabaseTestsBase
{
    [Test]
    public void Validate_vTestExecutions_Is_Deployed()
    {
        var expected = ViewTestExecutionsInformationSchema.Get();

        var actual = DbConnection.Query<InformationSchemaTable>(
            "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'vTestExecutions'",
            commandType: CommandType.Text)
            .ToList();

        AssertTableInformationSchemasAreEqual(actual, expected);
    }

    [Test]
    public void Validate_vTestRuns_Is_Deployed()
    {
        var expected = ViewTestRunsInformationSchema.Get();

        var actual = DbConnection.Query<InformationSchemaTable>(
            "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'vTestRuns'",
            commandType: CommandType.Text)
            .ToList();

        AssertTableInformationSchemasAreEqual(actual, expected);
    }
}
