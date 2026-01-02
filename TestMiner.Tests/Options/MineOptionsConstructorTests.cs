namespace TestMiner.Tests.Options;

using NUnit.Framework;

using TestMiner.Options;

[TestFixture]
public class MineOptionsConstructorTests
{
    [Test]
    public void Constructor_WithReportFilePaths_ReturnsMineOptions()
    {
        var reportFilePaths = new[] { "a" };

        var mineOptions = new MineOptions()
        {
            ReportFilePaths = reportFilePaths,
        };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(mineOptions, Is.Not.Null);
            Assert.That(mineOptions.ReportFilePaths, Is.EqualTo(reportFilePaths));
            Assert.That(mineOptions.ConnectionString, Is.EqualTo(string.Empty));
        }
    }
}
