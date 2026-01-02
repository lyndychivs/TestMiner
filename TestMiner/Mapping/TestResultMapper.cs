namespace TestMiner.Mapping;

using TestMiner.Models.TestRun;
using TestMiner.TestReports.NUnit3;

internal static class TestResultMapper
{
    internal static Result MapToResult(this TestResult testResult)
    {
        return testResult switch
        {
            TestResult.Inconclusive => Result.Inconclusive,
            TestResult.Passed => Result.Passed,
            TestResult.Warning => Result.Warning,
            TestResult.Skipped => Result.Skipped,
            TestResult.Failed => Result.Failed,
            TestResult.Error => Result.Error,
            _ => Result.Inconclusive,
        };
    }
}
