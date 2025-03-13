namespace TestMiner.TestReports.NUnit3
{
    using System.Xml.Serialization;

    [XmlRoot("test-case")]
    public class TestCase : Test
    {
        [XmlAttribute("classname")]
        required public string ClassName { get; set; }

        [XmlAttribute("seed")]
        public long Seed { get; set; }

        [XmlAttribute("label")]
        public string? Label { get; set; }
    }
}