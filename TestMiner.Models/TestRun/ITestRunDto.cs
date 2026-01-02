namespace TestMiner.Models.TestRun;

using System;
using System.Collections.Generic;

public interface ITestRunDto
{
    DateTime StartTime { get; }

    DateTime EndTime { get; }

    TimeSpan Duration { get; }

    int Total { get; }

    int Inconclusive { get; }

    int Passed { get; }

    int Warning { get; }

    int Skipped { get; }

    int Failed { get; }

    int Error { get; }

    IEnvironmentDto Environment { get; }

    TestMinerStatus TestMinerStatus { get; }

    IList<ITestDto> Tests { get; }

    public void AddTest(ITestDto testDto);

    string ToString();

    string CalculateMd5Hash();
}
