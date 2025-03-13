namespace TestMiner.Models.TestRun
{
    using System;

    public interface ITestDto
    {
        string Name { get; set; }

        string ClassName { get; set; }

        Result Result { get; set; }

        long Seed { get; set; }

        string Label { get; set; }

        DateTime StartTime { get; set; }

        DateTime EndTime { get; set; }

        TimeSpan Duration { get; set; }

        int Asserts { get; set; }

        string FailureMessage { get; set; }

        string StackTrace { get; set; }

        string Reason { get; set; }

        string ToString();
    }
}