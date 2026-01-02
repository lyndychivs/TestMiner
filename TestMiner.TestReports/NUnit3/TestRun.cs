namespace TestMiner.TestReports.NUnit3;

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("test-run")]
public class TestRun
{
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

    [XmlElement("test-suite")]
    public List<TestSuite> TestSuites { get; set; } = [];
}
