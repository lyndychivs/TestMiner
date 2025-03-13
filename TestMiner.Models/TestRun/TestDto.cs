namespace TestMiner.Models.TestRun
{
    using System;

    public class TestDto : ITestDto
    {
        required public string Name { get; set; }

        required public string ClassName { get; set; }

        public Result Result { get; set; }

        public long Seed { get; set; }

        public string Label { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public TimeSpan Duration { get; set; }

        public int Asserts { get; set; }

        public string FailureMessage { get; set; } = string.Empty;

        public string StackTrace { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{ClassName}.{Name} {nameof(Result)}: {Result} {nameof(StartTime)}: {StartTime} {nameof(EndTime)}: {EndTime} {nameof(Duration)}: {Duration} {nameof(Asserts)}: {Asserts} {nameof(FailureMessage)}: {FailureMessage}";
        }
    }
}