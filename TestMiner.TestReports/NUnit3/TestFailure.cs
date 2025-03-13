namespace TestMiner.TestReports.NUnit3
{
    using System.Xml.Serialization;

    public class TestFailure
    {
        [XmlElement("message")]
        public string? Message { get; set; }

        [XmlElement("stack-trace")]
        public string? StackTrace { get; set; }
    }
}
