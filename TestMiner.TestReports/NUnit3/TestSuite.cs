namespace TestMiner.TestReports.NUnit3;

using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("test-suite")]
public class TestSuite : Test
{
    [XmlAttribute("type")]
    public TestSuiteType Type { get; set; }

    [XmlElement("environment")]
    public TestEnvironment? Environment { get; set; }

    [XmlElement("test-suite", typeof(TestSuite))]
    [XmlElement("test-case", typeof(TestCase))]
    public List<Test> Tests { get; set; } = [];
}
