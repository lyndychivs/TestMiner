namespace TestMiner.TestReports.NUnit3;

using System.Xml.Serialization;

[XmlRoot("environment")]
public class TestEnvironment
{
    [XmlAttribute("machine-name")]
    required public string MachineName { get; set; }

    [XmlAttribute("user")]
    required public string User { get; set; }
}
