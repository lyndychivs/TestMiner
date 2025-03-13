namespace TestMiner.Models.TestRun
{
    using System;
    using System.Collections.Generic;

    public interface ITestRunDto
    {
        DateTime StartTime { get; set; }

        DateTime EndTime { get; set; }

        TimeSpan Duration { get; set; }

        int Total { get; }

        int Inconclusive { get; }

        int Passed { get; }

        int Warning { get; }

        int Skipped { get; }

        int Failed { get; }

        int Error { get; }

        IEnvironmentDto Environment { get; set; }

        TestMinerStatus TestMinerStatus { get; set; }

        IList<ITestDto> Tests { get; }

        public void AddTest(ITestDto testDto);

        string ToString();

        string CalculateMd5Hash();
    }
}