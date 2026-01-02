namespace TestMiner.Mapping;

using TestMiner.Models.TestRun;

using TestMiner.TestReports.NUnit3;

internal interface ITestRunMapper
{
    ITestRunDto MapTestRunToDto(TestRun testRun);
}
