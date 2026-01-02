namespace TestMiner.Serializer;

using System;
using System.IO;
using System.Xml;
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
            using var stringReader = new StringReader(fileContent);
            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Prohibit,
                XmlResolver = null,
            };

            using var xmlReader = XmlReader.Create(stringReader, settings);

            return new XmlSerializer(typeof(TestRun)).Deserialize(xmlReader) as TestRun ?? throw new NullReferenceException(nameof(TestRun));
        }
        catch (Exception exception)
        {
            _logWrapper.Error(exception, $"Failed to {nameof(Deserialize)} {nameof(TestRun)}.");
            throw;
        }
    }
}
