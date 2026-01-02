namespace TestMiner.TestReports.NUnit3;

using System;
using System.Xml.Serialization;

public class Test
{
    [XmlAttribute("name")]
    required public string Name { get; set; }

    [XmlAttribute("result")]
    public TestResult Result { get; set; }

    [XmlAttribute("start-time")]
    public string? StartTime { private get; set; }

    [XmlIgnore]
    public DateTime StartDateTimeUtc => DateTime.TryParse(StartTime, out DateTime startDateTime)
        ? startDateTime.ToUniversalTime()
        : DateTime.MinValue;

    [XmlAttribute("end-time")]
    public string? EndTime { private get; set; }

    [XmlIgnore]
    public DateTime EndDateTimeUtc => DateTime.TryParse(EndTime, out DateTime endDateTime)
        ? endDateTime.ToUniversalTime()
        : DateTime.MinValue;

    [XmlAttribute("duration")]
    public double Duration { private get; set; }

    [XmlIgnore]
    public TimeSpan DurationTimeSpan => TimeSpan.FromSeconds(Duration);

    [XmlAttribute("asserts")]
    public int Asserts { get; set; }

    [XmlElement("reason")]
    public TestReason? Reason { get; set; }

    [XmlElement("failure")]
    public TestFailure? Failure { get; set; }
}
