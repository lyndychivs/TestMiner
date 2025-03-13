namespace TestMiner.Tests.Options
{
    using NUnit.Framework;

    using TestMiner.Options;

    [TestFixture]
    public class TestMinerOptionsConstructorTests
    {
        [Test]
        public void Constructor_WithReportFilePaths_ReturnsTestMinerOptions()
        {
            var reportFilePaths = new[] { "a" };

            var testMinerOptions = new TestMinerOptions()
            {
                ReportFilePaths = reportFilePaths,
            };

            Assert.Multiple(() =>
            {
                Assert.That(testMinerOptions, Is.Not.Null);
                Assert.That(testMinerOptions.ReportFilePaths, Is.EqualTo(reportFilePaths));
                Assert.That(testMinerOptions.ConnectionString, Is.EqualTo(string.Empty));
            });
        }
    }
}