namespace TestMiner.Models.TestRun;

using System;

public class TestDto : ITestDto
{
    required public string Name { get; init; }

    required public string ClassName { get; init; }

    public Result Result { get; init; }

    public long Seed { get; init; }

    public string Label { get; set; } = string.Empty;

    public DateTime StartTime { get; init; }

    public DateTime EndTime { get; init; }

    public TimeSpan Duration { get; init; }

    public int Asserts { get; init; }

    public string FailureMessage { get; set; } = string.Empty;

    public string StackTrace { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{ClassName}.{Name}";
    }
}
