namespace TestMiner.TestReports.NUnit3
{
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
        public DateTime StartDateTimeUtc
        {
            get
            {
                if (DateTime.TryParse(StartTime, out DateTime startDateTime))
                {
                    return startDateTime.ToUniversalTime();
                }

                return DateTime.MinValue;
            }
        }

        [XmlAttribute("end-time")]
        public string? EndTime { private get; set; }

        [XmlIgnore]
        public DateTime EndDateTimeUtc
        {
            get
            {
                if (DateTime.TryParse(EndTime, out DateTime endDateTime))
                {
                    return endDateTime.ToUniversalTime();
                }

                return DateTime.MinValue;
            }
        }

        [XmlAttribute("duration")]
        public double Duration { private get; set; }

        [XmlIgnore]
        public TimeSpan DurationTimeSpan
        {
            get
            {
                return TimeSpan.FromSeconds(Duration);
            }
        }

        [XmlAttribute("asserts")]
        public int Asserts { get; set; }

        [XmlElement("reason")]
        public TestReason? Reason { get; set; }

        [XmlElement("failure")]
        public TestFailure? Failure { get; set; }
    }
}