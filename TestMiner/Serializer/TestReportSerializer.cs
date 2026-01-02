namespace TestMiner.Serializer;

using System;
using System.IO;
using System.Xml.Serialization;

using TestMiner.Logger;
using TestMiner.TestReports.NUnit3;

internal class TestReportSerializer : ITestReportSerializer
{
    private readonly ILogWrapper _logWrapper;

    internal TestReportSerializer(ILogWrapper logWrapper)
    {
        _logWrapper = logWrapper ?? throw new ArgumentNullException(nameof(logWrapper));
    }

    public TestRun Deserialize(string fileContent)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileContent);

        try
        {
            return new XmlSerializer(typeof(TestRun)).Deserialize(new StringReader(fileContent)) as TestRun ?? throw new NullReferenceException(nameof(TestRun));
        }
        catch (Exception exception)
        {
            _logWrapper.Error(exception, $"Failed to {nameof(Deserialize)} {nameof(TestRun)}.");
            throw;
        }
    }
}
