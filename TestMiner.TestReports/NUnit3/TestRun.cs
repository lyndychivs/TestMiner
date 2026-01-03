namespace TestMiner.TestReports.NUnit3;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

[XmlRoot("test-run")]
public class TestRun
{
    [XmlAttribute("start-time")]
    public string? StartTime { private get; set; }

    [XmlIgnore]
    public DateTime StartDateTimeUtc => DateTime.TryParse(StartTime, CultureInfo.InvariantCulture, out DateTime startDateTime)
        ? startDateTime.ToUniversalTime()
        : DateTime.MinValue;

    [XmlAttribute("end-time")]
    public string? EndTime { private get; set; }

    [XmlIgnore]
    public DateTime EndDateTimeUtc => DateTime.TryParse(EndTime, CultureInfo.InvariantCulture, out DateTime endDateTime)
        ? endDateTime.ToUniversalTime()
        : DateTime.MinValue;

    [XmlAttribute("duration")]
    public double Duration { private get; set; }

    [XmlIgnore]
    public TimeSpan DurationTimeSpan => TimeSpan.FromSeconds(Duration);

    [XmlElement("test-suite")]
    public List<TestSuite> TestSuites { get; set; } = [];
}
