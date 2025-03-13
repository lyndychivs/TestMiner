namespace TestMiner.TestReports.NUnit3
{
    using System.Xml.Serialization;

    public class TestReason
    {
        [XmlElement("message")]
        public string? Messages { get; set; }
    }
}