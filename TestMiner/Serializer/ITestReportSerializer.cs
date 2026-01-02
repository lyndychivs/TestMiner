namespace TestMiner.Serializer;

using TestMiner.TestReports.NUnit3;

internal interface ITestReportSerializer
{
    TestRun Deserialize(string filePath);
}
