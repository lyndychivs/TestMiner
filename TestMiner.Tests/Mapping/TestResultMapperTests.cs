namespace TestMiner.Tests.Mapping;

using NUnit.Framework;

using TestMiner.Mapping;
using TestMiner.Models.TestRun;
using TestMiner.TestReports.NUnit3;

[TestFixture]
public class TestResultMapperTests
{
    [TestCase(TestResult.Inconclusive, Result.Inconclusive)]
    [TestCase(TestResult.Passed, Result.Passed)]
    [TestCase(TestResult.Warning, Result.Warning)]
    [TestCase(TestResult.Skipped, Result.Skipped)]
    [TestCase(TestResult.Failed, Result.Failed)]
    [TestCase(TestResult.Error, Result.Error)]
    public void MapToResult_ValidTestResult_ReturnsExpectedResult(TestResult testResult, Result expectedResult)
    {
        var result = testResult.MapToResult();

        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
