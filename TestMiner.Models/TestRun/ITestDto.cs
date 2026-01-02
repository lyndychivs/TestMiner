namespace TestMiner.Models.TestRun;

using System;

public interface ITestDto
{
    string Name { get; init; }

    string ClassName { get; init; }

    Result Result { get; init; }

    long Seed { get; init; }

    string Label { get; set; }

    DateTime StartTime { get; init; }

    DateTime EndTime { get; init; }

    TimeSpan Duration { get; init; }

    int Asserts { get; init; }

    string FailureMessage { get; set; }

    string StackTrace { get; set; }

    string Reason { get; set; }

    string ToString();
}
